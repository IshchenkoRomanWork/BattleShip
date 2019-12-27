using CustomORM.Interfaces;
using CustomORM.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using System.Collections;
using CustomORM.Models.Attributes;
using System.Data;

namespace CustomORM.Services
{
    public class ORM<Model> : IORM<Model> 
    {
        private IRepository _repository { get; set; }
        private IMapper<Model> _mapper;
        private AttributeHelper _helper;
        private string _dataBaseName;

        public ORM(string connectionString)
        {
            _helper = new AttributeHelper();
            if (!_helper.HasAttribute(typeof(Model), typeof(TableAttribute)))
                throw new Exception("Is not database type!");

            _repository = new ADORepository(connectionString);
            _mapper = new Mapper<Model>();
            _dataBaseName = _helper.GetDataBaseAttribute(typeof(Model)).DBName;

        }
        public IEnumerable<Model> GetAllFromDatabase()
        {
            var dboList = _repository.GetAll(_dataBaseName);
            var dtoList = dboList.Select(dbo => (Model)GetDTOInCohesion(dbo, typeof(Model)).InnerObject);
            return dtoList;
        }
        public void DeleteFromDatabase(object id) //OnDelete logic lies on database here
        {
            Model oldModel = GetFromDatabase(id);
            List<DBObject> cohesionList = GetDBOInCohesion(oldModel, typeof(Model));
            DeleteDBOListWithCohesionRespect(cohesionList);
        }
        public Model GetFromDatabase(object id)
        {
            var dbo = _repository.Get(id, _dataBaseName);
            var dto = GetDTOInCohesion(dbo, typeof(Model));
            return (Model)dto.InnerObject;
        }
        public void InsertToDatabase(Model item)
        {
            List<DBObject> cohesionList = GetDBOInCohesion(item, typeof(Model));
            foreach (var dbo in cohesionList)
            {
                _repository.Create(dbo);
            }
        }
        public void UpdateInDatabase(Model item)
        {
            List<DBObject> cohesionList = GetDBOInCohesion(item, typeof(Model));
            if (!_repository.Exists(cohesionList[0]))
            {
                throw new Exception("There's no according model in db");
            }

            Model oldModel = GetFromDatabase(_helper.GetId(item));
            List<DBObject> oldCohesionList = GetDBOInCohesion(oldModel, typeof(Model));

            DeleteDBOListWithCohesionRespect(oldCohesionList.Except(cohesionList));
            foreach (var dbo in cohesionList)
            {
                if (_repository.Exists(dbo))
                {
                    _repository.Update(dbo);
                }
                else
                {
                    _repository.Create(dbo);
                }
            }
        }
        private List<DBObject> GetDBOInCohesion(object item, Type baseType)
        {
            List<DBObject> finalList = new List<DBObject>();
            var dto = GetDTOFromModel(item, baseType);
            var dbo = _mapper.GetDboFrom(dto);

            var foreignKeys = _helper.GetAllForeignKeys(item.GetType());
            var toManyForeignKeys = foreignKeys.Where(fk => _helper.IsToMany(fk));
            var toOneForeignKeys = foreignKeys.Where(fk => !_helper.IsToMany(fk));
            Type foreignKeyBaseType;

            foreach (var foreignKey in toOneForeignKeys)
            {
                object foreignKeyValue = foreignKey.GetValue(item);
                foreignKeyBaseType = foreignKey.GetObjectType();
                ColumnAttribute colAttrib = (ColumnAttribute)_helper.GetDataBaseAttribute(foreignKey.AsMemberInfo());

                finalList.AddRange(GetDBOInCohesion(foreignKeyValue, foreignKeyBaseType));
                dbo.Add(_helper.GetId(foreignKeyValue), colAttrib.DBName, _helper.ParseToSqlDbType(colAttrib.DBDataType));

            }
            finalList.Add(dbo); //Position is relevant due to foreign key constraint conflicts
            foreach (var foreignKey in toManyForeignKeys)
            {
                object foreignKeyValue = foreignKey.GetValue(item);
                foreignKeyBaseType = foreignKey.GetObjectType().GenericTypeArguments[0];
                ColumnAttribute colAttrib = (ColumnAttribute)_helper.GetDataBaseAttribute(foreignKey.AsMemberInfo());

                foreach (var element in foreignKeyValue as ICollection)
                {
                    var innerList = GetDBOInCohesion(element, foreignKeyBaseType);
                    finalList.AddRange(innerList); 
                    var toManydbo = innerList[innerList.Count - 1];
                    toManydbo.Add(_helper.GetId(item), colAttrib.DBName, _helper.ParseToSqlDbType(colAttrib.DBDataType));
                }
            }
            return finalList;

        }
        private DTObject GetDTOInCohesion(DBObject dbo, Type type)
        {
            var dto = _mapper.GetDtoFrom(dbo, type);
            var model = dto.InnerObject;
            var foreignKeys = _helper.GetAllForeignKeys(model.GetType());
            foreach (var foreignKey in foreignKeys)
            {
                Type foreignKeyType = foreignKey.GetObjectType();
                object foreignKeyValue;
                if (_helper.IsToMany(foreignKey)) //foreignKeyValue is empty
                {
                    var foreignKeyElementType = foreignKeyType.GenericTypeArguments[0];
                    var foreignKeyElementDboList = _repository.GetAllWithForeignKey(_helper.GetDataBaseAttribute(model.GetType()).DBName,
                        _helper.GetId(model), _helper.GetDataBaseAttribute(foreignKeyElementType).DBName, true);
                    foreignKeyValue = foreignKeyElementDboList.Select(dbo => GetDTOInCohesion(dbo, foreignKeyElementType).InnerObject).ToList();
                    MethodInfo foreignKeyAddMethod = foreignKeyType.GetMethod("Add");
                    object castToDtoList = Activator.CreateInstance(foreignKeyType);
                    foreach (var dtoForeignKeyElement in foreignKeyValue as ICollection)
                    {
                        foreignKeyAddMethod.Invoke(castToDtoList, new object[] { Convert.ChangeType(dtoForeignKeyElement, foreignKeyElementType) });
                    }
                    foreignKey.SetValue(dto.InnerObject, castToDtoList);
                    break;
                }
                else
                {
                    object foreignKeyId = dbo.GetValueByColumnName(_helper.GetDataBaseAttribute(foreignKey.AsMemberInfo()).DBName);
                    var foreignKeyDBOValue = _repository.Get(foreignKeyId, _helper.GetDataBaseAttribute(foreignKeyType).DBName);
                    foreignKeyValue = GetDTOInCohesion(foreignKeyDBOValue, foreignKeyType).InnerObject;
                    foreignKey.SetValue(dto.InnerObject, foreignKeyValue);
                }
            }
            return dto;
        }

        private void DeleteDBOListWithCohesionRespect(IEnumerable<DBObject> dboEnum)
        {
            //var dboList = dboEnum.ToList();
            //while(dboList.Count != 0)
            //{
            //    int startingCount = dboList.Count;
            //    for (int i = 0; i < dboList.Count;)
            //    {
            //        if (!dboList.Any(dbo => dbo.RowValues.Any(rv => rv == dboList[i].PrimaryKey)))
            //        {
            //            _repository.Delete(dboList[i].PrimaryKey, dboList[i].TableName);
            //            dboList.RemoveAt(i);
            //        }
            //        else
            //        {
            //            i++;
            //        }
            //    }
            //    if(startingCount == dboList.Count)
            //    {
            //        throw new Exception("Database is fully FK self-connected, we can't delete any value");
            //    }
            //}
        }
        private DTObject GetDTOFromModel(object item, Type baseType)
        {
            var propertyFields = _helper.GetPropertyFieldList(item.GetType());
            return new DTObject()
            {
                BaseType = baseType,
                InnerObject = item,
                PropertiesFields = propertyFields.ToList()
            };
        }
    }
}

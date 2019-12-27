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
    public class ORM<Model> : IORM<Model> where Model : new()
    {
        private IRepository _repository { get; set; }
        private IMapper<Model> _mapper;
        private AttributeHelper _helper;

        public ORM(string connectionString)
        {
            _helper = new AttributeHelper();
            if (!_helper.HasAttribute(typeof(Model), typeof(TableAttribute)))
                throw new Exception("Is not database type!");

            _repository = new ADORepository(connectionString);
            _mapper = new Mapper<Model>();

        }

        public void DeleteFromDatabase(object id) //OnDelete logic lies on database here
        {
            _repository.Delete(id, _helper.GetDataBaseAttribute(typeof(Model)).DBName);
        }

        public Model GetFromDatabase(object id)
        {
            var dbo = _repository.Get(id, _helper.GetDataBaseAttribute(typeof(Model)).DBName);
            var dto = GetDTOInCohesion(dbo, typeof(Model));
            return (Model)dto.InnerObject;
        }

        public void InsertToDatabase(Model item)
        {
            List<DBObject> cohesionList = GetDBOInCohesion(item);
            foreach (var dbo in cohesionList)
            {
                _repository.Create(dbo);
            }
        }

        public void UpdateInDatabase(Model item)
        {
            List<DBObject> cohesionList = GetDBOInCohesion(item);
            if (!_repository.Exists(cohesionList[0]))
            {
                throw new Exception("There's no according model in db");
            }
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
        private List<DBObject> GetDBOInCohesion(object item)
        {
            List<DBObject> finalList = new List<DBObject>();
            var dto = GetDTOFromModel(item);
            var dbo = _mapper.GetDboFrom(dto);

            var foreignKeys = _helper.GetAllForeignKeys(item.GetType());
            var toManyForeignKeys = foreignKeys.Where(fk => _helper.IsToMany(fk));
            var toOneForeignKeys = foreignKeys.Where(fk => !_helper.IsToMany(fk));
            foreach (var foreignKey in toOneForeignKeys)
            {
                object foreignKeyValue = foreignKey.GetValue(item);
                ColumnAttribute colAttrib = (ColumnAttribute)_helper.GetDataBaseAttribute(foreignKey.AsMemberInfo());

                finalList.AddRange(GetDBOInCohesion(foreignKeyValue));
                dbo.Add(_helper.GetId(foreignKeyValue), colAttrib.DBName, _helper.ParseToSqlDbType(colAttrib.DBDataType));

            }
            finalList.Add(dbo); //Position is relevant due to foreign key constraint conflicts
            foreach (var foreignKey in toManyForeignKeys)
            {
                object foreignKeyValue = foreignKey.GetValue(item);
                ColumnAttribute colAttrib = (ColumnAttribute)_helper.GetDataBaseAttribute(foreignKey.AsMemberInfo());

                foreach (var element in foreignKeyValue as ICollection)
                {
                    var innerList = GetDBOInCohesion(element);
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
                    object foreignKeyId = null;
                    for (int i = 0; i < dbo.ColumnNames.Count; i++)
                    {
                        if (dbo.ColumnNames[i] == _helper.GetDataBaseAttribute(foreignKey.AsMemberInfo()).DBName)
                        {
                            foreignKeyId = dbo.RowValues[i];
                        }
                    }
                    var foreignKeyDBOValue = _repository.Get(foreignKeyId, _helper.GetDataBaseAttribute(foreignKeyType).DBName);
                    foreignKeyValue = GetDTOInCohesion(foreignKeyDBOValue, foreignKeyType).InnerObject;
                    foreignKey.SetValue(dto.InnerObject, foreignKeyValue);
                }
            }
            return dto;
        }

        private DTObject GetDTOFromModel(object item)
        {
            var propertyFields = _helper.GetPropertyFieldList(item.GetType());
            return new DTObject()
            {
                InnerObject = item,
                PropertiesFields = propertyFields.ToList()
            };
        }
    }
}

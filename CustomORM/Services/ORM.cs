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
            if(!_repository.Exists(cohesionList[0]))
            {
                throw new Exception("There's no according model in db");
            }
            foreach (var dbo in cohesionList)
            {
                if(_repository.Exists(dbo))
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
                object foreignKeyValue = null;
                switch (foreignKey.MemberType)
                {
                    case MemberTypes.Property:
                        var property = foreignKey as PropertyInfo;
                        foreignKeyValue = property.GetValue(item);
                        break;
                    case MemberTypes.Field:
                        var field = foreignKey as PropertyInfo;
                        foreignKeyValue = field.GetValue(item);
                        break;
                }
                ColumnAttribute colAttrib = (ColumnAttribute)_helper.GetDataBaseAttribute(foreignKey);

                finalList.AddRange(GetDBOInCohesion(foreignKeyValue));
                dbo.RowValues.Add(_helper.GetId(foreignKeyValue));
                dbo.ColumnNames.Add(colAttrib.DBName);
                dbo.ColumnDataTypes.Add((SqlDbType)Enum.Parse(typeof(SqlDbType),
                    _helper.TrimCases(colAttrib.DBDataType), true));

            }
            finalList.Add(dbo); //Position is relevant due to foreign key constraint conflicts
            foreach (var foreignKey in toManyForeignKeys)
            {
                object foreignKeyValue = null;
                switch (foreignKey.MemberType)
                {
                    case MemberTypes.Property:
                        var property = foreignKey as PropertyInfo;
                        foreignKeyValue = property.GetValue(item);
                        break;
                    case MemberTypes.Field:
                        var field = foreignKey as PropertyInfo;
                        foreignKeyValue = field.GetValue(item);
                        break;
                }
                ColumnAttribute colAttrib = (ColumnAttribute)_helper.GetDataBaseAttribute(foreignKey);

                foreach (var element in foreignKeyValue as ICollection)
                { 
                    var innerList = GetDBOInCohesion(element);
                    finalList.AddRange(innerList);
                    var toManydbo = innerList[innerList.Count - 1];
                    toManydbo.ColumnNames.Add(colAttrib.DBName);
                    toManydbo.ColumnDataTypes.Add((SqlDbType)Enum.Parse(typeof(SqlDbType),
                        _helper.TrimCases(colAttrib.DBDataType), true));
                    toManydbo.RowValues.Add(_helper.GetId(item));
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
                Type foreignKeyType = null;
                switch (foreignKey.MemberType)
                {
                    case MemberTypes.Property:
                        var property = foreignKey as PropertyInfo;
                        foreignKeyType = property.PropertyType;
                        break;
                    case MemberTypes.Field:
                        var field = foreignKey as FieldInfo;
                        foreignKeyType = field.FieldType;
                        break;
                }
                object foreignKeyValue;
                if (_helper.IsToMany(foreignKey)) //foreignKeyValue is empty
                {
                    var elementType = foreignKeyType.GenericTypeArguments[0];
                    var dboForeignKeyList = _repository.GetAllWithForeignKey(_helper.GetDataBaseAttribute(model.GetType()).DBName,
                        _helper.GetId(model), _helper.GetDataBaseAttribute(elementType).DBName, true);
                    foreignKeyValue = dboForeignKeyList.Select(dbo => GetDTOInCohesion(dbo, elementType).InnerObject).ToList();
                    object castToDtoList = null;
                    switch (foreignKey.MemberType)
                    {
                        case MemberTypes.Property:
                            PropertyInfo propertyFKtype = foreignKey as PropertyInfo;
                            MethodInfo pfkeyAdd = propertyFKtype.PropertyType.GetMethod("Add");
                            castToDtoList = Activator.CreateInstance(propertyFKtype.PropertyType);
                            foreach (var dtoForeignKeyElement in foreignKeyValue as ICollection)
                            {
                                pfkeyAdd.Invoke(castToDtoList, new object[] { Convert.ChangeType(dtoForeignKeyElement, elementType) });
                            }        
                            (foreignKey as PropertyInfo).SetValue(dto.InnerObject, castToDtoList);
                            break;
                        case MemberTypes.Field:
                            FieldInfo fieldFKtype = foreignKey as FieldInfo;
                            MethodInfo ffkeyAdd = fieldFKtype.FieldType.GetMethod("Add");
                            castToDtoList = Activator.CreateInstance(fieldFKtype.FieldType);
                            foreach (var dtoForeignKeyElement in foreignKeyValue as ICollection)
                            {
                                ffkeyAdd.Invoke(castToDtoList, new object[] { Convert.ChangeType(dtoForeignKeyElement, elementType) });
                            }
                            (foreignKey as PropertyInfo).SetValue(dto.InnerObject, castToDtoList);
                            break;
                            break;
                    }
                }
                else
                {
                    object fkId = null;
                    for (int i = 0; i < dbo.ColumnNames.Count; i++)
                    {
                        if (dbo.ColumnNames[i] == _helper.GetDataBaseAttribute(foreignKey).DBName)
                        {
                            fkId = dbo.RowValues[i];
                        }

                    }
                    var fkDbo = _repository.Get(fkId, _helper.GetDataBaseAttribute(foreignKeyType).DBName);
                    foreignKeyValue = GetDTOInCohesion(fkDbo, foreignKeyType).InnerObject;
                    switch (foreignKey.MemberType)
                    {
                        case MemberTypes.Property:
                            (foreignKey as PropertyInfo).SetValue(dto.InnerObject, foreignKeyValue);
                            break;
                        case MemberTypes.Field:
                            (foreignKey as FieldInfo).SetValue(dto.InnerObject, foreignKeyValue);
                            break;
                    }
                }
            }
            return dto;
        }

        private DTObject GetDTOFromModel(object item)
        {
            var propertyList = new List<PropertyInfo>(item.GetType().GetProperties())
                .Where(p => _helper.HasAttribute(p, typeof(ColumnAttribute))).ToList();
            var fieldList = new List<FieldInfo>(item.GetType().GetFields())
                .Where(f => _helper.HasAttribute(f, typeof(ColumnAttribute))).ToList();
            return new DTObject()
            {
                InnerObject = item,
                Properties = propertyList,
                Fields = fieldList
            };
        }
    }
}

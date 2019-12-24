using CustomORM.Interfaces;
using CustomORM.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using System.Collections;
using CustomORM.Models.Attributes;

namespace CustomORM.Services
{
    public class ORM<Model> : IORM<Model>
    {
        private IRepository _repository { get; set;}
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
            throw new NotImplementedException();
        }

        public void InsertToDatabase(Model item)
        {
            List<DTObject> cohesionList = new List<DTObject>();
            GetAllModelsInCohesion(item, ref cohesionList);
            foreach(var dto in cohesionList)
            {
                var dbo = _mapper.GetDboFrom(dto);
                _repository.Create(dbo);
            }
        }

        public void UpdateInDatabase(Model item)
        {
            List<DTObject> cohesionList = new List<DTObject>();
            GetAllModelsInCohesion(item, ref cohesionList);
            foreach (var dto in cohesionList)
            {
                var dbo = _mapper.GetDboFrom(dto);
                _repository.Update(dbo);
            }
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
        private void GetAllModelsInCohesion(object item, ref List<DTObject> finalList) //We can work async way
        {
            finalList.Add(GetDTOFromModel(item));
            var foreignKeys = _helper.GetAllForeignKeys(item.GetType());
            foreach(var foreignKey in foreignKeys)
            {
                Type memberAsType = null;
                object element = null;
                switch(foreignKey.MemberType)
                {
                    case MemberTypes.Property:
                        var property = foreignKey as PropertyInfo;
                        memberAsType = property.PropertyType;
                        element = property.GetValue(item);
                        break;
                    case MemberTypes.Field:
                        var field = foreignKey as PropertyInfo;
                        memberAsType = field.PropertyType;
                        element = field.GetValue(item);
                        break;
                }
                if (memberAsType.GetInterfaces().Any(i => i.GetType() == typeof(IEnumerable))) //ToMany
                {
                    //var toManyType = foreignKey.Item1.GetInterfaces().Single(i => i.GetGenericTypeDefinition() == typeof(IEnumerable<>)).GetGenericArguments()[0];
                    foreach (var elementItem in element as IEnumerable)
                    {
                        if (!finalList.Contains(elementItem))
                        {
                            GetAllModelsInCohesion(elementItem, ref finalList);
                        }
                    }
                }
                if (!finalList.Contains(element)) //ToOne
                {
                    GetAllModelsInCohesion(element, ref finalList);
                }
            }
        }
    }
}
 
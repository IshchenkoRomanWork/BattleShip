using CustomORM.Interfaces;
using CustomORM.Models;
using CustomORM.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Linq;
using System.Collections;

namespace CustomORM.Services
{
    internal class Mapper<Model> : IMapper<Model> where Model : new()
    {
        private AttributeHelper _helper;

        internal Mapper()
        {
            _helper = new AttributeHelper();
        }

        DBObject IMapper<Model>.GetDboFrom(DTObject item)
        {
            var tableName = (Attribute.GetCustomAttribute(item.InnerObject.GetType(), typeof(TableAttribute)) as TableAttribute).DBName;
            var nonFKProperties = item.Properties.Where(p => !_helper.HasAttribute(p, typeof(IsForeignKeyAttribute)));
            var nonFKFields = item.Fields.Where(f => !_helper.HasAttribute(f, typeof(IsForeignKeyAttribute)));
            var columnNames = new List<string>();
            var columnDataTypes = new List<SqlDbType>();
            var rowValues = new List<object>();
            object primaryKey = null;

            foreach (var property in nonFKProperties)
            {
                ColumnAttribute colAttrib = (ColumnAttribute)_helper.GetDataBaseAttribute(property);
                rowValues.Add(property.GetValue(item.InnerObject));
                columnNames.Add(colAttrib.DBName);
                columnDataTypes.Add((SqlDbType)Enum.Parse(typeof(SqlDbType),
                    TrimCases(colAttrib.DBDataType), true));
                if(_helper.IsPrimaryKey(property))
                {
                    primaryKey = property.GetValue(item.InnerObject);
                }
            }
            foreach (var field in nonFKFields)
            {
                ColumnAttribute colAttrib = (ColumnAttribute)_helper.GetDataBaseAttribute(field);
                columnNames.Add(colAttrib.DBName);
                columnDataTypes.Add((SqlDbType)Enum.Parse(typeof(SqlDbType),
                   TrimCases(colAttrib.DBDataType), true));
                rowValues.Add(field.GetValue(item.InnerObject));
                if (_helper.IsPrimaryKey(field))
                {
                    primaryKey = field.GetValue(item.InnerObject);
                }
            }
            return new DBObject
            {
                TableName = tableName,
                PrimaryKey = primaryKey,
                ColumnDataTypes = columnDataTypes,
                ColumnNames = columnNames,
                RowValues = rowValues
            };
        }

        DTObject IMapper<Model>.GetDtoFrom(DBObject item, Type type)
        {
            var fullPropertyList = new List<PropertyInfo>(type.GetProperties());
            var fullFieldList = new List<FieldInfo>(type.GetFields());
            var nonFKPropertyList = fullPropertyList.Where(p => _helper.HasAttribute(p, typeof(ColumnAttribute)) && !_helper.HasAttribute(p, typeof(IsForeignKeyAttribute))).ToList();
            var nonFKFieldList = fullFieldList.Where(f => _helper.HasAttribute(f, typeof(ColumnAttribute)) && !_helper.HasAttribute(f, typeof(IsForeignKeyAttribute))).ToList();

            object innerObject = Activator.CreateInstance(type);
            foreach (var property in nonFKPropertyList)
            {
                int index = item.ColumnNames.FindIndex(cName => cName == _helper.GetDataBaseAttribute(property).DBName);
                var value = item.RowValues[index];
                property.SetValue(innerObject, value);
            }
            foreach (var field in nonFKFieldList)
            {
                int index = item.ColumnNames.FindIndex(cName => cName == _helper.GetDataBaseAttribute(field).DBName);
                var value = item.RowValues[index];
                field.SetValue(innerObject, value);
            }
            return new DTObject()
            {
                InnerObject = innerObject,
                Properties = nonFKPropertyList,
                Fields = nonFKFieldList
            };
        }
        internal string TrimCases(string str)
        {
            var chars = str.TakeWhile(c => c != '(');
            return string.Concat(chars);
        }

        

    }
}

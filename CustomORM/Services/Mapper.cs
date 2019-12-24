using CustomORM.Interfaces;
using CustomORM.Models;
using CustomORM.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Linq;

namespace CustomORM.Services
{
    internal class Mapper<Model> : IMapper<Model> where Model : new()
    {
        private AttributeHelper _helper;

        public Mapper()
        {
            _helper = new AttributeHelper();
        }
        DBObject IMapper<Model>.GetDboFrom(DTObject item)
        {
            var tableName = (Attribute.GetCustomAttribute(typeof(Model), typeof(TableAttribute)) as TableAttribute).DBName;
            var columnNames = new List<string>();
            var columnDataTypes = new List<SqlDbType>();
            var rowValues = new List<object>();
            foreach(var property in item.Properties)
            {
                ColumnAttribute colAttrib = (ColumnAttribute)_helper.GetDataBaseAttribute(property);

                columnNames.Add(colAttrib.DBName);
                columnDataTypes.Add((SqlDbType)Enum.Parse(typeof(SqlDbType),
                    TrimCases(colAttrib.DBDataType), true));
                rowValues.Add(property.GetValue(item.InnerObject));
            }
            foreach (var field in item.Fields)
            {
                ColumnAttribute colAttrib = (ColumnAttribute)_helper.GetDataBaseAttribute(field);
                columnNames.Add(colAttrib.DBName);
                columnDataTypes.Add((SqlDbType)Enum.Parse(typeof(SqlDbType),
                   TrimCases(colAttrib.DBDataType), true));
                rowValues.Add(field.GetValue(item.InnerObject));
            }
            return new DBObject()
            {
                TableName = tableName,
                ColumnNames = columnNames,
                ColumnDataTypes = columnDataTypes,
                RowValues = rowValues
            };
        }

        DTObject IMapper<Model>.GetDtoFrom(DBObject item)
        {
            var fullPropertyList = new List<PropertyInfo>(typeof(Model).GetProperties());
            var fullFieldList = new List<FieldInfo>(typeof(Model).GetFields());
            var propertyList = fullPropertyList.Where(p=> _helper.HasAttribute(p, typeof(ColumnAttribute))).ToList();
            var fieldList = fullFieldList.Where(f => _helper.HasAttribute(f, typeof(ColumnAttribute))).ToList();

            var innerObject = new Model();
            foreach(var property in propertyList)
            {
                int index = item.ColumnNames.FindIndex(cName => cName == _helper.GetDataBaseAttribute(property).DBName);
                var value = item.RowValues[index];
                property.SetValue(innerObject, value);
            }
            foreach (var field in fieldList)
            {
                int index = item.ColumnNames.FindIndex(cName => cName == _helper.GetDataBaseAttribute(field).DBName);
                var value = item.RowValues[index];
                field.SetValue(innerObject, value);
            }
            return new DTObject()
            {
                InnerObject = innerObject,
                Properties = propertyList,
                Fields = fieldList
            };
        }
        private string TrimCases(string str)
        {
            var chars = str.TakeWhile(c => c != '(');
            return String.Concat(chars);
        }
    }
}

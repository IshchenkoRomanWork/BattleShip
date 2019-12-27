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
    internal class Mapper<Model> : IMapper<Model> 
    {
        private AttributeHelper _helper;

        internal Mapper()
        {
            _helper = new AttributeHelper();
        }

        DBObject IMapper<Model>.GetDboFrom(DTObject item)
        {
            var dbo = new DBObject();
            dbo.TableName = (Attribute.GetCustomAttribute(item.InnerObject.GetType(), typeof(TableAttribute)) as TableAttribute).DBName;
            var nonFKPropertiesFields = item.PropertiesFields.Where(pf => !_helper.HasAttribute(pf.AsMemberInfo(), typeof(IsForeignKeyAttribute)));

            foreach (var propertyField in nonFKPropertiesFields)
            {
                ColumnAttribute colAttrib = (ColumnAttribute)_helper.GetDataBaseAttribute(propertyField.AsMemberInfo());
                dbo.Add(propertyField.GetValue(item.InnerObject), colAttrib.DBName, _helper.ParseToSqlDbType(colAttrib.DBDataType));
                if(_helper.IsPrimaryKey(propertyField))
                {
                    dbo.PrimaryKey = propertyField.GetValue(item.InnerObject);
                }
            }
            //var def = this.GetType().GetMethod("GetDefaultGeneric").MakeGenericMethod(dbo.PrimaryKey.GetType()).Invoke(this, null); 
            //if (dbo.PrimaryKey == default)
            //{
            //    throw new Exception("Primary Key isn't set");
            //}
            if (item.InnerObject.GetType() != item.BaseType)
            {
                dbo.Add(item.InnerObject.GetType().FullName, "Discriminator", SqlDbType.NVarChar);
            }
            return dbo;
        }

        DTObject IMapper<Model>.GetDtoFrom(DBObject item, Type type)
        {
            List<IPropertyFieldInfo> propertyFields = _helper.GetPropertyFieldList(type).ToList();
            var nonFkPropertyFields = propertyFields.Where(pf => !_helper.HasAttribute(pf.AsMemberInfo(), typeof(IsForeignKeyAttribute))).ToList();

            string realType = (string)item.GetValueByColumnName("Discriminator");
            if (realType != null)
            {
                string assemblyName = realType.Split('.')[0];
                type = Assembly.Load(assemblyName).GetType(realType);
            }
            object innerObject = Activator.CreateInstance(type);
            foreach (var propertyField in nonFkPropertyFields)
            {
                int index = item.ColumnNames.FindIndex(cName => cName == _helper.GetDataBaseAttribute(propertyField.AsMemberInfo()).DBName);
                var value = item.RowValues[index];
                propertyField.SetValue(innerObject, value);
            }
            return new DTObject()
            {
                InnerObject = innerObject,
                PropertiesFields = nonFkPropertyFields
            };
        }
    }
}

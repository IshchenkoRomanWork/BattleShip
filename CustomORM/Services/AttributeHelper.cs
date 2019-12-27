using CustomORM.Interfaces;
using CustomORM.Models;
using CustomORM.Models.Abstract;
using CustomORM.Models.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace CustomORM.Services
{
    internal class AttributeHelper
    {
        internal DataBaseAttribute GetDataBaseAttribute(MemberInfo info)
        {
            if (HasAttribute(info, typeof(DataBaseAttribute)))
            {
                return Attribute.GetCustomAttribute(info, typeof(DataBaseAttribute)) as DataBaseAttribute;
            }
            return null;
        }
        internal bool HasAttribute(MemberInfo info, Type attributeType)
        {
            return info.GetCustomAttribute(attributeType, true) != null;
        }
        internal bool IsPrimaryKey(IPropertyFieldInfo info)
        {
            var attributes = info.AsMemberInfo().GetCustomAttributes();
            foreach (var attrib in attributes)
            {
                if (attrib.GetType() == typeof(ColumnAttribute) && ((ColumnAttribute)attrib).IsPrimaryKey)
                    return true;
            }
            return false;
        }
        internal List<IPropertyFieldInfo> GetAllForeignKeys(Type type)
        {
            var foreignKeys = new List<IPropertyFieldInfo>();
            var propertyFields = GetPropertyFieldList(type);
            foreach (var propertyField in propertyFields)
            {
                var foreignKeyAttributeList = propertyField.AsMemberInfo().GetCustomAttributes(typeof(IsForeignKeyAttribute), true);
                if (foreignKeyAttributeList.Length != 0)
                {
                    foreignKeys.Add(propertyField);
                }
            }
            return foreignKeys;
        }
        public bool IsToMany(IPropertyFieldInfo foreignKey)
        {
            Type memberAsType = foreignKey.GetObjectType();
            var ifaces = memberAsType.GetInterfaces();
            bool isenum = ifaces.Any(i => i == typeof(ICollection));
            return isenum;
        }
        public object GetId(object model)
        {
            var type = model.GetType();
            var propertiesFields = GetPropertyFieldList(type);
            foreach (var propertyField in propertiesFields)
            {
                if (IsPrimaryKey(propertyField))
                {
                    return propertyField.GetValue(model);
                }
            }
            return null;
        }
        internal string TrimCases(string str)
        {
            var chars = str.TakeWhile(c => c != '(');
            return String.Concat(chars);
        }
        internal IEnumerable<IPropertyFieldInfo> GetPropertyFieldList(Type type)
        {
            var propertyList = new List<PropertyInfo>(type.GetProperties())
                .Where(p => HasAttribute(p, typeof(ColumnAttribute))).ToList();
            var fieldList = new List<FieldInfo>(type.GetFields())
                .Where(f => HasAttribute(f, typeof(ColumnAttribute))).ToList();
            List<IPropertyFieldInfo> propertyFields = new List<IPropertyFieldInfo>();
            propertyFields.AddRange(propertyList.Select(p => new PropertyFieldInfo(p)));
            propertyFields.AddRange(fieldList.Select(f => new PropertyFieldInfo(f)));
            return propertyFields;
        }
        internal SqlDbType ParseToSqlDbType(string dBType)
        {
            return (SqlDbType)Enum.Parse(typeof(SqlDbType), TrimCases(dBType), true);
        }


    }
}

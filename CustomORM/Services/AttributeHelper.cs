using CustomORM.Models.Abstract;
using CustomORM.Models.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
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
            return info.GetCustomAttribute(attributeType, false) != null;
        }
        internal bool IsPrimaryKey(MemberInfo info)
        {
            var attributes = info.GetCustomAttributes();
            foreach (var attrib in attributes)
            {
                if (attrib.GetType() == typeof(ColumnAttribute) && ((ColumnAttribute)attrib).IsPrimaryKey)
                    return true;
            }
            return false;
        }
        internal List<MemberInfo> GetAllForeignKeys(Type type)
        {
            var foreignKeys = new List<MemberInfo>();
            var properties = new List<PropertyInfo>(type.GetProperties());
            var fields = new List<FieldInfo>(type.GetFields());
            foreach (var property in properties)
            {
                var foreignKeyAttributeList = property.GetCustomAttributes(typeof(IsForeignKeyAttribute), false);
                if (foreignKeyAttributeList.Length != 0)
                {
                    foreignKeys.Add(property);
                }
            }
            foreach (var field in fields)
            {
                var foreignKeyAttributeList = field.GetCustomAttributes(typeof(IsForeignKeyAttribute), false);
                if (foreignKeyAttributeList.Length != 0)
                {
                    foreignKeys.Add(field);
                }
            }
            return foreignKeys;
        }

        public bool IsToMany(MemberInfo foreignKey)
        {
            Type memberAsType = null;
            switch (foreignKey.MemberType)
            {
                case MemberTypes.Property:
                    var property = foreignKey as PropertyInfo;
                    memberAsType = property.PropertyType;
                    break;
                case MemberTypes.Field:
                    var field = foreignKey as PropertyInfo;
                    memberAsType = field.PropertyType;
                    break;
            }
            var ifaces = memberAsType.GetInterfaces();
            bool isenum = ifaces.Any(i => i == typeof(ICollection));
            return isenum;
        }

        public object GetId(object model)
        {
            var type = model.GetType();
            var properties = type.GetProperties();
            var fields = type.GetFields();
            foreach (var property in properties)
            {
                if (IsPrimaryKey(property))
                {
                    return property.GetValue(model);
                }
            }
            foreach (var field in fields)
            {
                if (IsPrimaryKey(field))
                {
                    return field.GetValue(model);
                }
            }
            return null;
        }

        internal string TrimCases(string str)
        {
            var chars = str.TakeWhile(c => c != '(');
            return String.Concat(chars);
        }
    }
}

using CustomORM.Models;
using CustomORM.Models.Abstract;
using CustomORM.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

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
    }
}

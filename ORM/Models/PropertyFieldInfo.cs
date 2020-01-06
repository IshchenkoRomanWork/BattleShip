using ORM.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ORM.Models
{
    class PropertyFieldInfo : IPropertyFieldInfo
    {
        private PropertyInfo propertyInfo;
        private FieldInfo fieldInfo;
        private MemberTypes memberType;
        internal PropertyFieldInfo(MemberInfo info)
        {
            switch (info.MemberType)
            {
                case MemberTypes.Property:
                    memberType = MemberTypes.Property;
                    propertyInfo = info as PropertyInfo;
                    break;
                case MemberTypes.Field:
                    memberType = MemberTypes.Field;
                    fieldInfo = info as FieldInfo;
                    break;
            }
        }

        Type IPropertyFieldInfo.GetObjectType()
        {
            switch (memberType)
            {
                case MemberTypes.Property:
                    return propertyInfo.PropertyType;
                case MemberTypes.Field:
                    return fieldInfo.FieldType;
            }
            return null;
        }

        void IPropertyFieldInfo.SetValue(object objecToSet, object value)
        {
            switch (memberType)
            {
                case MemberTypes.Property:
                    propertyInfo.SetValue(objecToSet, value);
                    break;
                case MemberTypes.Field:
                    fieldInfo.SetValue(objecToSet, value);
                    break;
            }
        }

        object IPropertyFieldInfo.GetValue(object objecToGetFrom)
        {
            switch (memberType)
            {
                case MemberTypes.Property:
                    return propertyInfo.GetValue(objecToGetFrom);
                case MemberTypes.Field:
                    return fieldInfo.GetValue(objecToGetFrom);
            }
            return null;
        }

        MemberInfo IPropertyFieldInfo.AsMemberInfo()
        {
            switch (memberType)
            {
                case MemberTypes.Property:
                    return propertyInfo;
                case MemberTypes.Field:
                    return fieldInfo;
            }
            return null;
        }
    }
}

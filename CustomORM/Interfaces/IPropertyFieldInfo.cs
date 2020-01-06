using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ORM.Interfaces
{
    interface IPropertyFieldInfo
    {
        internal Type GetObjectType();
        internal void SetValue(object objecToSet, object value);
        internal object GetValue(object objectToGetFrom);
        internal MemberInfo AsMemberInfo();
    }
}

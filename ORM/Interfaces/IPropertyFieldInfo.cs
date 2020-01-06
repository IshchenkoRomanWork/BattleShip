using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ORM.Interfaces
{
    interface IPropertyFieldInfo
    {
        Type GetObjectType();
        void SetValue(object objecToSet, object value);
        object GetValue(object objectToGetFrom);
        MemberInfo AsMemberInfo();
    }
}

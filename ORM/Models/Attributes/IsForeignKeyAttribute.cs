using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class IsForeignKeyAttribute : Attribute
    {
       public IsForeignKeyAttribute()
        {
        }
    }
}

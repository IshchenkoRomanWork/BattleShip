using System;
using System.Collections.Generic;
using System.Text;

namespace CustomORM.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class IsForeignKeyAttribute : Attribute
    {
       internal string OriginalTable { get; set; }
       public IsForeignKeyAttribute(string originalTable)
        {
            OriginalTable = originalTable;
        }
    }
}

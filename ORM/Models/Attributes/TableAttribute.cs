using ORM.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class TableAttribute : DataBaseAttribute
    {
        public TableAttribute(string tableName) : base(tableName)
        {
        }
    }
}

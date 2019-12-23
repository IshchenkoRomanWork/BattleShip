using CustomORM.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomORM.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class TableAttribute : DataBaseAttribute
    {
        public TableAttribute(string tableName) : base(tableName)
        {
        }
    }
}

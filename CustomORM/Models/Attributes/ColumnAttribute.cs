using CustomORM.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomORM.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class IsColumnAttribute : DataBaseAttribute
    {
        internal string DBDataType { get; set; }

        public IsColumnAttribute(string columnName, string dbDataType) : base(columnName)
        {
            DBDataType = dbDataType;
        }
    }
}

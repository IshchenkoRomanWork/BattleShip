using CustomORM.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomORM.Models.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ColumnAttribute : DataBaseAttribute
    {
        internal string DBDataType { get; set; }
        internal bool IsPrimaryKey { get; set; }

        public ColumnAttribute(string columnName, string dbDataType, bool isPrimaryKey) : base(columnName)
        {
            DBDataType = dbDataType;
            IsPrimaryKey = isPrimaryKey;
        }
        public ColumnAttribute(string columnName, string dbDataType) : this(columnName, dbDataType, false)
        {
        }
    }
}

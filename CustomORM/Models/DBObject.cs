using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CustomORM.Models
{
    class DBObject
    {
        internal string TableName { get; set; }
        internal object PrimaryKey { get; set; }
        internal List<object> RowValues { get; set; }
        internal List<string> ColumnNames { get; set; }
        internal List<SqlDbType> ColumnDataTypes { get; set; }

        public DBObject()
        {
            RowValues = new List<object>();
            ColumnNames = new List<string>();
            ColumnDataTypes = new List<SqlDbType>();
        }
    }
}

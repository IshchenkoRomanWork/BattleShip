using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ORM.Models
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

        internal void Add(object rowValue, string columnName, SqlDbType columnDataType)
        {

            RowValues.Add(rowValue);
            ColumnNames.Add(columnName);
            ColumnDataTypes.Add(columnDataType);
        }
        internal object GetValueByColumnName(string columnName)
        {
            for(int i = 0; i< ColumnNames.Count; i++)
            {
                if (ColumnNames[i] == columnName)
                    return RowValues[i];
            }
            return null;
        }

    }
}

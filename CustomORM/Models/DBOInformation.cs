using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CustomORM.Models
{
    class DBOInformation
    {
        internal string TableName { get; set; }
        internal List<string> ColumnNames { get; set; }
        internal List<SqlDbType> DataTypes { get; set; }
    }
}

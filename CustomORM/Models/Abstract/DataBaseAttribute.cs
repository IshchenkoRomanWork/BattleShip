using System;
using System.Collections.Generic;
using System.Text;

namespace CustomORM.Models.Abstract
{
    public abstract class DataBaseAttribute : Attribute
    {
        internal string DBName { get; set; }

        public DataBaseAttribute(string dbName)
        {
            DBName = dbName;
        }
    }

}

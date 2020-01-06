using ORM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Interfaces
{
    internal interface IRepository
    {
        internal void Create(DBObject dBObject);
        internal DBObject Get(object id, string tablename);
        internal void Update(DBObject dBObject);
        internal void Delete(object id, string tableName);
        internal List<DBObject> GetAll(string tableName);
        internal bool Exists(DBObject dBObject);

        internal List<DBObject> GetForeignKeyValues(string firstTableName, object foreignKeyValue, string secondTableName, bool toMany);


    }
}

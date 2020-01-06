using ORM.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Interfaces
{
    internal interface IRepository
    {
        void Create(DBObject dBObject);
        DBObject Get(object id, string tablename);
        void Update(DBObject dBObject);
        void Delete(object id, string tableName);
        List<DBObject> GetAll(string tableName);
        bool Exists(DBObject dBObject);

        List<DBObject> GetForeignKeyValues(string firstTableName, object foreignKeyValue, string secondTableName, bool toMany);


    }
}

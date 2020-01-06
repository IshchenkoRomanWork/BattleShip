using System;
using System.Collections.Generic;
using System.Text;

namespace ORM.Interfaces
{
    public interface IORM<Model>
    {
        void InsertToDatabase(Model item);
        Model GetFromDatabase(object id);
        void UpdateInDatabase(Model item);
        void DeleteFromDatabase(object id);
        IEnumerable<Model> GetAllFromDatabase();
    }
}

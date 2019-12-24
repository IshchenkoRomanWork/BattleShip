using System;
using System.Collections.Generic;
using System.Text;

namespace CustomORM.Interfaces
{
    public interface IORM<Model> where Model : new()
    {
        public void InsertToDatabase(Model item);
        public Model GetFromDatabase(object id);
        public void UpdateInDatabase(Model item);
        public void DeleteFromDatabase(object id);
    }
}

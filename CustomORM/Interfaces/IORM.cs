using System;
using System.Collections.Generic;
using System.Text;

namespace CustomORM.Interfaces
{
    interface IORM<Model>
    {
        public void InsertToDatabase(Model item);
        public Model GetFromDatabase(object id);
        public void UpdateInDatabase(Model item);
        public void DeleteFromDatabase(Model item);
    }
}

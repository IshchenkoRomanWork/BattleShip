using CustomORM.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomORM.Services
{
    public class ORM<Model> : IORM<Model>
    {
        private IRepository _repository { get; set;}

        public void DeleteFromDatabase(Model item)
        {
            throw new NotImplementedException();
        }

        public Model GetFromDatabase(object id)
        {
            throw new NotImplementedException();
        }

        public void InsertToDatabase(Model item)
        {
            throw new NotImplementedException();
        }

        public void UpdateInDatabase(Model item)
        {
            throw new NotImplementedException();
        }
    }
}

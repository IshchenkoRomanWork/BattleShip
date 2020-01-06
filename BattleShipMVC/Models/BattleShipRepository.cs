using BattleShipMVC.Interfaces;
using ORM.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BattleShipMVC.Models
{
    public class BattleShipRepository<Entity> : IBattleShipRepository<Entity>
    {
        IORM<Entity> _orm;
        public BattleShipRepository(IORM<Entity> orm)
        {
            _orm = orm;
        }
        public void Create(Entity item)
        {
            _orm.InsertToDatabase(item);
        }

        public void Delete(object id)
        {
            _orm.DeleteFromDatabase(id);
        }

        public Entity Get(object id)
        {
            return _orm.GetFromDatabase(id);
        }

        public ICollection<Entity> GetAll()
        {
            return _orm.GetAllFromDatabase().ToList();
        }

        public void Update(Entity item)
        {
            _orm.UpdateInDatabase(item);
        }
    }
}
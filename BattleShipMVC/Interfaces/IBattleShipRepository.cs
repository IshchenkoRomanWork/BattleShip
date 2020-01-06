using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShipMVC.Interfaces
{
    public interface IBattleShipRepository<Entity> 
    {
        ICollection<Entity> GetAll();
        void Create(Entity item);
        Entity Get(object id);
        void Update(Entity item);
        void Delete(object id);
    }
}

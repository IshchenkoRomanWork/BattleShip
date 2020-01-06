using BattleShipGame.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShipGame.Services
{
    class UnitOfWork
    {
        public Map Map { get; set; }

        public UnitOfWork()
        {
            Map = new Map(100);
        }    
    }
}

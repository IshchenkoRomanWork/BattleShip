using BattleShip.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.Interfaces
{
    interface IWarShip
    {
        public bool Shot(Ship target);
    }
}

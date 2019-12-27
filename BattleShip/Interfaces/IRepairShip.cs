using BattleShip.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.Interfaces
{
    interface IRepairShip
    {
        public bool Repair(Ship target);
    }
}

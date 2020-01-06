using BattleShipGame.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShipGame.Interfaces
{
    interface IRepairShip
    {
        bool Repair(Ship target);
    }
}

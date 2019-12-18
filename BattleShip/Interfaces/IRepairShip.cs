using BattleShip.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.Interfaces
{
    interface IRepairShip : IActionRange
    {
        public int RepairAmount { get; set; }
        public bool Repair(Ship target);
    }
}

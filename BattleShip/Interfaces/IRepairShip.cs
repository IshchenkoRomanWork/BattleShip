using BattleShip.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.Interfaces
{
    interface IRepairShip
    {
        public int ActionRange { get; set; }
        public int RepairAmount { get; set; }
        public bool Repair(Ship target);
    }
}

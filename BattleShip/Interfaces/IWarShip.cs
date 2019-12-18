using BattleShip.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.Interfaces
{
    interface IWarShip : IActionRange
    {
        public int ShotDamage { get; set; }
        public bool Shot(Ship target);
    }
}

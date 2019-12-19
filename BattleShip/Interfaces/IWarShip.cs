using BattleShip.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.Interfaces
{
    interface IWarShip
    {
        public int ActionRange { get; set; }
        public int ShotDamage { get; set; }
        public bool Shot(Ship target);
    }
}

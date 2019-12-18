using BattleShip.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.Models
{
    class WarShip : Ship, IWarShip
    {
        public int ActionRange { get; set; }
        public int ShotDamage { get; set; }

        public bool Shot(Ship target)
        {
            throw new NotImplementedException();
        }

        public WarShip(Coords coords, int speed, int hitPoints, int actionRange, int shotDamage) : base(coords, speed, hitPoints)
        {
            ActionRange = actionRange;
            ShotDamage = shotDamage;
        }

        public override string GetState()
        {
            return base.GetState();
        }
    }
}

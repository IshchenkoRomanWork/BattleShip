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

        public WarShip(int length, int speed, int hitPoints, int actionRange, int shotDamage) : base(length, speed, hitPoints)
        {
            ActionRange = actionRange;
            ShotDamage = shotDamage;
        }

        public WarShip()
        {
        }

        public bool Shot(Ship target)
        {
            throw new NotImplementedException();
        }


        public override string ToString()
        {
            StringBuilder sBuilder = new StringBuilder();
            sBuilder.Append(String.Format("This is War Type Ship \n"));
            sBuilder.Append(base.ToString());
            return sBuilder.ToString();
        }
    }
}

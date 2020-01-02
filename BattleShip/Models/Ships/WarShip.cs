namespace BattleShip.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using BattleShip.Interfaces;

    internal class WarShip : Ship, IWarShip
    {
        public WarShip(int length, int speed, int hitPoints, int actionRange)
            : base(length, speed, hitPoints, actionRange)
        {
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
            sBuilder.Append(string.Format("This is War Type Ship \n"));
            sBuilder.Append(base.ToString());
            return sBuilder.ToString();
        }
    }
}

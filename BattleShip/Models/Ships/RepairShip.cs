using BattleShip.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.Models
{
    public class RepairShip : Ship, IRepairShip
    {
        public RepairShip(int length, int speed, int hitPoints, int actionRange) : base(length, speed, hitPoints, actionRange)
        {
            ActionRange = actionRange;
        }

        public RepairShip()
        {
        }

        public bool Repair(Ship target)
        {
            throw new NotImplementedException();
        }



        public override string ToString()
        {
            StringBuilder sBuilder = new StringBuilder();
            sBuilder.Append(String.Format("This is Repair Type Ship \n"));
            sBuilder.Append(base.ToString());
            return sBuilder.ToString();
        }
    }
}

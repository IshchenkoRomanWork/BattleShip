using BattleShip.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.Models
{
    public class RepairShip : Ship, IRepairShip
    {
        public int ActionRange { get; set; }
        public int RepairAmount { get; set ; }

        public RepairShip(int length, int speed, int hitPoints, int actionRange, int repairAmount) : base(length, speed, hitPoints)
        {
            ActionRange = actionRange;
            RepairAmount = repairAmount;
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

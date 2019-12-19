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

        public bool Repair(Ship target)
        {
            throw new NotImplementedException();
        }

        public RepairShip(Coords coords, int speed, int hitPoints, int actionRange, int repairAmount) : base(coords, speed, hitPoints)
        {
            ActionRange = actionRange;
            RepairAmount = repairAmount;
        }

        public override string GetState()
        {
            StringBuilder sBuilder = new StringBuilder();
            sBuilder.Append("This is Repair Type Ship \n");
            sBuilder.Append(base.GetState());
            return sBuilder.ToString();
        }
    }
}

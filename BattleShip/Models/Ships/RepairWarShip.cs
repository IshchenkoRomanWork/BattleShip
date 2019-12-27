using BattleShip.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.Models
{
    class RepairWarShip : Ship, IWarShip, IRepairShip //Mixed or Universal Ship, this naming comes from possible expansion
    {

        public RepairWarShip(int length, int speed, int hitPoints, int actionRange) : base(length, speed, hitPoints, actionRange)
        {
            ActionRange = actionRange;
        }

        public RepairWarShip()
        {
        }

        public bool Repair(Ship target)
        {
            throw new NotImplementedException();
        }

        public bool Shot(Ship target)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            StringBuilder sBuilder = new StringBuilder();
            sBuilder.Append(string.Format("This is Mixed Type Ship \n"));
            sBuilder.Append(base.ToString());
            return sBuilder.ToString();
        }
    }
}

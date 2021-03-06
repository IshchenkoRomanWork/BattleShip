﻿using BattleShip.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.Models
{
    class RepairWarShip : Ship, IWarShip, IRepairShip //Mixed or Universal Ship, this naming comes from possible expansion
    {
        public int ActionRange { get; set ; }
        public int ShotDamage { get; set; }
        public int RepairAmount { get; set; }

        public bool Repair(Ship target)
        {
            throw new NotImplementedException();
        }

        public bool Shot(Ship target)
        {
            throw new NotImplementedException();
        }

        public RepairWarShip(Coords coords, int speed, int hitPoints, int actionRange, int repairAmount, int shotDamage) : base(coords, speed, hitPoints)
        {
            ActionRange = actionRange;
            RepairAmount = repairAmount;
            ShotDamage = shotDamage;
        }

        public override string GetState()
        {
            StringBuilder sBuilder = new StringBuilder();
            sBuilder.Append("This is Mixed Type Ship \n");
            sBuilder.Append(base.GetState());
            return sBuilder.ToString();
        }
    }
}

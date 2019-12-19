using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace BattleShip.Models
{
    public abstract class Ship : IEquatable<Ship>
    {
        public int Length { get; set; }
        public int Speed { get; set; }
        public int HitPoints { get; set; }

        public Ship(int length, int speed, int hitPoints)
        {
            Length = length;
            Speed = speed;  
            HitPoints = hitPoints;
        }
        public override string ToString()
        {
            StringBuilder sBuilder = new StringBuilder();
            sBuilder.Append(String.Format("This ship Length is {0} \n", Length));
            sBuilder.Append(String.Format("This ships speed is {0} \n", Speed));
            sBuilder.Append(String.Format("This ships hitPoints is {0} \n", HitPoints));
            sBuilder.Append(String.Format("This ships Length is {0} \n", Length));
            return sBuilder.ToString();
        }

        public static bool operator ==(Ship firstShip, Ship secondShip)
        {
            return firstShip.Equals(secondShip);
        }

        public static bool operator !=(Ship firstShip, Ship secondShip)
        {
            return !(firstShip == secondShip);
        }

        public bool Equals([AllowNull] Ship other)
        {
            bool typesAreEqual = this.GetType() == other.GetType();
            bool speedAreEqual = this.Speed == other.Speed;
            bool lengthAreEqual = this.Length == other.Length;
            return typesAreEqual && speedAreEqual && lengthAreEqual;
            //Also we can use "if" to stop calculation after first inequality
        }




    }
}

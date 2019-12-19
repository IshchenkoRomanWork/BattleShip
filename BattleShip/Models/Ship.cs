using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.Models
{
    public abstract class Ship
    {
        public Coords Coords { get; set; }
        public int Speed { get; set; }
        public int HitPoints { get; set; }

        public Ship(Coords coords, int speed, int hitPoints)
        {
            Coords = coords;
            Speed = speed;
            hitPoints = HitPoints;
        }
        public virtual string GetState()
        {
            StringBuilder sBuilder = new StringBuilder();
            sBuilder.Append(($"This ship is located at X: {0}, Y: {1} \n", Coords.Head.Item1, Coords.Head.Item2));
            sBuilder.Append(($"This ships speed is {0} \n", Speed));
            sBuilder.Append(($"This ships hitPoints is {0} \n", HitPoints));
            sBuilder.Append(($"This ships Length is {0} \n", GetLength(this)));
            return sBuilder.ToString();
        }

        public static bool operator ==(Ship firstShip, Ship secondShip)
        {
            bool typesAreEqual = firstShip.GetType() == secondShip.GetType();
            bool speedAreEqual = firstShip.Speed == secondShip.Speed;
            bool lengthAreEqual = GetLength(firstShip) == GetLength(secondShip);
            return typesAreEqual && speedAreEqual && lengthAreEqual;
            //Also we can use "if" to stop calculation after first inequality
        }

        private static int GetLength(Ship ship)
        {
            int value = (ship.Coords.Head.Item1 - ship.Coords.Stern.Item1)
                + (ship.Coords.Head.Item2 - ship.Coords.Stern.Item2);
            return Math.Abs(value);
        }

        public static bool operator !=(Ship firstShip, Ship secondShip)
        {
            return !(firstShip == secondShip);
        }


    }
}

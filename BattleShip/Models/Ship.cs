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
            return default(string);
        }

        public static bool operator ==(Ship firstShip, Ship secondShip)
        {
            return default(bool);
        }

        public static bool operator !=(Ship firstShip, Ship secondShip)
        {
            return default(bool);
        }


    }
}

namespace BattleShip.Models
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Text;
    using ORM.Models.Attributes;

    [Table("Ship")]
    public abstract class Ship : IEquatable<Ship>
    {
        [Column("ActionRange", "int")]
        public int ActionRange { get; set; }

        private int _length;

        [Column("Length", "int")]
        public int Length
        {
            get
            {
                return _length;
            }
            set
            {
                _length = value;
                EventHandler handler = LengthChanged;
                handler?.Invoke(this, null);
            }
        }

        public event EventHandler LengthChanged;

        [Column("Speed", "int")]    
        public int Speed { get; set; }

        [Column("HitPoints", "int")]
        public int HitPoints { get; set; }

        [Column("ShipId", "int", true)]
        public int Id { get; private set; }

        public Ship(int length, int speed, int hitPoints, int actionRange)
        {
            if (length < 1)
                throw new Exception("Length of Ship can't be lesser than one");
            Length = length;
            if (speed < 0)
                throw new Exception("Speed can't be negative");
            Speed = speed;
            if (hitPoints < 0)
                throw new Exception("Hit points can't be negative");
            HitPoints = hitPoints;
            if (speed < 0)
                throw new Exception("Action Range can't be negative");
            ActionRange = actionRange;
            Id = new Random().Next();
        }

        public Ship() 
        {
        }
        public override string ToString()
        {
            StringBuilder sBuilder = new StringBuilder();
            sBuilder.Append(string.Format("This ships Length is {0} \n", Length));
            sBuilder.Append(String.Format("This ships speed is {0} \n", Speed));
            sBuilder.Append(String.Format("This ships hitPoints is {0} \n", HitPoints));
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

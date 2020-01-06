using ORM.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShipGame.Models
{
    [Table("ShipLocation")]
    public class ShipLocation
    {
        [Column("Direction","int")]
        public Direction Direction { get; set; }
        [Column("CoordX", "int")]
        public int CoordX { get; set; }
        [Column("CoordY", "int")]
        public int CoordY { get; set; }
        [Column("SLID", "int", true)]
        public int Id { get; private set; }

        public ShipLocation(Direction dir, (int, int) coords)
        {
            Direction = dir;
            CoordX = coords.Item1;
            CoordY = coords.Item2;
            Id = new Random().Next();
        }
        public ShipLocation()
        {
        }
    }
}

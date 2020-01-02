using CustomORM.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.Models
{
    [Table("ShipInformation")]
    public class ShipInformation
    {
        [IsForeignKey]
        [Column("SLID", "int")]
        public ShipLocation ShipLocation { get; set; }
        [IsForeignKey]
        [Column("ShipID", "int")]
        public Ship Ship {get; set;}
        [Column("SIID", "int", true)]
        public int Id { get; private set; }

        public ShipInformation(ShipLocation location, Ship ship)
        {
            ShipLocation = location;
            Ship = ship;
            Id = new Random().Next();
        }

        public ShipInformation()
        {

        }
    }
}

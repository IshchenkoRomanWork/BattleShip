using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.Models
{
    public struct ShipLocation
    {
        public Direction Direction { get; set; }
        public (int, int) Coords { get; set; }

        public ShipLocation(Direction dir, (int, int) coords)
        {
            Direction = dir;
            Coords = coords;
        }
    }
}

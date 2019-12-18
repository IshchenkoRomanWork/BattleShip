using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.Models
{
    public class Map
    {
        public Dictionary<Coords, Ship> AllShips { get; set; }

        public bool AddShip(Ship ship)
        {
            return default(bool);
        }

        public string GetState()
        {
            return default(string);
        }

        public bool Move(Direction direction, Ship ship)
        {
            return default(bool);
        }

        private bool CheckPositionsAreFree(Coords checkedCoords)
        {
            return default(bool);
        }
    }
}

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
            if (!CheckPositionsAreFree(ship.Coords))
                return false;
            AllShips.Add(ship.Coords, ship);
            return true;
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
            //Direction direction = checkedCoords.Head.Item1 != checkedCoords.Stern.Item1 ? Direction.Horizontal : Direction.Vertical;
        }

        private bool CheckPositionAreFree((int, int) coord)
        {
            return default(bool);
            //foreach(KeyValuePair<Coords, Ship> pair in AllShips)
            //{

            //}
        }

        public Ship this[(int, int) coord]
        {
            get
            {
                return default(Ship);
            }
            set
            {

            }
        }

    }
}

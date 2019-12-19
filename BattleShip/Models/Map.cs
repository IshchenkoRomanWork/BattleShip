using BattleShip.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.Models
{
    public class Map
    {
        private List<(ShipLocation, Ship)> _allShips;

        private List<(int, int)> _occupiedCoords;

        public Map()
        {
            _allShips = new List<(ShipLocation, Ship)>();
            _occupiedCoords = new List<(int, int)>();
        }

        public bool AddShip(Ship ship, ShipLocation startingLocation)
        {
            var coords = GetAllCoordsInSection(startingLocation.Direction, ship.Length, startingLocation.Coords);

            if (CheckPositionsAreFree(coords))
            {
                _allShips.Add((startingLocation, ship));
                _occupiedCoords.AddRange(coords);
            }
            else
            {
                return false;
            }

            return true;
        }


        public Ship this[int quadrant, int xCoord, int yCoord]
        {
            get
            {
                if (quadrant < 1 || quadrant > 4)
                    throw new Exception("Quadrans can range only from 1 to 4");
                if (xCoord < 0 || yCoord < 0)
                    throw new Exception("Coords cant be less than zero");

                switch (quadrant)
                {
                    case 2:
                        xCoord = -xCoord;
                        break;
                    case 3:
                        xCoord = -xCoord;
                        yCoord = -yCoord;
                        break;
                    case 4:
                        yCoord = -yCoord;
                        break;
                }

                foreach (var pair in _allShips)
                {
                    if (pair.Item1.Coords == (xCoord, yCoord))
                        return pair.Item2;
                }

                throw new Exception("Ship is not Found on these coordinates");
            }
        }

        public override string ToString()
        {
            _allShips.Sort(new ShipComparer());

            StringBuilder sBuilder = new StringBuilder();
            foreach (var pair in _allShips)
            {
                sBuilder.Append(pair.Item2.ToString());
                sBuilder.Append("\n");
            }

            return sBuilder.ToString();
        }

        private bool CheckPositionsAreFree(List<(int, int)> checkedCoords)
        {
            foreach (var coord in checkedCoords)
            {
                if (_occupiedCoords.Contains(coord))
                    return false;
            }
            return true;
        }

        private List<(int, int)> GetAllCoordsInSection(Direction direction, int length, (int, int) startingCoord)
        {
            var sectionCoords = new List<(int, int)>();
            sectionCoords.Add(startingCoord);

            int headX = startingCoord.Item1;
            int headY = startingCoord.Item1;

            for (int i = 1; i <= length; i++)
            {
                switch (direction)
                {
                    case Direction.Right:
                        sectionCoords.Add((headX + i, headY));
                        break;
                    case Direction.Left:
                        sectionCoords.Add((headX - i, headY));
                        break;
                    case Direction.Down:
                        sectionCoords.Add((headX, headY + 1));
                        break;
                    case Direction.Up:
                        sectionCoords.Add((headX, headY - 1));
                        break;
                }
            }
            return sectionCoords;
        }

    }
}

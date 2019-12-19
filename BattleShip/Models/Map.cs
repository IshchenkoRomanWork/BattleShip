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

        private int _quadrantSize;

        public Map(int quadrantSize)
        {
            _allShips = new List<(ShipLocation, Ship)>();
            _occupiedCoords = new List<(int, int)>();
            _quadrantSize = quadrantSize;
        }

        public void AddShip(Ship ship, ShipLocation startingLocation)
        {
            var coords = GetAllCoordsInSection(startingLocation.Direction, ship.Length, startingLocation.Coords);

            ValidateCoords(coords);

            _allShips.Add((startingLocation, ship));
            _occupiedCoords.AddRange(coords);
        }

        private void ValidateCoords(List<(int, int)> validationCoords)
        {
            //There's a reason to make validation interface and validation list
            if(!CoordIsNotOnAxis(validationCoords[0]))
            {
                throw new Exception("Start coord can't lie on axis");
            }
            if (!InQuadrantSize(validationCoords))
            {
                throw new Exception("Coords can't be ot of quadrant size");
            }
            bool coordsAreFree = CheckCoordsAreFree(validationCoords);
            if (!CheckCoordsAreFree(validationCoords))
                {
                    throw new Exception("There's another ship on this coordinates");
                }
        }

        private bool InQuadrantSize(List<(int, int)> checkedCoords)
        {
            foreach(var coord in checkedCoords)
            {
                if (Math.Abs(coord.Item1) > _quadrantSize || Math.Abs(coord.Item2) > _quadrantSize)
                    return false;
            }
            return true;
        } //Validation Check

        private bool CheckCoordsAreFree(List<(int, int)> checkedCoords)
        {
            foreach (var coord in checkedCoords)
            {
                if (_occupiedCoords.Contains(coord))
                    return false;
            }
            return true;
        } //Validation Check
        private bool CoordIsNotOnAxis((int, int) checkedCoord)
        {
            return checkedCoord.Item1 != 0 && checkedCoord.Item2 != 0;
        } //Validation Check

        private List<(int, int)> GetAllCoordsInSection(Direction direction, int length, (int, int) startingCoord)
        {
            var sectionCoords = new List<(int, int)>();
            sectionCoords.Add(startingCoord);

            int headX = startingCoord.Item1;
            int headY = startingCoord.Item2;

            for (int i = 1; i < length; i++)
            {
                switch (direction)
                {
                    case Direction.Right:
                        if (headX - i == 0)
                        {
                            i++;
                            length++;
                        }
                        sectionCoords.Add((headX - i, headY));
                        break;
                    case Direction.Left:
                        if (headX + i == 0)
                        {
                            i++;
                            length++;
                        }
                        sectionCoords.Add((headX + i, headY));
                        break;
                    case Direction.Down:
                        if (headY + i == 0)
                        {
                            i++;
                            length++;
                        }
                        sectionCoords.Add((headX, headY + i));
                        break;
                    case Direction.Up:
                        if (headY - i == 0)
                        {
                            i++;
                            length++;
                        }
                        sectionCoords.Add((headX, headY - i));
                        break;
                }
            }
            return sectionCoords;
        }
        public override string ToString()
        {
            _allShips.Sort(new ShipComparer());

            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < _allShips.Count; i++)
            {
                sBuilder.Append(String.Format("This is {0} ship \n \n", i + 1));
                sBuilder.Append(_allShips[i].Item2.ToString());
                sBuilder.Append("\n");
            }

            return sBuilder.ToString();
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
    }
}

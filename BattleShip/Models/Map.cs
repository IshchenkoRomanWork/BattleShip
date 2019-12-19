using BattleShip.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.Models
{
    public class Map
    {
        private Dictionary<Coords, Ship> AllShips { get; set; }

        public bool AddShip(Ship ship)
        {
            if (!CheckPositionsAreFree(ship.Coords))
                return false;
            AllShips.Add(ship.Coords, ship);
            return true;
        }

        public string GetState()
        {
            Ship[] shipArray = new Ship[AllShips.Count];
            AllShips.Values.CopyTo(shipArray, 0);
            List<Ship> shipList = new List<Ship>(shipArray);
            shipList.Sort(new ShipComparer());

            StringBuilder sBuilder = new StringBuilder();
            foreach (var ship in shipList)
            {
                sBuilder.Append(ship.GetState());
                sBuilder.Append("/n");
            }

            return sBuilder.ToString();
        }

        public bool Move(Direction direction, Ship ship)
        {
            int headX = ship.Coords.Head.Item1;
            int sternX = ship.Coords.Stern.Item1;
            int headY = ship.Coords.Head.Item2;
            int sternY = ship.Coords.Stern.Item2;
            int speed = ship.Speed;
            switch(direction)
            {
                case Direction.Left:
                    headX -= speed;
                    sternX -= speed;
                    break;
                case Direction.Right:
                    headX += speed;
                    sternX += speed;
                    break;
                case Direction.Down:
                    headY -= speed;
                    sternY -= speed;
                    break;
                case Direction.Up:
                    headY += speed;
                    sternY += speed;
                    break;
            }

            Coords newLocationCoords = new Coords((headX, headY), (sternX, sternY));

            if (!CheckPositionsAreFree(newLocationCoords))
                return false;

            ship.Coords = newLocationCoords;
            AllShips.Remove(ship.Coords);
            AllShips.Add(newLocationCoords, ship);
            return true;
        }

        private bool CheckPositionsAreFree(Coords checkedCoords)
        {
            //return default(bool);
            List<(int, int)> checkedSection = GetAllCoordsInSection(checkedCoords);
            foreach (var sectionCoord in checkedSection)
            {
                if (!CheckPositionAreFree(sectionCoord))
                    return false;
            }
            return true;
        }

        private bool CheckPositionAreFree((int, int) coord)
        {
            //return default(bool);
            foreach(KeyValuePair<Coords, Ship> pair in AllShips)
            {
                var sectionCoords = GetAllCoordsInSection(pair.Key);
                foreach (var sectionCoord in sectionCoords)
                {
                    if (sectionCoord == coord)
                        return false;
                }
            }
            return true;
        }

        private List<(int, int)> GetAllCoordsInSection(Coords coords)
        {
            var sectionCoords = new List<(int, int)>();
            sectionCoords.Add(coords.Head);
            Direction direction = Direction.NoDirection;
            int length = 0;
            int headX = coords.Head.Item1;
            int sternX = coords.Stern.Item1;
            int headY = coords.Head.Item2;
            int sternY = coords.Stern.Item2;

            //Direction Check

            if (headX > sternX)
            {
                length = headX - sternX;
                direction = Direction.Left;
            }
            else if (headX < sternX)
            {
                length = sternX - headX;
                direction = Direction.Right;
            }
            else if (headY > sternY)
            {
                length = headY - sternY;
                direction = Direction.Down;
            }
            else if (headY < sternY)
            {
                length = sternY - headY;
                direction = Direction.Up;
            }
            for (int i = 1; i <= length; i++)
            {
                switch (direction)
                {
                    case Direction.Right:
                        sectionCoords.Add((headX - i, headY));
                        break;
                    case Direction.Left:
                        sectionCoords.Add((headX + i, headY));
                        break;
                    case Direction.Down:
                        sectionCoords.Add((headX, headY - 1));
                        break;
                    case Direction.Up:
                        sectionCoords.Add((headX, headY + 1));
                        break;
                }
            }
            return sectionCoords;
        }

        public Ship this[int quadrant, int xCoord, int yCoord]
        {
            get
            {
                if (quadrant < 1 || quadrant > 4)
                    throw new Exception("Wrong quadrant specified");

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

                foreach (var pair in AllShips)
                {
                    if (pair.Key.Head == (xCoord, yCoord))
                        return pair.Value;
                }

                throw new Exception("Ship is not Found on these coordinates");
            }
        }

    }
}

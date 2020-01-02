using BattleShip.Services;
using CustomORM.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleShip.Models
{
    [Table("Map")]
    public class Map
    {
        [Column("MapId", "int", true)]
        public int Id { get; private set; }
        private List<ShipInformation> _allShips;
        [IsForeignKey]
        [Column("MapId", "int")]
        public List<ShipInformation> AllShips
        {
            get
            {
                return _allShips;
            }
            set
            {
                foreach (var info in value ?? Enumerable.Empty<ShipInformation>())
                {
                    info.Ship.LengthChanged += EventValidateCoords;
                    var coords = _mapHelper.GetAllCoordsInSection
                        (info.ShipLocation.Direction, info.Ship.Length, info.ShipLocation.CoordX, info.ShipLocation.CoordY);
                    ValidateCoords(coords);

                    _occupiedCoords.AddRange(coords);
                }
                _allShips = value;
            }
        }
        [Column("QuadrantSize", "int")]
        private int _quadrantSize;
        public int QuadrantSize
        {
            get
            {
                return _quadrantSize;
            }
            set
            {
                _quadrantSize = value;
                try
                {
                    ValidateCoords(_occupiedCoords);
                }
                catch(Exception e)
                {
                    throw new Exception("New quadrant size is not valid", e);
                }
            }
        }
        private List<(int, int)> _occupiedCoords;

        private MapHelper _mapHelper;
        public Map(int quadrantSize) : this()
        {
            if (quadrantSize < 1)
            {
                throw new Exception("Quadrant size can't be less thah 1");
            }
            QuadrantSize = quadrantSize;
            Id = new Random().Next();
        }

        public Map()
        {
            _allShips = new List<ShipInformation>();
            _mapHelper = new MapHelper();
            _occupiedCoords = new List<(int, int)>();
        }

        public void AddShip(Ship ship, ShipLocation startingLocation)
        {
            ship.LengthChanged += EventValidateCoords;
            var coords = _mapHelper.GetAllCoordsInSection(startingLocation.Direction, ship.Length, startingLocation.CoordX, startingLocation.CoordY);

            ValidateCoords(coords);

            _allShips.Add(new ShipInformation(startingLocation, ship));
            _occupiedCoords.AddRange(coords);
        }

        private void EventValidateCoords(object ship, EventArgs eventArgs)
        {
            var shipToValidate = _allShips.Find(si => si.Ship.Id == (ship as Ship).Id);
            var coords = _mapHelper.GetAllCoordsInSection(shipToValidate.ShipLocation.Direction, shipToValidate.Ship.Length, shipToValidate.ShipLocation.CoordX, shipToValidate.ShipLocation.CoordY);
            ValidateCoords(coords);
        }
        private void ValidateCoords(List<(int, int)> validationCoords)
        {
            //There's a reason to make validation interface and validation list
            if (validationCoords.Count == 0)
                return;

            if (!CoordIsNotOnAxis(validationCoords[0]))
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
            foreach (var coord in checkedCoords)
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

        public override string ToString()
        {
            _allShips.Sort(new ShipComparer());

            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < _allShips.Count; i++)
            {
                sBuilder.Append(String.Format("This is {0} ship \n \n", i + 1));
                sBuilder.Append(_allShips[i].Ship.ToString());
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
                if (xCoord < 1 || yCoord < 1)
                    throw new Exception("Coords cant be less than one");
                if (Math.Abs(xCoord) > _quadrantSize || Math.Abs(yCoord) > _quadrantSize)
                    throw new Exception("Coords can't be ot of quadrant size");

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
                    if (pair.ShipLocation.CoordX == xCoord && pair.ShipLocation.CoordY == yCoord)
                        return pair.Ship;
                }

                throw new Exception("Ship is not Found on these coordinates");
            }
        }
    }
}

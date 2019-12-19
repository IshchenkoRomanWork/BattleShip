using BattleShip.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace BattleShip.Services
{
    class ShipComparer : IComparer<(ShipLocation, Ship)>
    {

        public int Compare([AllowNull] (ShipLocation, Ship) firstShip, [AllowNull] (ShipLocation, Ship) secondShip)
        {
            int firstShipHeadSqr = (int)(Math.Pow(firstShip.Item1.Coords.Item1, 2) + Math.Pow(firstShip.Item1.Coords.Item2, 2));
            int secondShipHeadSqr = (int)(Math.Pow(secondShip.Item1.Coords.Item1, 2) + Math.Pow(secondShip.Item1.Coords.Item2, 2));
            double firstShipDistance = Math.Sqrt(firstShipHeadSqr);
            double secondShipDistance = Math.Sqrt(secondShipHeadSqr);

            if (firstShipHeadSqr == secondShipHeadSqr)
                return 0;
            else if (firstShipDistance > secondShipDistance)
                return 1;
            return -1;
        }
    }
}

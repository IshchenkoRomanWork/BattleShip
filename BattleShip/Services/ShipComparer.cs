using BattleShip.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace BattleShip.Services
{
    class ShipComparer : IComparer<ShipInformation>
    {

        public int Compare([AllowNull] ShipInformation firstShip, [AllowNull] ShipInformation secondShip)
        {
            int firstShipHeadSqr = (int)(Math.Pow(firstShip.ShipLocation.CoordX, 2) + Math.Pow(firstShip.ShipLocation.CoordY, 2));
            int secondShipHeadSqr = (int)(Math.Pow(secondShip.ShipLocation.CoordY, 2) + Math.Pow(secondShip.ShipLocation.CoordY, 2));
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

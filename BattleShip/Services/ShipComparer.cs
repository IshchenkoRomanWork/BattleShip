using BattleShip.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace BattleShip.Services
{
    class ShipComparer : IComparer<Ship>
    {

        public int Compare([AllowNull] Ship firstShip, [AllowNull] Ship secondShip)
        {
            int firstShipHeadSqr = (int)(Math.Pow(firstShip.Coords.Head.Item1, 2) + Math.Pow(firstShip.Coords.Head.Item2, 2));
            int secondShipHeadSqr = (int)(Math.Pow(secondShip.Coords.Head.Item1, 2) + Math.Pow(secondShip.Coords.Head.Item2, 2));
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

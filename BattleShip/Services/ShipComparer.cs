using BattleShip.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace BattleShip.Services
{
    class ShipComparer : IComparer<KeyValuePair<Coords, Ship>>
    {

        public int Compare([AllowNull] KeyValuePair<Coords, Ship> x, [AllowNull] KeyValuePair<Coords, Ship> y)
        {
            return default(int);
        }
    }
}

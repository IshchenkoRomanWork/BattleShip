using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.Models
{
    public struct Coords
    {
        public (int, int) Head; //Coords are X and Y
        public (int, int) Stern;

        public Coords((int, int) head, (int, int) stern)
        {
            Head = head;
            Stern = stern;
        }
    }
}

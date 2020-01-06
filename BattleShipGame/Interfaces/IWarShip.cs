using BattleShipGame.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShipGame.Interfaces
{
    interface IWarShip
    {
        bool Shot(Ship target);
    }
}

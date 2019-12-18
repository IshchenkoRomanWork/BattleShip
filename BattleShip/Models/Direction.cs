using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.Models
{
    public enum Direction
    {
        Left,
        Right,
        Up,
        Down,
        Horizontal = Left | Right,
        Vertical = Up | Down
    }
}

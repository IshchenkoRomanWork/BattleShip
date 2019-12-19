using BattleShip.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.Services
{
    class UnitOfWork
    {
        public Map Map { get; set; }

        public UnitOfWork()
        {
            Map = new Map(100);
        }


        //public bool Move()
        //{
        //    return default;
        //    int headX = ship.Coords.Head.Item1;
        //    int sternX = ship.Coords.Stern.Item1;
        //    int headY = ship.Coords.Head.Item2;
        //    int sternY = ship.Coords.Stern.Item2;
        //    int speed = ship.Speed;
        //    switch (direction)
        //    {
        //        case Direction.Left:
        //            headX -= speed;
        //            sternX -= speed;
        //            break;
        //        case Direction.Right:
        //            headX += speed;
        //            sternX += speed;
        //            break;
        //        case Direction.Down:
        //            headY -= speed;
        //            sternY -= speed;
        //            break;
        //        case Direction.Up:
        //            headY += speed;
        //            sternY += speed;
        //            break;
        //    }

        //    Coords newLocationCoords = new Coords((headX, headY), (sternX, sternY));

        //    if (!CheckPositionsAreFree(newLocationCoords))
        //        return false;

        //    ship.Coords = newLocationCoords;
        //    AllShips.Remove(ship.Coords);
        //    AllShips.Add(newLocationCoords, ship);
        //    return true;
        //}
    }
}

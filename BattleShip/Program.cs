using BattleShip.Models;
using BattleShip.Services;
using CustomORM.Interfaces;
using CustomORM.Services;
using CustomORM.Test;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace BattleShip
{
    class Program
    {
        static void Main(string[] args)
        {
            //ConsoleWorker consoleWorker = new ConsoleWorker();
            //consoleWorker.EntryPoint();

            Test();
        }

        static void Test()
        {

            IORM<NonExistentDbClass> orm = new ORM<NonExistentDbClass>("Server=ISHCHENKO;Database=BattleShip;Trusted_Connection=True;");
            //var map = new Map(50);
            //map.AllShips = new List<ShipInformation>();
            //map.AddShip(
            //    new WarShip(5, 10, 10, 2),
            //    new ShipLocation(Direction.Down, (1, 1))
            //    );
            //map.AllShips[0].Ship.Length = 70;
            orm.GetAllFromDatabase();

            //orm.InsertToDatabase(map);
            //orm.GetAllFromDatabase().Single();
        }
    }
}

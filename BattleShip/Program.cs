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

            IORM<Map> orm = new ORM<Map>("Server=ISHCHENKO;Database=BattleShip;Trusted_Connection=True;");
            //var map = new Map(50);
            //map.AllShips = new List<ShipInformation>();
            //map.AllShips.Add(new ShipInformation(
            //    new ShipLocation(Direction.Down, (1, 1)),
            //    new WarShip(5, 10, 10, 2)
            //    ));
            //map.AllShips.Add(new ShipInformation(
            //    new ShipLocation(Direction.Up, (2, 2)),
            //    new WarShip(7, 13, 12, 5)
            //    ));
            //map.AllShips.Add(new ShipInformation(
            //    new ShipLocation(Direction.Up, (3, 3)),
            //    new WarShip(6, 11, 6, 100)
            //    ));

            //orm.InsertToDatabase(map);
            var map = orm.GetAllFromDatabase().Single();
            map.AllShips = null;
            orm.UpdateInDatabase(map);
        }
    }
}

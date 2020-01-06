using BattleShip.Models;
using BattleShip.Services;
using ORM.Interfaces;
using ORM.Services;
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
            //map.AddShip(
            //    new WarShip(5, 10, 10, 2),
            //    new ShipLocation(Direction.Down, (1, 1))
            //    );
            //map.AllShips[0].Ship.Length = 70;
            orm.GetFromDatabase("stringId");
            //orm.InsertToDatabase(map);
            //orm.GetAllFromDatabase().Single();
        }
    }
}

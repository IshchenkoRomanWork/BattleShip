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
            //TestClass testClass = new TestClass()
            //{
            //    Id = 100,
            //    IntValue = 200,
            //    stringValue = "mystring"
            //};
            //TestClass tc = new TestClass() { Id = 320, IntValue = 400, stringValue = "ListString" };
            //tc.SecondList = new List<SecondTestClass>() 
            //{
            //    new SecondTestClass() {boolValue=false, Id=11},
            //    new SecondTestClass() {boolValue=true, Id =22 },
            //    new SecondTestClass() {boolValue=true, Id =33 },
            //    new SecondTestClass() {boolValue=true, Id = 44 }
            //};

            //IORM<TestClass> orm = new ORM<TestClass>("Server=ISHCHENKO;Database=TestDatabase;Trusted_Connection=True;");
            //var all = orm.GetAllFromDatabase();
            //foreach(var tc in all)
            //{
            //    Console.WriteLine(tc.IntValue);
            //    foreach (var stc in tc.SecondList)
            //    {
            //        Console.WriteLine(stc.Id);
            //    }

            //}

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
            //var map = orm.GetAllFromDatabase().Single();
            orm.DeleteFromDatabase(1892184318);
            //foreach(var map in maps)
            //{
            //    Console.WriteLine(map);
            //};

            //using (SqlConnection connection = new SqlConnection("Server = ISHCHENKO; Database = TestDatabase; Trusted_Connection = True;"))
            //{
            //    object petya = 5;
            //    var sqlCommand = new SqlCommand();
            //    sqlCommand.CommandText = "SELECT * FROM TestTable WHERE IntVal = @value";
            //    sqlCommand.Parameters.AddWithValue("@value", petya);
            //    connection.Open();
            //    sqlCommand.Connection = connection;
            //    var result = sqlCommand.ExecuteReader();
            //    result.Read();
            //    Console.WriteLine(result.GetValue(0).ToString(), result.GetValue(1).ToString(), result.GetValue(2).ToString());
            //    connection.Close();
            //}
        }
    }
}

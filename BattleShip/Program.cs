using BattleShip.Services;
using CustomORM.Interfaces;
using CustomORM.Services;
using CustomORM.Test;
using System;
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

            //TestClass testClass = new TestClass()
            //{
            //    Id = 100,
            //    IntValue = 200,
            //    stringValue = "mystring"
            //};
            TestClass tc = new TestClass();

            IORM<TestClass> orm = new ORM<TestClass>("Server=ISHCHENKO;Database=TestDatabase;Trusted_Connection=True;");
            orm.UpdateInDatabase(new TestClass() { Id = 100, IntValue = 300, stringValue = "myNewString" });

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

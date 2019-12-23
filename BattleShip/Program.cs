using BattleShip.Services;
using System;

namespace BattleShip
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleWorker consoleWorker = new ConsoleWorker();
            consoleWorker.EntryPoint();

        }
    }
}

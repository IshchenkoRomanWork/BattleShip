using BattleShip.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BattleShip.Services
{
    class ConsoleWorker
    {
        UnitOfWork _unitOfWork;

        public ConsoleWorker()
        {
            _unitOfWork = new UnitOfWork();
        }
        public void EntryPoint()
        {
            Stubs();
            Console.WriteLine("Hello, Commander! \n");
            Console.WriteLine("To give a command type it in command line and press enter. \n");
            bool notEnd;
            do
            {
                notEnd = ChooseOrder();
            }
            while (notEnd);
            Console.WriteLine("Good Luck Commander! \n");
            Thread.Sleep(1000);

        }

        void Stubs()
        {
            _unitOfWork.Map.AddShip(new WarShip(2, 20, 20, 20, 20), new ShipLocation(Direction.Right, (0, 0)));
            _unitOfWork.Map.AddShip(new RepairShip(3, 30, 30, 30, 30), new ShipLocation(Direction.Left, (0, 1)));
            _unitOfWork.Map.AddShip(new RepairWarShip(4, 40, 40, 40, 40, 40), new ShipLocation(Direction.Right, (0, 2)));
            _unitOfWork.Map.AddShip(new WarShip(5, 50, 50, 50, 50), new ShipLocation(Direction.Left, (0, 3)));
        }

        bool ChooseOrder()
        {
            StringBuilder sBuilder = new StringBuilder();
            sBuilder.Append("What is your Order? \n");
            //sBuilder.Append("1: To add new ship type \"Add new ship\" \n");
            //sBuilder.Append("2: To select ship for an orders type \"Select ship\" \n");
            sBuilder.Append("3: To get current sea state s type \"Get State\" \n");
            sBuilder.Append("4: To stop work type \"Stop Work\" \n");
            Console.WriteLine(sBuilder.ToString());
            bool orderNotSelected = false;

            do
            {
                string order = Console.ReadLine();
                switch (order)
                {
                    //    case "Add new ship":
                    //        AddShip();
                    //        break;
                    //    case "Select ship":
                    //        SelectShip();
                    //        break;
                    case "Get State":
                        GetTotalState();
                        break;
                    case "Stop Work":
                        return false;
                        break;
                    default:
                        Console.WriteLine("Please, type again \n");
                        orderNotSelected = true;
                        break;
                }
            }
            while (orderNotSelected);
            return true;
        }

        //void AddShip()
        //{
            //Console.WriteLine("Good! Okay, choose ship's type \n");
            //Console.WriteLine("Types are \"War\", \"Repair\" and \"Mixed\", To choose type it's name\n");
            //bool wrongTyped = false;

            //Ship buildingShip;
            //do
            //{
            //    string type = Console.ReadLine();
            //    switch (type)
            //    {
            //        case "War":
            //            buildingShip = new WarShip();
            //            break;
            //        case "Repair ship":
            //            buildingShip = new RepairShip();
            //            break;
            //        case "Get State":
            //            buildingShip = new RepairWarShip();
            //            break;
            //        default:
            //            Console.WriteLine("Please, type again \n");
            //            wrongTyped = true;
            //            break;
            //    }
            //}
            //while (wrongTyped);

            //Console.WriteLine("Good! Okay, choose ship's Coordinates \n");

            //Coords shipCoords;
            //bool isDiagonal;
            //do
            //{
            //    shipCoords = GetCoords();
            //    isDiagonal = shipCoords.Head.Item1 != shipCoords.Stern.Item1 && shipCoords.Head.Item2 != shipCoords.Stern.Item1;
            //    if (isDiagonal)
            //        Console.WriteLine("Ship can't be diagonal, type again");
            //}
            //while (isDiagonal);

            //Console.WriteLine("Good! Okay, choose ship's Speed \n");

        //}

        //Coords GetCoords()
        //{
        //    Console.WriteLine("Head's X Coordinate: \n");
        //    int headX;
        //    bool typedRight;
        //    do
        //    {
        //        typedRight = int.TryParse(Console.ReadLine(), out headX);
        //        if (!typedRight)
        //            Console.WriteLine("Please, type again \n");
        //    }
        //    while (!typedRight);

        //    Console.WriteLine("Head's Y Coordinate: \n");
        //    int headY;
        //    do
        //    {
        //        typedRight = int.TryParse(Console.ReadLine(), out headY);
        //        if (!typedRight)
        //            Console.WriteLine("Please, type again \n");
        //    }
        //    while (!typedRight);

        //    Console.WriteLine("Stern's Y Coordinate: \n");
        //    int sternX;
        //    do
        //    {
        //        typedRight = int.TryParse(Console.ReadLine(), out sternX);
        //        if (!typedRight)
        //            Console.WriteLine("Please, type again \n");
        //    }
        //    while (!typedRight);

        //    Console.WriteLine("Stern's Y Coordinate: \n");
        //    int sternY;
        //    do
        //    {
        //        typedRight = int.TryParse(Console.ReadLine(), out sternY);
        //        if (!typedRight)
        //            Console.WriteLine("Please, type again \n");
        //    }
        //    while (!typedRight);

        //    return new Coords((headX, headY), (sternX, sternY));
        //}
        //void SelectShip()
        //{

        //}
        //void MoveShip(Ship ship)
        //{

        //}
        void GetTotalState()
        {
            Console.WriteLine("There's Current Sea State: \n");
            Console.WriteLine(_unitOfWork.Map.ToString());
        }
    }
}

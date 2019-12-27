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
            //_unitOfWork.Map.AddShip(new RepairWarShip(10, 40, 40, 40, 40, 40), new ShipLocation(Direction.Right, (1, 2)));
            //_unitOfWork.Map.AddShip(new WarShip(5, 50, 50, 50, 50), new ShipLocation(Direction.Left, (10, 10)));
            //_unitOfWork.Map.AddShip(new WarShip(2, 20, 20, 20, 20), new ShipLocation(Direction.Right, (1, 50)));
            //_unitOfWork.Map.AddShip(new RepairShip(3, 30, 30, 30, 30), new ShipLocation(Direction.Left, (-100, 1)));
            //var stubShip = _unitOfWork.Map[2, 1000, 1];
            //Console.WriteLine(stubShip.ToString());
        }

        bool ChooseOrder()
        {
            StringBuilder sBuilder = new StringBuilder();
            sBuilder.Append("What is your next Order? \n");
            sBuilder.Append("1: To add new ship type \"Add new ship\" \n");
            //sBuilder.Append("2: To select ship for an orders type \"Select ship\" \n");
            sBuilder.Append("3: To get current sea state s type \"Get State\" \n");
            sBuilder.Append("4: To stop work type \"Stop Work\" \n");
            Console.WriteLine(sBuilder.ToString());
            bool orderNotSelected = false;

            do
            {
                string order = Console.ReadLine();
                orderNotSelected = false;
                switch (order)
                {
                    case "Add new ship":
                        AddShip();
                        break;
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

        void AddShip()
        {
            Console.WriteLine("Good! Okay, choose ship's type \n");
            var buildingShip = GetType();

            Console.WriteLine("Good! Okay, choose ship's head starting coordinates \n");
            var shipCoords = GetCoords();

            Console.WriteLine("Good! Okay, choose ship's Speed \n");
            var speed = GetSpeed();

            Console.WriteLine("Good! Okay, choose ship's Length \n");
            var length = GetLength();

            Console.WriteLine("Good! Okay, choose ship's starting hit points \n");
            var hitPoints = GetHitPoints();

            Console.WriteLine("Good! Okay, choose ship's starting Direction \n");
            var dir = GetDirection();

            buildingShip.HitPoints = hitPoints;
            buildingShip.Length = length;
            buildingShip.Speed = speed;

            _unitOfWork.Map.AddShip(buildingShip, new ShipLocation(dir, shipCoords));

            Console.WriteLine("Congratulations! Your ship has been created!");

        }

        private Direction GetDirection()
        {
            Console.WriteLine("Choos one direction: Left, Right, Up, Down \n");
            bool orderNotSelected = default;
            Direction dir = Direction.NoDirection;
            do
            {
                orderNotSelected = false;
                string direction = Console.ReadLine();
                switch (direction)
                {
                    case "Right":
                        dir = Direction.Right;
                        break;
                    case "Left":
                        dir = Direction.Left;
                        break;
                    case "Up":
                        dir = Direction.Up;
                        break;
                    case "Down":
                        dir = Direction.Down;
                        break;
                    default:
                        Console.WriteLine("Please, type again \n");
                        orderNotSelected = true;
                        break;
                }
            }
            while (orderNotSelected);

            return dir;
        }

        private int GetHitPoints()
        {
            int hitPoints;
            bool typedRight;
            do
            {
                typedRight = int.TryParse(Console.ReadLine(), out hitPoints);
                if (!typedRight)
                    Console.WriteLine("Please, type again \n");
            }
            while (!typedRight);
            return hitPoints;
        }

        private int GetLength()
        {
            int length;
            bool typedRight;
            do
            {
                typedRight = int.TryParse(Console.ReadLine(), out length);
                if (!typedRight)
                    Console.WriteLine("Please, type again \n");
            }
            while (!typedRight);
            return length;
        }

        private int GetSpeed()
        {
            int speed;
            bool typedRight;
            do
            {
                typedRight = int.TryParse(Console.ReadLine(), out speed);
                if (!typedRight)
                    Console.WriteLine("Please, type again \n");
            }
            while (!typedRight);
            return speed;
        }

        private Ship GetType()
        {
            Console.WriteLine("Types are \"War\", \"Repair\" and \"Mixed\", To choose type it's name\n");
            bool wrongTyped = default;
            Ship buildingShip = default;
            do
            {
                wrongTyped = false;
                string type = Console.ReadLine();
                switch (type)
                { 
                    case "War":
                        buildingShip = new WarShip();
                        break;
                    case "Repair ship":
                        buildingShip = new RepairShip();
                        break;
                    case "Get State":
                        buildingShip = new RepairWarShip();
                        break;
                    default:
                        Console.WriteLine("Please, type again \n");
                        wrongTyped = true;
                        break;
                }
            }
            while (wrongTyped);

            return buildingShip;
        }

        (int, int) GetCoords()
        {
            Console.WriteLine("Head's X Coordinate: \n");
            int headX;
            bool typedRight;
            do
            {
                typedRight = int.TryParse(Console.ReadLine(), out headX);
                if (!typedRight)
                    Console.WriteLine("Please, type again \n");
            }
            while (!typedRight);

            Console.WriteLine("Head's Y Coordinate: \n");
            int headY;
            do
            {
                typedRight = int.TryParse(Console.ReadLine(), out headY);
                if (!typedRight)
                    Console.WriteLine("Please, type again \n");
            }
            while (!typedRight);

            return (headX, headY);
        }
        //void SelectShip()
        //{

        //}
        //void MoveShip(Ship ship)
        //{

        //}
        void GetTotalState()
        {
            Console.WriteLine("There's Current Seas State: \n");
            Console.WriteLine(_unitOfWork.Map.ToString());
        }
    }
}

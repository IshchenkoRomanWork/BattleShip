﻿using BattleShip.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip.Services
{
    class MapHelper
    {
        public List<(int, int)> GetAllCoordsInSection(Direction direction, int length, (int, int) startingCoord)
        {
            var sectionCoords = new List<(int, int)>();
            sectionCoords.Add(startingCoord);

            int headX = startingCoord.Item1;
            int headY = startingCoord.Item2;

            for (int i = 1; i < length; i++)
            {
                switch (direction)
                {
                    case Direction.Right:
                        if (headX - i == 0)
                        {
                            i++;
                            length++;
                        }
                        sectionCoords.Add((headX - i, headY));
                        break;
                    case Direction.Left:
                        if (headX + i == 0)
                        {
                            i++;
                            length++;
                        }
                        sectionCoords.Add((headX + i, headY));
                        break;
                    case Direction.Down:
                        if (headY + i == 0)
                        {
                            i++;
                            length++;
                        }
                        sectionCoords.Add((headX, headY + i));
                        break;
                    case Direction.Up:
                        if (headY - i == 0)
                        {
                            i++;
                            length++;
                        }
                        sectionCoords.Add((headX, headY - i));
                        break;
                }
            }
            return sectionCoords;
        }
    }
}
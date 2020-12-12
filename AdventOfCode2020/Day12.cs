using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day12
    {
        public static void Run()
        {
            List<string> inputs = File.ReadAllLines("day12Input.txt").ToList();

            int dir = 90;
            int xPos = 0;
            int yPos = 0;
            foreach (var line in inputs)
            {
                char instruction = line[0];
                int value = int.Parse(line.Substring(1));
                switch (instruction)
                {
                    case 'N':
                        yPos += value;
                        break;
                    case 'E':
                        xPos += value;
                        break;
                    case 'S':
                        yPos -= value;
                        break;
                    case 'W':
                        xPos -= value;
                        break;
                    case 'L':
                        for (int i = 0; i < value / 90; i++)
                        {
                            dir = dir - 90;
                            if (dir < 0)
                            {
                                dir = 270;
                            }
                        }
                        break;
                    case 'R':
                        for (int i = 0; i < value / 90; i++)
                        {
                            dir = dir + 90;
                            if (dir == 360)
                            {
                                dir = 0;
                            }
                        }
                        break;
                    case 'F':
                        switch (dir)
                        {
                            case 0:
                                yPos += value;
                                break;
                            case 90:
                                xPos += value;
                                break;
                            case 180:
                                yPos -= value;
                                break;
                            case 270:
                                xPos -= value;
                                break;
                            default:
                                Console.WriteLine("We've lost the cardinal directions, Cap'n!");
                                break;
                        }
                        break;
                    default:
                        Console.Write("Ship's Fucked, Cap'n!");
                        break;
                }
            }
            Console.WriteLine($"Final Manhattan distance: {Math.Abs(xPos) + Math.Abs(yPos)}");

            int shipXPos = 0;
            int shipYPos = 0;
            int waypointXDist = 10;
            int waypointYDist = 1;
            foreach(string line in inputs)
            {
                char instruction = line[0];
                int value = int.Parse(line.Substring(1));
                switch (instruction)
                {
                    case 'N':
                        waypointYDist += value;
                        break;
                    case 'E':
                        waypointXDist += value;
                        break;
                    case 'S':
                        waypointYDist -= value;
                        break;
                    case 'W':
                        waypointXDist -= value;
                        break;
                    case 'F':
                        for(int i = 0; i < value; i++)
                        {
                            shipXPos += waypointXDist;
                            shipYPos += waypointYDist;
                        }
                        break;
                    case 'L':
                        for(int i = 0; i < value / 90; i++)
                        {
                            int prevX = waypointXDist;
                            int prevY = waypointYDist;

                            waypointXDist = prevY * -1;
                            waypointYDist = prevX;
                        }
                        break;
                    case 'R':
                        for (int i = 0; i < value / 90; i++)
                        {
                            int prevX = waypointXDist;
                            int prevY = waypointYDist;

                            waypointXDist = prevY;
                            waypointYDist = prevX * -1;
                        }
                        break;
                }
                Console.WriteLine($"({shipXPos},{shipYPos}) - ({waypointXDist},{waypointYDist})");
            }
            Console.WriteLine($"Second Manhattan Distance: {Math.Abs(shipXPos) + Math.Abs(shipYPos)}");
        }
    }
}

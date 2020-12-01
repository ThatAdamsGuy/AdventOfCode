using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("*******************");
                Console.WriteLine("Advent of Code 2020");
                Console.WriteLine("*******************");
                Console.WriteLine();
                Console.Write("Run day: ");
                int dayToRun = int.Parse(Console.ReadLine());

                switch (dayToRun)
                {
                    case 1:
                        Day01.Run();
                        break;
                    case 2:
                        Day02.Run();
                        break;
                    case 3:
                        Day03.Run();
                        break;
                    case 4:
                        Day04.Run();
                        break;
                    case 5:
                        Day05.Run();
                        break;
                    case 6:
                        Day06.Run();
                        break;
                    case 7:
                        Day07.Run();
                        break;
                    case 8:
                        Day08.Run();
                        break;
                    case 9:
                        Day09.Run();
                        break;
                    case 10:
                        Day10.Run();
                        break;
                    case 11:
                        Day11.Run();
                        break;
                    case 12:
                        Day12.Run();
                        break;
                    case 13:
                        Day13.Run();
                        break;
                    case 14:
                        Day14.Run();
                        break;
                    case 15:
                        Day15.Run();
                        break;
                    case 16:
                        Day16.Run();
                        break;
                    case 17:
                        Day17.Run();
                        break;
                    case 18:
                        Day18.Run();
                        break;
                    case 19:
                        Day19.Run();
                        break;
                    case 20:
                        Day20.Run();
                        break;
                    case 21:
                        Day21.Run();
                        break;
                    case 22:
                        Day22.Run();
                        break;
                    case 23:
                        Day23.Run();
                        break;
                    case 24:
                        Day24.Run();
                        break;
                    case 25:
                        Day25.Run();
                        break;
                }
                Console.WriteLine();
            }
        }
    }
}

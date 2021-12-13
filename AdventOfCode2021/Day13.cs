using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    internal class Day13
    {
        public static void Run()
        {
            List<Coordinate> points = new List<Coordinate>();
            var input = File.ReadAllLines("Day13Input.txt").ToList();

            while (!string.IsNullOrWhiteSpace(input.First()))
            {
                var split = input.First().Split(',');
                points.Add(new Coordinate(int.Parse(split[0]), int.Parse(split[1])));
                input.RemoveAt(0);
            }
            input.RemoveAt(0);

            for(int i = 0; i < input.Count(); i++)
            {
                var toDo = input[i].Split(' ')[2].Split('=');
                int foldLine = int.Parse(toDo[1]);

                if (toDo[0].Equals("x"))
                {
                    foreach (var coord in points)
                    {
                        if (coord.X == foldLine)
                        {
                            Console.WriteLine("Fuck");
                        }

                        if (coord.X > foldLine)
                        {
                            coord.X = foldLine - (coord.X - foldLine);
                        }
                    }
                }
                else
                {
                    foreach (var coord in points)
                    {
                        if (coord.Y == foldLine)
                        {
                            Console.WriteLine("Fuck");
                        }

                        if (coord.Y > foldLine)
                        {
                            coord.Y = foldLine - (coord.Y - foldLine);
                        }
                    }
                }

                points = points.Distinct().ToList();
                points = points.Where(x => x.X >= 0 && x.Y >= 0).ToList();
                if (i == 0)
                {
                    Console.WriteLine("Part 1 - " + points.Count());
                }
            }

            Console.WriteLine();
            Console.WriteLine("Part 2: ");
            Console.WriteLine();

            int maxX = 0;
            int maxY = 0;
            foreach (var point in points)
            {
                if (point.X > maxX) maxX = point.X;
                if (point.Y > maxY) maxY = point.Y;
            }

            for(int i = 0; i <= maxY; i++)
            {
                for(int j = 0; j <= maxX; j++)
                {
                    if(points.Any(x => x.X == j && x.Y == i))
                    {
                        Console.Write("█");
                    } else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}

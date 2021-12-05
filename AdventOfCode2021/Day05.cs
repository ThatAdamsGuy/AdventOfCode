using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    internal class Day05
    {
        public static void Run()
        {
            var inputs = File.ReadAllLines("Day05Input.txt");
            Dictionary<Coordinate, int> vents = new Dictionary<Coordinate, int>();

            foreach(string line in inputs)
            {
                var parts = line.Split(' ');

                var coordOne = parts[0].Split(',');
                var coordTwo = parts[2].Split(',');
                Coordinate one = new Coordinate(int.Parse(coordOne[0]), int.Parse(coordOne[1]));
                Coordinate two = new Coordinate(int.Parse(coordTwo[0]), int.Parse(coordTwo[1]));

                if(one.X != two.X && one.Y != two.Y)
                {
                    continue;
                }
                else if (one.X != two.X)
                {
                    if(one.X < two.X)
                    {
                        for(int i = one.X; i <= two.X; i++)
                        {
                            SortCoord(i, one.Y, vents);
                        }
                    } else
                    {
                        for (int i = two.X; i <= one.X; i++)
                        {
                            SortCoord(i, one.Y, vents);
                        }
                    }
                }
                else
                {
                    if (one.Y < two.Y)
                    {
                        for (int i = one.Y; i <= two.Y; i++)
                        {
                            SortCoord(one.X, i, vents);
                        }
                    }
                    else
                    {
                        for (int i = two.Y; i <= one.Y; i++)
                        {
                            SortCoord(one.X, i, vents);
                        }
                    }
                }
            }

            Console.WriteLine($"Part 1 - {vents.Where(s => s.Value > 1).Count()}");
            vents.Clear();

            foreach(string line in inputs)
            {
                var parts = line.Split(' ');

                var coordOne = parts[0].Split(',');
                var coordTwo = parts[2].Split(',');
                Coordinate one = new Coordinate(int.Parse(coordOne[0]), int.Parse(coordOne[1]));
                Coordinate two = new Coordinate(int.Parse(coordTwo[0]), int.Parse(coordTwo[1]));

                if (one.X != two.X && one.Y != two.Y)
                {
                    //Diagonal Lines
                    if(one.X < two.X && one.Y < two.Y)
                    {
                        //Right and Up
                        for(int i = one.X; i <= two.X; i++)
                        {
                            SortCoord(i, one.Y + (i - one.X), vents);
                        }
                    }
                    else if (one.X < two.X && one.Y > two.Y)
                    {
                        //Right and Down
                        for (int i = one.X; i <= two.X; i++)
                        {
                            SortCoord(i, one.Y - (i - one.X), vents);
                        }
                    }
                    else if (one.X > two.X && one.Y < two.Y)
                    {
                        //Left and Up
                        for (int i = two.X; i <= one.X; i++)
                        {
                            SortCoord(i, two.Y - (i - two.X), vents);
                        }
                    }
                    else if (one.X > two.X && one.Y > two.Y)
                    {
                        //Left and Down
                        for (int i = two.X; i <= one.X; i++)
                        {
                            SortCoord(i, two.Y + (i - two.X), vents);
                        }
                    }
                    else
                    {
                        Console.WriteLine("How the fuck did you get here?");
                    }
                }
                else if (one.X != two.X)
                {
                    if (one.X < two.X)
                    {
                        for (int i = one.X; i <= two.X; i++)
                        {
                            SortCoord(i, one.Y, vents);
                        }
                    }
                    else
                    {
                        for (int i = two.X; i <= one.X; i++)
                        {
                            SortCoord(i, one.Y, vents);
                        }
                    }
                }
                else
                {
                    if (one.Y < two.Y)
                    {
                        for (int i = one.Y; i <= two.Y; i++)
                        {
                            SortCoord(one.X, i, vents);
                        }
                    }
                    else
                    {
                        for (int i = two.Y; i <= one.Y; i++)
                        {
                            SortCoord(one.X, i, vents);
                        }
                    }
                }
            }
            Console.WriteLine($"Part 2 - {vents.Where(s => s.Value > 1).Count()}");
        }

        private static void SortCoord(int x, int y, Dictionary<Coordinate, int> list)
        {
            Coordinate newCoord = new Coordinate(x, y);
            if (list.ContainsKey(newCoord))
            {
                list[newCoord]++;
            }
            else
            {
                list.Add(newCoord, 1);
            }
        }
    }

    class Coordinate
    {
        public int X;
        public int Y;

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            Coordinate other = obj as Coordinate;
            return other != null && other.X == this.X && other.Y == this.Y;
        }
    }
}

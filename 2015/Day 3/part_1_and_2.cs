using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int santaLocationX = 0;
            int santaLocationY = 0;

            int roboLocationX = 0;
            int roboLocationY = 0;

            string input = File.ReadAllText("../../input.txt").Replace("\n", "");
            List<Location> list = new List<Location>();
            list.Add(new Location(santaLocationX, santaLocationY));
            Console.WriteLine(input);

            bool santa = true;
            foreach (char now in input)
            {
                int counter = 0;
                switch (now)
                {
                    case '>':
                        if (santa)
                        {
                            santaLocationX++;
                        }
                        else { roboLocationX++; };
                        break;
                    case '<':
                        if (santa)
                        {
                            santaLocationX--;
                        }
                        else { roboLocationX--; };
                        break;
                    case '^':
                        if (santa)
                        {
                            santaLocationY++;
                        }
                        else { roboLocationY++; };
                        break;
                    case 'v':
                        if (santa)
                        {
                            santaLocationY--;
                        }
                        else { roboLocationY--; };
                        break;
                }

                if (santa)
                {
                    Console.WriteLine("SANTA - X: " + santaLocationX + ". Y: " + santaLocationY);
                } else
                {
                    Console.WriteLine("ROBO  - X: " + roboLocationX + ". Y: " + roboLocationY);
                }

                if (santa && !(list.Exists(n => n.x == santaLocationX && n.y == santaLocationY)))
                {
                    list.Add(new Location(santaLocationX, santaLocationY));
                }
                else if (!santa && !(list.Exists(n => n.x == roboLocationX && n.y == roboLocationY)))
                {
                    list.Add(new Location(roboLocationX, roboLocationY));
                }
                counter++;

                santa = !santa;
            }

            Console.WriteLine("SUM: " + (list.Count));
            Console.ReadLine();
        }

        public class Location
        {
            public int x;
            public int y;

            public Location(int inX, int inY)
            {
                this.x = inX;
                this.y = inY;
            }
        }
    }
}

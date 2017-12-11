using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _10._1
{
    class Program
    {
        static void Main(string[] args)
        {
            int santaLocationX = 0;
            int santaLocationY = 0;

            int roboLocationX = 0;
            int roboLocationY = 0;

            string input = File.ReadAllText("../../input.txt");
            List<Location> list = new List<Location>();
            list.Add(new Location(santaLocationX, santaLocationY));

            bool santa = true;
            foreach (char now in input)
            {
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

                if (!(list.Exists(n => n.x == santaLocationX && n.y == santaLocationY)))
                {
                    if (santa)
                    {
                        list.Add(new Location(santaLocationX, santaLocationY));
                        santa = !santa;
                    }
                    else
                    {
                        list.Add(new Location(roboLocationX, roboLocationY));
                        santa = !santa;
                    }
                }
            }
            //&& !roboList.Exists(n => n.x == roboLocationX && n.y == roboLocationY)


            Console.WriteLine("SUM: " + (list.Count));
            Console.ReadLine();
        }
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

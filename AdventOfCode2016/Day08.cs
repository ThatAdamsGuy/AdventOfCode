using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    class Day08
    {
        private static int Width;
        private static int Height;
        private static string Filepath;

        private static bool Debug = true;

        public static void Run()
        {
            if (!Debug)
            {
                Width = 50;
                Height = 6;
                Filepath = "Day08Input.txt";
            }
            else
            {
                Width = 7;
                Height = 3;
                Filepath = "Day08Known.txt";
            }

            var instructions = File.ReadAllLines(Filepath);
            bool[,] screen = new bool[Width, Height];

            for (int i = 0; i < Width; i++)
            {
                for(int k = 0; k < Height; k++)
                {
                    screen[i, k] = false;
                }
            }

            foreach(var line in instructions)
            {
                if (line.Contains("rect"))
                {
                    ProcessRect(line, ref screen);
                }
                else if (line.Contains("row"))
                {
                    ProcessXRotate(line, ref screen);
                }
                else if (line.Contains("column"))
                {
                    ProcessYRotate(line, ref screen);
                }
                else
                {
                    throw new ArgumentException();
                }

                OutputScreen(screen);
            }
            int sum = screen.Cast<bool>().Count(x => x);
            Console.WriteLine("Part 1 - " + sum);
        }

        private static void OutputScreen(bool[,] screen)
        {
            for(int i = 0; i < Width; i++)
            {
                Console.Write("*");
            }
            Console.WriteLine();
            for(int i = 0; i < Height; i++)
            {
                for(int j = 0; j < Width; j++)
                {
                    Console.Write(screen[j, i] ? "#" : ".");
                }
                Console.WriteLine();
            }
        }

        private static void ProcessYRotate(string line, ref bool [,] screen)
        {
            var split = line.Split('=')[1];
            var split2 = split.Split(new string[] { " by " }, StringSplitOptions.None);

            int row = int.Parse(split2[0]);
            int num = int.Parse(split2[1]);

            for (int i = 0; i < num; i++)
            {
                bool last = false;
                for (int k = 0; k < Height; k++)
                {
                    if (i == 0 && k == 0)
                    {
                        last = screen[row, k];
                        screen[row, k] = screen[row, Height - 1];
                        continue;
                    }
                    var tmpLast = screen[row, k];
                    screen[row, k] = last;
                    last = tmpLast;
                }
            }
        }

        private static void ProcessXRotate(string line, ref bool [,] screen)
        {
            var split = line.Split('=')[1];
            var split2 = split.Split(new string[] { " by " }, StringSplitOptions.None);

            int row = int.Parse(split2[0]);
            int num = int.Parse(split2[1]);

            for(int i = 0; i < num; i++)
            {
                bool last = false;
                for (int k = 0; k < Width; k++)
                {
                    if (i == 0 && k == 0)
                    {
                        last = screen[k, row];
                        screen[k, row] = screen[Width - 1, row];
                        continue;
                    }
                    var tmpLast = screen[k, row];
                    screen[k, row] = last;
                    last = tmpLast;
                }
            }
        }

        private static void ProcessRect(string line, ref bool[,] screen)
        {
            var split = line.Replace("rect", "").Trim().Split('x');
            int x = int.Parse(split[0]);
            int y = int.Parse(split[1]);

            for (int i = 0; i < x; i++)
            {
                for(int j = 0; j < y; j++)
                {
                    screen[i, j] = true;
                }
            }
        }
    }
}

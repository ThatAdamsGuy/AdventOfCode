using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day02
    {
        private const int redCubes = 12;
        private const int greenCubes = 13;
        private const int blueCubes = 14;

        public enum Colour { Red, Blue, Green}
        public static void Run()
        {
            int sum = 0;
            int powerSum = 0;
            foreach(var line in File.ReadAllLines("Day02Input.txt"))
            {
                var firstSplit = line.Split(':');
                int id = int.Parse(firstSplit[0].Split(' ')[1]);
                var rounds = firstSplit[1].Split(';');

                int minRed = 0;
                int minGreen = 0;
                int minBlue = 0;

                bool validGame = true;
                foreach(var round in rounds)
                {
                    var cubes = round.Split(',');
                    foreach(var group in cubes)
                    {
                        var finalSplit = group.Trim().Split(' ');
                        int count = int.Parse(finalSplit[0]);
                        if (finalSplit[1] == "red")
                        {
                            if (count > redCubes) validGame = false;
                            if (count > minRed) minRed = count;
                        }
                        else if (finalSplit[1] == "blue")
                        {
                            if (count > blueCubes) validGame = false;
                            if (count > minBlue) minBlue = count;
                        }
                        else if (finalSplit[1] == "green")
                        {
                            if (count > greenCubes) validGame = false;
                            if (count > minGreen) minGreen= count;
                        }
                    }
                }
                if (validGame) sum += id;
                powerSum += (minRed * minGreen * minBlue);
            }
            Console.WriteLine("Part 1 - " + sum);
            Console.WriteLine("Part 2 - " + powerSum);
        }
    }
}

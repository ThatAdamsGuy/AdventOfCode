using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    internal class Day02
    {
        public static void Run()
        {
            var input = File.ReadAllLines("Day02Input.txt");

            int aim = 0;
            int horPos = 0;
            int partOneDepth = 0;
            int partTwoDepth = 0;

            foreach (var line in input)
            {
                var split = line.Split(' ');
                switch (split[0])
                {
                    case "forward":
                        horPos += int.Parse(split[1]);
                        partTwoDepth += (aim * int.Parse(split[1]));
                        break;
                    case "up":
                        partOneDepth -= int.Parse(split[1]);
                        aim -= int.Parse(split[1]);
                        break;
                    case "down":
                        partOneDepth += int.Parse(split[1]);
                        aim += int.Parse(split[1]);
                        break;
                    default:
                        break;
                }
            }

            Console.WriteLine("Part 1 - " + (horPos * partOneDepth));
            Console.WriteLine("Part 2 - " + (horPos * partTwoDepth));
        }
    }
}

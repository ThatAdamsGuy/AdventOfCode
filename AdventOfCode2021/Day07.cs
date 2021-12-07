using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    internal class Day07
    {
        public static void Run()
        {
            List<int> inputs = new List<int>(File.ReadAllLines("Day07Input.txt").First().Split(',').Select(x => int.Parse(x)));
            int min = inputs.Min();
            int max = inputs.Max();

            int part1Counter = 0;
            int part2Counter = 0;
            int part1Fuel = int.MaxValue;
            int part2Fuel = int.MaxValue;
            for(int i = min; i <= max; i++)
            {
                part1Counter = 0;
                part2Counter = 0;
                foreach(int crab in inputs)
                {
                    if(crab > i)
                    {
                        part1Counter += (crab - i);
                        part2Counter += SumFromOne(crab - i);
                    } 
                    else if (crab < i)
                    {
                        part1Counter += (i - crab);
                        part2Counter += SumFromOne(i - crab);
                    }
                    if(part1Counter > part1Fuel && part2Counter > part2Fuel)
                    {
                        break;
                    }
                }
                if(part1Counter < part1Fuel)
                {
                    part1Fuel = part1Counter;
                }
                if(part2Counter < part2Fuel)
                {
                    part2Fuel = part2Counter;
                }
            }
            Console.WriteLine($"Part 1 - {part1Fuel}");
            Console.WriteLine($"Part 2 - {part2Fuel}");
        }

        //It really feels like there should be a better way to do this...
        private static int SumFromOne(int final)
        {
            int sum = 0;
            for(int i = 1; i <= final; i++)
            {
                sum += i;
            }
            return sum;
        }
    }
}

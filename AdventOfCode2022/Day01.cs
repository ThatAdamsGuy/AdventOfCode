using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day01
    {
        public static void Run()
        {
            int sum = 0;
            List<int> elves = new List<int>();
            foreach (string line in File.ReadLines("Day01Input.txt"))
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    elves.Add(sum);
                    sum = 0;
                    continue;
                }

                sum += int.Parse(line);
            }

            elves.Sort();
            Console.WriteLine("Part 1: " + elves.Last());
            Console.WriteLine("Part 2: " + (elves[elves.Count - 1] + elves[elves.Count - 2] + elves[elves.Count - 3]));
        }
    }
}

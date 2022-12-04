using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day03
    {
        public static void Run()
        {
            int p1Sum = 0;
            int p2Sum = 0;
            int counter = 0;
            List<string> group = new List<string>();

            foreach (string line in File.ReadLines("Day03Input.txt"))
            {
                counter++;
                group.Add(line);

                var first = line.Substring(0, (int)(line.Length / 2));
                var last = line.Substring((int)(line.Length / 2), (int)(line.Length / 2));

                var common = first.Intersect(last);
                foreach(char c in common)
                {
                    p1Sum += GetValue(c);
                }

                if(counter == 3)
                {
                    common = (group[0].Intersect(group[1])).Intersect(group[2]);
                    foreach (char c in common)
                    {
                        p2Sum += GetValue(c);
                    }

                    counter = 0;
                    group = new List<string>();
                }
            }
            Console.WriteLine("Part 1 - " + p1Sum);
            Console.WriteLine("Part 2 - " + p2Sum);
        }

        private static int GetValue(char c)
        {
            if (char.IsUpper(c))
                return c - 38;
            else
                return c - 96;
        }
    }
}

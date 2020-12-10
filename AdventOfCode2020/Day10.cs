using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day10
    {
        public static List<int> inputs { get; set; }

        public static void Run()
        {
            List<string> strings = File.ReadAllLines("day10Known.txt").ToList();
            inputs = new List<int>();
            foreach(string item in strings)
            {
                inputs.Add(int.Parse(item));
            }
            inputs.Add(0);
            inputs.Add(inputs.Max() + 3);
            inputs.Sort();
            int oneJoltDiffereces = 0;
            int threeJoltDifferences = 0;
            for(int i = 0; i < inputs.Count(); i++)
            {
                if(i == inputs.Count() - 1)
                {
                    //Last one, add your device
                    threeJoltDifferences++;
                    break;
                }
                if(inputs[i + 1] - inputs[i] == 3)
                {
                    threeJoltDifferences++;
                } else if (inputs[i + 1] - inputs[i] == 1)
                {
                    oneJoltDiffereces++;
                }
            }
            Console.WriteLine($"3-Jolts: {threeJoltDifferences}. 1-Jolts: {oneJoltDiffereces}. Multiplied: {threeJoltDifferences * oneJoltDiffereces}");
            Console.WriteLine($"Total Possibilities: {Part2(0)}");

        }

        public static long Part2(int pos)
        {
            if (pos == inputs.Count())
            {
                return 1;
            }

            long result = 0;
            if (pos + 1 < inputs.Count() && inputs.Contains(inputs[pos+1])) result += Part2(pos+1);
            if (pos + 2 < inputs.Count() && inputs.Contains(inputs[pos+2])) result += Part2(pos+2);
            if (pos + 3 < inputs.Count() && inputs.Contains(inputs[pos+3])) result += Part2(pos+3);
            return result;
        }
    }
}

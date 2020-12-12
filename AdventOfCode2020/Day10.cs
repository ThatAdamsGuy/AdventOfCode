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
            List<string> strings = File.ReadAllLines("day10Input.txt").ToList();
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

            Dictionary<int, long> cache = new Dictionary<int, long>();
            for (int i = inputs.Count() - 1; i >= 0; i--)
            {
                long possibilities = 0;
                int pos = i + 1;
                while (true)
                {
                    if (pos == inputs.Count())
                    {
                        possibilities = 1;
                        break;
                    }
                    else
                    {
                        if (inputs[pos] > inputs[i] + 3)
                        {
                            break;
                        }
                        else
                        {
                            possibilities += cache[pos];
                            pos++;
                        }
                    }
                }
                cache.Add(i, possibilities);
            }
            Console.WriteLine($"Total Possibilities: {cache[0]}");
        }
    }
}

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    class Day06
    {
        public static void Run()
        {
            List<string> inputs = File.ReadAllLines("day06Input.txt").ToList();
            List<Dictionary<char,int>> counters = new List<Dictionary<char,int>>();
            for(int i = 0; i < inputs.First().Length; i++)
            {
                counters.Add(new Dictionary<char, int>());
            }
            foreach(string line in inputs)
            {
                for(int i = 0; i < line.Length; i++)
                {
                    if (counters[i].ContainsKey(line[i])){
                        counters[i][line[i]]++;
                    } else
                    {
                        counters[i].Add(line[i], 1);
                    }
                }
            }

            string mostCommonResult = "";
            string leastCommonResult = "";
            for (int i = 0; i < counters.Count(); i++)
            {
                var ordered = counters[i].OrderByDescending(s => s.Value);
                mostCommonResult += ordered.First().Key;
                leastCommonResult += ordered.Last().Key;
            }
            Console.WriteLine($"Most Common Decoded message: {mostCommonResult}");
            Console.WriteLine($"Least Common Decoded message: {leastCommonResult}");
        }
    }
}

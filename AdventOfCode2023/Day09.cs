using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day09
    {
        public static void Run()
        {
            var input = File.ReadAllLines("day09input.txt");
            long pt1Sum = 0;
            long pt2Sum = 0;
            foreach (var line in input)
            {
                List<List<int>> patterns = new List<List<int>>();
                var prev = line.Split(' ').Select(x => int.Parse(x)).ToList();
                patterns.Add(prev);
                int lastDiff = 0;
                int firstDiff = 0;
                while (true)
                {
                    List<int> diffResult = new List<int>();
                    for (int i = 0; i < patterns.Last().Count - 1; i++)
                    {
                        diffResult.Add(prev[i + 1] - prev[i]);
                    }
                    patterns.Add(diffResult);
                    var distinct = diffResult.Distinct();
                    if (distinct.Count() == 1)
                    {
                        lastDiff = distinct.Last();
                        firstDiff = distinct.First();
                        break;
                    }
                    else
                    {
                        prev = diffResult;
                    }
                }

                for (int i = patterns.Count - 1; i > 0; i--)
                {
                    var current = patterns[i];
                    var previous = patterns[i - 1];
                    previous.Add(previous.Last() + lastDiff);
                    lastDiff = previous.Last();
                    previous.Insert(0, previous.First() - firstDiff);
                    firstDiff = previous.First();
                }

                pt1Sum += patterns.First().Last();
                pt2Sum += patterns.First().First();
            }
            Console.WriteLine("Part 1 - " + pt1Sum);
            Console.WriteLine("Part 2 - " + pt2Sum);
        }
    }
}

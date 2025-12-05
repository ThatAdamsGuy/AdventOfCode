using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025
{
    internal class Day05
    {
        public static void Run()
        {
            var input = File.ReadAllLines("day05input.txt");
            var ranges = new List<Tuple<long, long>>();
            var ids = new List<long>();

            int freshCounter = 0;
            foreach(var line in input)
            {
                if (line.Contains("-"))
                {
                    var splitLine = line.Split('-');
                    ranges.Add(Tuple.Create(long.Parse(splitLine[0]), long.Parse(splitLine[1])));
                }
                else
                {
                    if(string.IsNullOrWhiteSpace(line))
                        continue;
                    ids.Add(long.Parse(line));
                }
            }

            foreach(var id in ids)
            {
                if(ranges.Any(x => id >= x.Item1 && id <= x.Item2))
                    freshCounter++;
            }
            Console.WriteLine($"Part 1: {freshCounter}");

            var mergedRanges = MergeRanges(ranges);
            long part2 = 0;
            foreach (var range in mergedRanges)
            {
                part2 += (range.Item2 - range.Item1 + 1);
            }
            Console.WriteLine($"Part 2: {part2}");
        }

        private static List<Tuple<long, long>> MergeRanges(List<Tuple<long, long>> ranges)
        {
            if (ranges.Count == 0) return new List<Tuple<long, long>>();

            // Sort ranges by start position
            var sortedRanges = ranges.OrderBy(r => r.Item1).ToList();
            var merged = new List<Tuple<long, long>>();

            var current = sortedRanges[0];

            for (int i = 1; i < sortedRanges.Count; i++)
            {
                var next = sortedRanges[i];

                // Check if ranges overlap or are adjacent
                if (next.Item1 <= current.Item2 + 1)
                {
                    // Merge ranges
                    current = Tuple.Create(current.Item1, Math.Max(current.Item2, next.Item2));
                }
                else
                {
                    // No overlap, add current and move to next
                    merged.Add(current);
                    current = next;
                }
            }

            merged.Add(current);
            return merged;
        }
    }
}
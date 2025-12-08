using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day05
    {
        public static void Run()
        {
            var maps = new List<Map>();
            // Parse Inputs
            var lines = File.ReadAllLines("Day05input.txt").ToList();
            var groups = SplitByEmptyLines(lines);
            var seeds = groups[0].First().Split(' ').Skip(1).Select(x => long.Parse(x)).ToList();
            groups.RemoveAt(0);
            foreach (var group in groups)
            {
                var map = new Map();
                for (int i = 0; i < group.Count; i++)
                {
                    if (i == 0)
                    {
                        var split = group[i].Split(' ')[0].Split('-');
                        map.Source = split[0];
                        map.Destination = split[2];
                    }
                    else
                    {
                        var split = group[i].Split(' ');
                        map.SourceRanges.Add(new Mapping(long.Parse(split[1]), long.Parse(split[2])));
                        map.DestinationRanges.Add(new Mapping(long.Parse(split[0]), long.Parse(split[2])));
                    }
                }
                maps.Add(map);
            }

            Console.WriteLine("Part 1 - " + CalculateLowestPosition(seeds, maps));

            // Part 2: Convert seed pairs to ranges
            var seedRanges = new List<Range>();
            for (int i = 0; i < seeds.Count; i += 2)
            {
                seedRanges.Add(new Range(seeds[i], seeds[i + 1]));
            }

            Console.WriteLine("Part 2 - " + CalculateLowestPositionFromRanges(seedRanges, maps));
        }

        private static long CalculateLowestPosition(List<long> seeds, List<Map> maps)
        {
            var location = long.MaxValue;
            foreach (var seed in seeds)
            {
                string state = "seed";
                long currentPos = seed;
                while (true)
                {
                    if (state == "location")
                    {
                        location = Math.Min(currentPos, location);
                        break;
                    }
                    else
                    {
                        var map = maps.Where(x => x.Source == state).Single();
                        var sourceMapping = map.SourceRanges.Where(x => currentPos >= x.Start && currentPos <= x.End).SingleOrDefault();
                        if (sourceMapping != null)
                        {
                            var index = map.SourceRanges.IndexOf(sourceMapping);
                            var distance = currentPos - sourceMapping.Start;
                            var destMapping = map.DestinationRanges[index];
                            currentPos = destMapping.Start + distance;
                        }
                        state = map.Destination;
                    }
                }
            }
            return location;
        }

        private static long CalculateLowestPositionFromRanges(List<Range> seedRanges, List<Map> maps)
        {
            var currentRanges = seedRanges;
            string state = "seed";

            while (state != "location")
            {
                var map = maps.Where(x => x.Source == state).Single();
                currentRanges = TransformRanges(currentRanges, map);
                state = map.Destination;
            }

            return currentRanges.Min(r => r.Start);
        }

        private static List<Range> TransformRanges(List<Range> inputRanges, Map map)
        {
            var resultRanges = new List<Range>();

            foreach (var inputRange in inputRanges)
            {
                var unmappedRanges = new List<Range> { inputRange };

                // Apply each mapping rule
                for (int i = 0; i < map.SourceRanges.Count; i++)
                {
                    var sourceMapping = map.SourceRanges[i];
                    var destMapping = map.DestinationRanges[i];
                    var newUnmappedRanges = new List<Range>();

                    foreach (var unmappedRange in unmappedRanges)
                    {
                        // Find intersection
                        long overlapStart = Math.Max(unmappedRange.Start, sourceMapping.Start);
                        long overlapEnd = Math.Min(unmappedRange.End, sourceMapping.End);

                        if (overlapStart <= overlapEnd)
                        {
                            // There's an overlap - transform it
                            long offset = destMapping.Start - sourceMapping.Start;
                            resultRanges.Add(new Range(overlapStart + offset, overlapEnd - overlapStart + 1));

                            // Add non-overlapping parts back to unmapped
                            if (unmappedRange.Start < overlapStart)
                            {
                                newUnmappedRanges.Add(new Range(unmappedRange.Start, overlapStart - unmappedRange.Start));
                            }
                            if (overlapEnd < unmappedRange.End)
                            {
                                newUnmappedRanges.Add(new Range(overlapEnd + 1, unmappedRange.End - overlapEnd));
                            }
                        }
                        else
                        {
                            // No overlap - keep as unmapped
                            newUnmappedRanges.Add(unmappedRange);
                        }
                    }

                    unmappedRanges = newUnmappedRanges;
                }

                // Add any remaining unmapped ranges (they pass through unchanged)
                resultRanges.AddRange(unmappedRanges);
            }

            return resultRanges;
        }

        private static List<List<string>> SplitByEmptyLines(List<string> lines)
        {
            var groups = new List<List<string>>();
            var currentGroup = new List<string>();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    if (currentGroup.Count > 0)
                    {
                        groups.Add(currentGroup);
                        currentGroup = new List<string>();
                    }
                }
                else
                {
                    currentGroup.Add(line);
                }
            }

            if (currentGroup.Count > 0)
            {
                groups.Add(currentGroup);
            }

            return groups;
        }

        class Map
        {
            public string Source { get; set; }
            public string Destination { get; set; }
            public List<Mapping> SourceRanges { get; set; }
            public List<Mapping> DestinationRanges { get; set; }

            public Map()
            {
                SourceRanges = new List<Mapping>();
                DestinationRanges = new List<Mapping>();
            }
        }

        class Mapping
        {
            public long Start { get; set; }
            public long Length { get; set; }

            public long End => Start + Length - 1;

            public Mapping(long start, long length)
            {
                Start = start;
                Length = length;
            }
        }

        class Range
        {
            public long Start { get; set; }
            public long Length { get; set; }

            public long End => Start + Length - 1;

            public Range(long start, long length)
            {
                Start = start;
                Length = length;
            }
        }
    }
}
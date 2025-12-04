using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2025
{
    internal class Day02
    {
        public static void Run()
        {
            // Regex to match entire string made of repeated patterns
            var repeatedPatternRegex = new Regex(@"^(.+?)\1+$");

            var input = File.ReadAllLines("day02input.txt").First();
            var split = input.Split(',');
            long part1sum = 0;
            long part2sum = 0;
            foreach (var item in split)
            {
                var newSplit = item.Split('-');
                var lowerBound = long.Parse(newSplit[0]);
                var upperBound = long.Parse(newSplit[1]);

                for (var currentId = lowerBound; currentId <= upperBound; currentId++)
                {
                    var idString = currentId.ToString();

                    // Part 2: Check if entire string is made of repeated pattern
                    if (repeatedPatternRegex.IsMatch(idString))
                    {
                        part2sum += currentId;
                    }

                    // Part 1: Check for identical halves (only even length numbers)
                    if (idString.Length % 2 == 0)
                    {
                        var firstHalf = idString.Substring(0, idString.Length / 2);
                        var secondHalf = idString.Substring(idString.Length / 2);

                        if (firstHalf.Equals(secondHalf))
                        {
                            part1sum += currentId;
                        }
                    }
                }
            }
            Console.WriteLine("Part 1: " + part1sum);
            Console.WriteLine("Part 2: " + part2sum);
        }
    }
}

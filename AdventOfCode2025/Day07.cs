using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025
{
    internal class Day07
    {
        private static char[][] grid;

        public static void Run()
        {
            grid = File.ReadAllLines("day07input.txt").Select(line => line.ToCharArray()).ToArray();
            
            // Count splits
            HashSet<int> beams = new HashSet<int>();
            int splits = 0;
            var startingIndex = Array.IndexOf(grid[0], 'S');
            beams.Add(startingIndex);
            for(int i = 1; i < grid.Length; i++){
                var line = grid[i];
                List<int> removals = new List<int>();
                List<int> adds = new List<int>();
                foreach (var beam in beams)
                {
                    if (beam >= 0 && beam < line.Length && line[beam] == '^')
                    {
                        removals.Add(beam);
                        adds.Add(beam - 1);
                        adds.Add(beam + 1);
                        splits++;
                    }  
                }
                foreach(var beam in removals)
                {
                    beams.Remove(beam);
                }
                foreach(var beam in adds)
                {
                    beams.Add(beam);
                }
            }
            Console.WriteLine("Part 1 - " + splits);

            // Count all possible paths
            var startCol = Array.IndexOf(grid[0], 'S');
            long pathCount = CountPaths(0, startCol);
            Console.WriteLine("Part 2 - " + pathCount);
        }

        private static Dictionary<string, long> memory = new Dictionary<string, long>();

        private static long CountPaths(int row, int col)
        {
            string key = $"{row},{col}";
            if (memory.ContainsKey(key))
                return memory[key];

            // End
            if (row >= grid.Length - 1)
            {
                memory[key] = 1;
                return 1;
            }

            // Next Row
            if (col < 0 || col >= grid[row + 1].Length)
            {
                memory[key] = 0;
                return 0;
            }

            char nextChar = grid[row + 1][col];
            long result;

            // Splitter
            if (nextChar == '^')
            {
                result = CountPaths(row + 1, col - 1) + CountPaths(row + 1, col + 1);
            }
            else
            {
                result = CountPaths(row + 1, col);
            }

            memory[key] = result;
            return result;
        }
    }
}

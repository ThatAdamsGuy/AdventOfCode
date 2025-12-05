using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace AdventOfCode2025
{
    internal class Day04
    {
        public static void Run()
        {
            var rows = File.ReadAllLines("day04input.txt");
            char[][] grid = rows.Select(row => row.ToCharArray()).ToArray();
            int total = 0;
            int prevTotal = 0;
            int iterations = 0;
            do
            {
                prevTotal = total;
                int result = RunLoop(grid);
                total += result;
                iterations++;
                if (iterations == 1)
                {
                    Console.WriteLine("Part 1 - " + total);
                }
            } while (total != prevTotal);
            Console.WriteLine("Part 2 - " + total);
        }

        private static bool IsValidPosition(int row, int col, char[][] grid)
        {
            return row >= 0 && row < grid.Length &&
                   col >= 0 && col < grid[0].Length;
        }

        private static int RunLoop(char[][] grid)
        {
            int removals = 0;
            // Direction arrays for 8 surrounding positions
            int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (grid[i][j] != '@') continue;

                    int surroundingRolls = 0;

                    // Check all 8 surrounding positions
                    for (int dir = 0; dir < 8; dir++)
                    {
                        int newRow = i + dx[dir];
                        int newCol = j + dy[dir];

                        // Check bounds and condition
                        if (IsValidPosition(newRow, newCol, grid) &&
                            grid[newRow][newCol] == '@') // Replace with your condition
                        {
                            surroundingRolls++;
                        }
                    }
                    if (surroundingRolls < 4)
                    {
                        removals++;
                        grid[i][j] = '.';
                    }
                }
            }
            return removals;
        }
    }
}



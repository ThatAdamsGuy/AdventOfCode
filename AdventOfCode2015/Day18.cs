using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
    class Day18
    {
        public static void Run()
        {
            string[] lines = File.ReadAllLines("Day18Input.txt"); 
            char[][] grid = File.ReadAllLines("Day18Input.txt")
                               .Select(line => line.ToCharArray())
                               .ToArray();
            for (int i = 0; i < 100; i++)
            {
                grid = RunLoop(grid, false);
            }

            int lightsOn = CountLights(grid);
            Console.WriteLine($"Part 1 - {lightsOn}");

            grid = File.ReadAllLines("Day18Input.txt")
                               .Select(line => line.ToCharArray())
                               .ToArray();
            grid[0][0] = '#';
            grid[0][99] = '#';
            grid[99][0] = '#';
            grid[99][99] = '#';

            for (int i = 0; i < 100; i++)
            {
                grid = RunLoop(grid, true);
            } 
            lightsOn = CountLights(grid);
            Console.WriteLine($"Part 2 - {lightsOn}");
        }

        private static char[][] RunLoop(char[][] grid, bool stuckCorners)
        {
            char[][] newGrid = new char[grid.Length][];
            for (int i = 0; i < grid.Length; i++)
            {
                newGrid[i] = new char[grid[i].Length];
            }

            // Direction arrays for 8 surrounding positions
            int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (stuckCorners && IsCorner(i, j))
                    {
                        newGrid[i][j] = grid[i][j];
                        continue;
                    }

                    int surroundingLights = 0;
                    // Check all 8 surrounding positions
                    for (int dir = 0; dir < 8; dir++)
                    {
                        int newRow = i + dx[dir];
                        int newCol = j + dy[dir];

                        // Check bounds and condition
                        if (IsValidPosition(newRow, newCol, grid) &&
                            grid[newRow][newCol] == '#') // Replace with your condition
                        {
                            surroundingLights++;
                        }
                    }
                    if (grid[i][j] == '#' && surroundingLights != 2 && surroundingLights != 3)
                    {
                        newGrid[i][j] = '.';
                    }
                    else if (grid[i][j] == '.' && surroundingLights == 3)
                    {
                        newGrid[i][j] = '#';
                    }
                    else
                    {
                        newGrid[i][j] = grid[i][j];
                    }
                }
            }
            return newGrid;
        }

        private static bool IsCorner(int i, int j)
        {
            return (i == 0 && j == 0)
                || (i == 0 && j == 99)
                || (i == 99 && j == 0)
                || (i == 99 && j == 99);
        }

        private static int CountLights(char[][] grid)
        {
            int count = 0;
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (grid[i][j] == '#')
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        private static bool IsValidPosition(int row, int col, char[][] grid)
        {
            return row >= 0 && row < grid.Length &&
                   col >= 0 && col < grid[0].Length;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    internal class Day09
    {
        public static List<Tuple<int, int>> lowPoints = new List<Tuple<int, int>>();

        public static void Run()
        {
            var input = File.ReadAllLines("Day09Input.txt");
            int rowLength = input.First().Length;
            int[,] grid = new int[input.Length, rowLength];

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < rowLength; j++)
                {
                    grid[i, j] = int.Parse(input[i][j].ToString());
                }
            }

            int sum = 0;
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < rowLength; j++)
                {
                    int cur = grid[i, j];
                    if(i == 0)
                    {
                        if(j == 0)
                        {
                            //TopLeftCorner
                            if(cur < grid[i, j + 1] && cur < grid[i + 1, j])
                            {
                                sum += cur + 1;
                                lowPoints.Add(new Tuple<int, int>(i, j));
                            }
                        } else if (j == rowLength - 1)
                        {
                            //TopRightCorner
                            if(cur < grid[i, j - 1] && cur < grid[i + 1, j])
                            {
                                sum += cur + 1;
                                lowPoints.Add(new Tuple<int, int>(i, j));
                            }
                        } else
                        {
                            //Top Row
                            if (cur < grid[i, j + 1] && cur < grid[i, j - 1] && cur < grid[i + 1, j])
                            {

                                sum += cur + 1;
                                lowPoints.Add(new Tuple<int, int>(i, j));
                            }
                        }
                    } else if (i == input.Length - 1)
                    {
                        if (j == 0)
                        {
                            //BottomLeftCorner
                            if(cur < grid[i, j + 1] && cur < grid[i - 1, j])
                            {
                                sum += cur + 1;
                                lowPoints.Add(new Tuple<int, int>(i, j));
                            }
                        }
                        else if (j == rowLength - 1)
                        {
                            //BottomRightCorner
                            if (cur < grid[i, j - 1] && cur < grid[i - 1, j])
                            {
                                sum += cur + 1;
                                lowPoints.Add(new Tuple<int, int>(i, j));
                            }
                        }
                        else
                        {
                            //Bottom Row
                            if (cur < grid[i, j + 1] && cur < grid[i, j - 1] && cur < grid[i - 1, j])
                            {
                                sum += cur + 1;
                                lowPoints.Add(new Tuple<int, int>(i, j));
                            }
                        }
                    } else
                    {
                        if (j == 0)
                        {
                            //Left Edge
                            if (cur < grid[i, j + 1] && cur < grid[i + 1, j] && cur < grid[i - 1, j])
                            {
                                sum += cur + 1;
                                lowPoints.Add(new Tuple<int, int>(i, j));
                            }
                        }
                        else if (j == rowLength - 1)
                        {
                            //Right Edge
                            if (cur < grid[i, j - 1] && cur < grid[i + 1, j] && cur < grid[i - 1, j])
                            {
                                sum += cur + 1;
                                lowPoints.Add(new Tuple<int, int>(i, j));
                            }
                        }
                        else
                        {
                            //Middle
                            if (cur < grid[i, j + 1] && cur < grid[i, j - 1] && cur < grid[i + 1, j] && cur < grid[i - 1, j])
                            {
                                sum += cur + 1;
                                lowPoints.Add(new Tuple<int, int>(i, j));
                            }
                        }
                    }
                }
            }
            Console.WriteLine("Part 1 - " + sum);
            Console.WriteLine(lowPoints.Count());

            List<int> results = new List<int>();

            while(lowPoints.Count() != 0)
            {
                var next = lowPoints.First();
                lowPoints.RemoveAt(0);
                results.Add(CheckPoint(grid, new List<Tuple<int, int>>(), next.Item1, next.Item2, null, null, input.Length , rowLength));
            }

            results.Sort();
            results.Reverse();
            Console.WriteLine($"Part 2 - {results[0]} * {results[1]} * {results[2]} = {results[0] * results[1] * results[2]}");
        }

        private static int CheckPoint(int[,] grid, List<Tuple<int, int>> visited, int row, int col, int? prevRow, int? prevCol, int rowCount, int colCount)
        {
            int sum = 0;

            if (visited.Any(x => x.Item1 == row && x.Item2 == col))
            {
                return sum;
            } else
            {
                visited.Add(new Tuple<int, int>(row, col));
            }

            //Check North
            if (row != 0 && grid[row - 1, col] != 9)
            {
                sum += CheckPoint(grid, visited, row - 1, col, row, col, rowCount, colCount);
            }

            //Check East
            if (col != colCount - 1 && grid[row, col + 1] != 9)
            {
                sum += CheckPoint(grid, visited, row, col + 1, row, col, rowCount, colCount);
            }

            //Check South
            if (row != rowCount - 1 && grid[row + 1, col] != 9)
            {
                sum += CheckPoint(grid, visited, row + 1, col, row, col, rowCount, colCount);
            }

            //Check West
            if (col != 0 && grid[row, col - 1] != 9)
            {
                sum += CheckPoint(grid, visited, row, col - 1, row, col, rowCount, colCount);
            }

            return sum + 1;
        }
    }
}

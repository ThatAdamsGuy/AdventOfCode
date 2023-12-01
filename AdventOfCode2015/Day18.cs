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
            bool[,] grid = new bool[100, 100];
            var input = File.ReadAllLines("Day18Input.txt");

            //Setup
            for(int i = 0; i < input.Length; i++)
            {
                for(int k = 0; k < input[i].Length; k++)
                {
                    if((input[i])[k] == '#')
                    {
                        grid[i, k] = true;
                    } else
                    {
                        grid[i, k] = false;
                    }
                }
            }

            for (int loops = 0; loops < 100; loops ++)
            {
                bool[,] tmpGrid = new bool[100, 100];
                for(int i = 0; i < 100; i++)
                {
                    for(int j = 0; j < 100; j++)
                    {
                        List<bool> neighbours = new List<bool>();
                        if(i == 0)
                        {
                            //Top Row
                            neighbours.Add(grid[j, i + 1]);
                            if(j == 0)
                            {
                                //Top left corner
                                neighbours.Add(grid[j + 1, i]);
                                neighbours.Add(grid[j + 1, i + 1]);
                            }
                            else if (j == 99)
                            {
                                //Top right corner
                                neighbours.Add(grid[j - 1, i]);
                                neighbours.Add(grid[j - 1, i + 1]);
                            }
                            else
                            {
                                //Top row
                                neighbours.Add(grid[j - 1, i]);
                                neighbours.Add(grid[j - 1, i + 1]);
                                neighbours.Add(grid[j + 1, i]);
                                neighbours.Add(grid[j + 1, i + 1]);
                            }
                        }
                        else if (i == 99)
                        {
                            //Bottom Row
                            neighbours.Add(grid[j, i - 1]);
                            if (j == 0)
                            {
                                //Bottom left corner
                                neighbours.Add(grid[j + 1, i]);
                                neighbours.Add(grid[j + 1, i - 1]);
                            }
                            else if (j == 99)
                            {
                                //Bottom right corner
                                neighbours.Add(grid[j - 1, i]);
                                neighbours.Add(grid[j - 1, i - 1]);
                            }
                            else
                            {
                                //Bottom row
                                neighbours.Add(grid[j - 1, i]);
                                neighbours.Add(grid[j - 1, i - 1]);
                                neighbours.Add(grid[j + 1, i]);
                                neighbours.Add(grid[j + 1, i - 1]);
                            }
                        }
                        else if (j == 0)
                        {
                            //Left column
                            neighbours.Add(grid[j, i - 1]);
                            neighbours.Add(grid[j + 1, i - 1]);
                            neighbours.Add(grid[j + 1, i]);
                            neighbours.Add(grid[j + 1, i + 1]);
                            neighbours.Add(grid[j, i + 1]);
                        }
                        else if (j == 99)
                        {
                            //Right column
                            neighbours.Add(grid[j, i - 1]);
                            neighbours.Add(grid[j - 1, i - 1]);
                            neighbours.Add(grid[j - 1, i]);
                            neighbours.Add(grid[j - 1, i + 1]);
                            neighbours.Add(grid[j, i + 1]);
                        }
                        else
                        {
                            //Middle of the grid
                            neighbours.Add(grid[j, i - 1]);
                            neighbours.Add(grid[j - 1, i - 1]);
                            neighbours.Add(grid[j - 1, i]);
                            neighbours.Add(grid[j - 1, i + 1]);
                            neighbours.Add(grid[j, i + 1]);
                            neighbours.Add(grid[j + 1, i - 1]);
                            neighbours.Add(grid[j + 1, i]);
                            neighbours.Add(grid[j + 1, i + 1]);
                        }

                        tmpGrid[i, j] = ProcessNeighbours(grid[i, j], neighbours);
                    }
                }
                
                grid = tmpGrid;
            }

            Console.WriteLine("Part 1 - " + grid.Cast<bool>().Count(x => x));
        }

        private static bool ProcessNeighbours(bool curValue, List<bool> neighbours)
        {
            if (curValue)
            {
                if(neighbours.Count(x => x) == 2 || neighbours.Count(x => x) == 3)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if(neighbours.Count(x => x) == 3)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}

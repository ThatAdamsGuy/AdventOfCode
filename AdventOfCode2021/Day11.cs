using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    internal class Day11
    {
        public static void Run()
        {
            var inputs = File.ReadAllLines("Day11Input.txt");
            Octopus[,] grid = new Octopus[10, 10];

            //Setup the grid
            for(int i = 0; i < inputs.Length; i++)
            {
                for(int j = 0; j < inputs.Length; j++)
                {
                    grid[i, j] = new Octopus(inputs[i][j]);
                }
            }

            long flashes = 0;
            int count = 0;
            while (true) { 
                //set the grid
                bool stepComplete = false; 
                for (int j = 0; j < 10; j++)
                {
                    for (int k = 0; k < 10; k++)
                    {
                        if(grid[j, k].hasFired)
                        {
                            grid[j, k].energy = 0;
                        }
                        grid[j, k].energy++;
                        grid[j, k].hasFired = false;
                    }
                }

                while (!stepComplete)
                {
                    stepComplete = true;
                    for (int j = 0; j < 10; j++)
                    {
                        for (int k = 0; k < 10; k++)
                        {
                            if(!grid[j, k].hasFired && grid[j, k].energy > 9)
                            {
                                //Octopus hasn't fired and energy above 9 - take cover!
                                flashes += FireOctopus(j, k, grid);
                                stepComplete = false;
                            }
                        }
                    }
                }
                count++;

                if(count == 100)
                {
                    Console.WriteLine("Part 1 - " + flashes);
                }

                bool inSync = true;
                for (int i = 0; i < inputs.Length; i++)
                {
                    for (int j = 0; j < inputs.Length; j++)
                    {
                        if(!grid[i, j].hasFired)
                        {
                            inSync = false;
                            break;
                        }
                    }
                    if (!inSync)
                    {
                        break;
                    }
                }
                if (inSync)
                {
                    break;
                }
            }
            Console.WriteLine("Part 2 - " + count);
        }

        private static long FireOctopus(int row, int col, Octopus[,] grid)
        {
            grid[row, col].energy = 0;
            grid[row, col].hasFired = true;
            long sum = 0;

            for(int i = row - 1; i < row + 2; i++)
            {
                //Grid position out of bounds
                if(i < 0)
                {
                    continue;
                } else if (i == 10)
                {
                    continue;
                }



                for(int j = col - 1; j < col + 2; j++)
                {
                    //Grid position out of bounds
                    if(j < 0)
                    {
                        continue;
                    }
                    else if (j >= 10)
                    {
                        continue;
                    }
                    if(i == row && j == col)
                    {
                        continue;
                    }

                    grid[i, j].energy++;

                    //Check if this octopus has been triggered by the previous firing
                    if(!grid[i,j].hasFired && grid[i, j].energy > 9)
                    {
                        sum += FireOctopus(i, j, grid);
                    }
                }
            }
            return sum + 1;
        }
    }

    public class Octopus
    {
        public int energy;
        public bool hasFired;

        public Octopus(char startValue)
        {
            energy = int.Parse(startValue.ToString());
            hasFired = false;
        }
    }
}

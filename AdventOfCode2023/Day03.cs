using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day03
    {
        public static void Run()
        {
            List<string> log = new List<string>();

            var input = File.ReadAllLines("Day03Input.txt");
            var len = input.First().Length;
            char[,] grid = new char[input.Length, len];

            for(int i = 0; i < input.Length; i++)
            {
                for(int j = 0; j < len; j++)
                {
                    grid[i,j] = (input[i])[j];
                }
            }

            int sum = 0;
            for(int i = 0; i < input.Length; i++)
            {
                string curString = "";
                bool bordersSymbol = false;
                bool isAsterisk = false;
                for(int j =0; j < len; j++)
                {
                    if(grid[i,j] < 48 || grid[i,j] > 57)
                    {
                        if(curString != "")
                        {
                            // Come to the end of number. Check this column, then sum
                            if (i != 0)
                            {
                                // Not first row, check above
                                bordersSymbol |= IsSymbol(grid[i - 1, j]);
                            }

                            // Check centre
                            bordersSymbol |= IsSymbol(grid[i, j]);

                            if (i != input.Count() - 1)
                            {
                                // Not final row, check below
                                bordersSymbol |= IsSymbol(grid[i + 1, j]);
                            }

                            log.Add(curString);
                            if (bordersSymbol)
                            {
                                sum += int.Parse(curString);
                                log.Add("Adjacent");
                            }
                            curString = "";
                            bordersSymbol = false;
                        }
                        continue;
                    }
                    
                    // We have a digit!
                    if(curString == "")
                    {
                        // First digit of new number
                        if(j != 0)
                        {
                            // Not first column, check left column
                            if (i != 0)
                            {
                                // Not first row, check above and left
                                bordersSymbol |= IsSymbol(grid[i - 1, j - 1]);
                            }

                            // Check left
                            bordersSymbol |= IsSymbol(grid[i, j - 1]);

                            if (i != input.Count() - 1)
                            {
                                // Not final row, check below and left
                                bordersSymbol |= IsSymbol(grid[i + 1, j - 1]);
                            }

                            // check above and below
                            if (i != 0) bordersSymbol |= IsSymbol(grid[i - 1, j]);
                            if (i != len - 1) bordersSymbol |= IsSymbol(grid[i + 1, j]);
                        }
                        else
                        {
                            // First column, just check above and below
                            if(i != 0) bordersSymbol |= IsSymbol(grid[i - 1, j]);
                            if(i != len - 1) bordersSymbol |= IsSymbol(grid[i + 1, j]);
                        }
                    }
                    else if (j == len - 1)
                    {
                        // Final column, check above and below then sum
                        if (i != 0) bordersSymbol |= IsSymbol(grid[i - 1, j]);
                        if (i != len - 1) bordersSymbol |= IsSymbol(grid[i + 1, j]);

                        curString += grid[i, j];
                        log.Add(curString);
                        if (bordersSymbol)
                        {
                            sum += int.Parse(curString);
                            log.Add("Adjacent");
                        }
                        curString = "";
                        bordersSymbol = false;
                    }
                    else
                    {
                        //Middle of the grid
                        if (i != 0)
                        {
                            // Not first row, check above
                            bordersSymbol |= IsSymbol(grid[i - 1, j]);
                        }

                        if (i != input.Count() - 1)
                        {
                            // Not final row, check below
                            bordersSymbol |= IsSymbol(grid[i + 1, j]);
                        }
                    }

                    curString += grid[i, j];
                }
            }
            File.WriteAllLines(@"C:\Users\harry\Desktop\log.txt", log);

            Console.WriteLine("Part 1 - " + sum);
        }

        private static bool IsSymbol(char c)
        {
            return ((c < 48 || c > 57) && c != '.');
        }
    }
}

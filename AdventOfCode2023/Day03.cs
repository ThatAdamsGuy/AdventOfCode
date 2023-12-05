using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day03
    {
        private static char[,] grid;

        public static void Run()
        {
            List<string> log = new List<string>();

            var input = File.ReadAllLines("Day03Input.txt");
            var len = input.First().Length;
            grid = new char[input.Length, len];

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

            Console.WriteLine("Part 1 - " + sum);

            // Find all asterisks
            List<Tuple<int, int>> asterisks = new List<Tuple<int, int>>();
            for(int i = 0; i < input.Length; i++)
            {
                for(int j = 0; j < len; j++)
                {
                    if (grid[i, j] == '*') asterisks.Add(Tuple.Create(i, j));
                }
            }

            long p2Sum = 0;
            foreach(var asterisk in asterisks)
            {
                // For ease
                int row = asterisk.Item1;
                int column = asterisk.Item2;

                if (row == 83 && column == 108)
                {
                    ;
                }

                List<int> partNumbers = new List<int>();

                if(row != 0 && column != 0 && row != input.Length - 1 && column != len - 1)
                {
                    // Middle square

                    // ABOVE
                    if (IsNumber(grid[row-1, column - 1]))
                    {
                        // Top left is a number
                        if (IsNumber(grid[row-1, column]))
                        {
                            // Top Left and Top Middle is a number
                            if (IsNumber(grid[row-1, column + 1]))
                            {
                                // Entire top row is a number
                                partNumbers.Add(Search(row - 1, column - 1, true));
                            }
                            else
                            {
                                // Above and Top Left is number
                                partNumbers.Add(Search(row - 1, column, false));
                            }
                        }
                        else
                        {
                            if (IsNumber(grid[row-1, column + 1]))
                            {
                                // Two numbers separated by above middle
                                partNumbers.Add(Search(row - 1, column - 1, false));
                                partNumbers.Add(Search(row - 1, column + 1, true));
                            }
                            else
                            {
                                // Only top left is a number
                                partNumbers.Add(Search(row - 1, column - 1, false));
                            }
                        }
                    }
                    else if (IsNumber(grid[row - 1, column]))
                    {
                        // Above is number, top left isn't
                        partNumbers.Add(Search(row - 1, column, true));
                    }
                    else if (IsNumber(grid[row-1, column + 1]))
                    {
                        // Top right corner is a number
                        partNumbers.Add(Search(row - 1,column + 1, true));
                    }

                    // BELOW
                    if (IsNumber(grid[row + 1, column - 1]))
                    {
                        // Bottom left is a number
                        if (IsNumber(grid[row + 1, column]))
                        {
                            // Bottom Left and Bottom Middle is a number
                            if (IsNumber(grid[row + 1, column + 1]))
                            {
                                // Entire Bottom row is a number
                                partNumbers.Add(Search(row + 1, column + 1, false));
                            }
                            else
                            {
                                // Below and Bottom Left is number
                                partNumbers.Add(Search(row + 1, column, false));
                            }
                        }
                        else
                        {
                            if (IsNumber(grid[row + 1, column + 1]))
                            {
                                // Two numbers separated by below middle
                                partNumbers.Add(Search(row + 1, column - 1, false));
                                partNumbers.Add(Search(row + 1, column + 1, true));
                            }
                            else
                            {
                                // Only below left is a number
                                partNumbers.Add(Search(row + 1, column - 1, false));
                            }
                        }
                    }
                    else if (IsNumber(grid[row + 1, column]))
                    {
                        // Below is number, Bottom left isn't
                        partNumbers.Add(Search(row + 1, column, true));
                    }
                    else if (IsNumber(grid[row + 1, column + 1]))
                    {
                        // Bottom right corner is a number
                        partNumbers.Add(Search(row + 1, column + 1, true));
                    }

                    // LEFT
                    if (IsNumber(grid[row, column - 1]))
                    {
                        partNumbers.Add(Search(row, column - 1, false));
                    }

                    // RIGHT
                    if (IsNumber(grid[row, column + 1]))
                    {
                        partNumbers.Add(Search(row, column + 1, true));
                    }
                }
                else
                {
                    if (row == 0 && column == 0)
                    {
                        // Top Left Corner

                        // Right
                        if (IsNumber(grid[row, column + 1]))
                        {
                            partNumbers.Add(Search(row, column + 1, true));
                        }

                        // Below
                        if (IsNumber(grid[row+1, column]))
                        {
                            partNumbers.Add(Search(row + 1, column, true));
                        }
                        else if (IsNumber(grid[row+1, column + 1]))
                        {
                            partNumbers.Add(Search(row + 1, column + 1, true));
                        }
                    }
                    else if (row == 0 && column == len - 1)
                    {
                        // Top Right Corner

                        // Left
                        if (IsNumber(grid[row, column - 1]))
                        {
                            partNumbers.Add(Search(row, column - 1, false));
                        }

                        // Below
                        if (IsNumber(grid[row + 1, column]))
                        {
                            partNumbers.Add(Search(row + 1, column, false));
                        }
                        else if (IsNumber(grid[row + 1, column - 1]))
                        {
                            partNumbers.Add(Search(row + 1, column - 1, false));
                        }
                    }
                    else if (row == input.Length - 1 && column == 0)
                    {
                        // Bottom Left Corner

                        // Right
                        if (IsNumber(grid[row, column + 1]))
                        {
                            partNumbers.Add(Search(row, column + 1, true));
                        }

                        // Above
                        if (IsNumber(grid[row - 1, column]))
                        {
                            partNumbers.Add(Search(row - 1, column, true));
                        }
                        else if (IsNumber(grid[row - 1, column + 1]))
                        {
                            partNumbers.Add(Search(row - 1, column + 1, true));
                        }
                    }
                    else if (row == input.Length - 1 && column == len - 1)
                    {
                        // Bottom Right Corner

                        // Left
                        if (IsNumber(grid[row, column - 1]))
                        {
                            partNumbers.Add(Search(row, column - 1, false));
                        }

                        // Above
                        if (IsNumber(grid[row + 1, column]))
                        {
                            partNumbers.Add(Search(row + 1, column, false));
                        }
                        else if (IsNumber(grid[row + 1, column - 1]))
                        {
                            partNumbers.Add(Search(row + 1, column - 1, false));
                        }
                    }
                    else
                    {
                        if(row == 0)
                        {
                            // Top Row

                            // Left
                            if (IsNumber(grid[row, column - 1]))
                            {
                                partNumbers.Add(Search(row, column - 1, false));
                            }

                            // Right
                            if (IsNumber(grid[row, column + 1]))
                            {
                                partNumbers.Add(Search(row, column + 1, true));
                            }

                            // Below
                            if (IsNumber(grid[row + 1, column - 1]))
                            {
                                // Bottom left is a number
                                if (IsNumber(grid[row + 1, column]))
                                {
                                    // Bottom Left and Bottom Middle is a number
                                    if (IsNumber(grid[row + 1, column + 1]))
                                    {
                                        // Entire Bottom row is a number
                                        partNumbers.Add(Search(row + 1, column - 1, true));
                                    }
                                    else
                                    {
                                        // Below and Bottom Left is number
                                        partNumbers.Add(Search(row + 1, column, false));
                                    }
                                }
                                else
                                {
                                    if (IsNumber(grid[row + 1, column + 1]))
                                    {
                                        // Two numbers separated by Bottom middle
                                        partNumbers.Add(Search(row + 1, column - 1, false));
                                        partNumbers.Add(Search(row + 1, column + 1, true));
                                    }
                                    {
                                        // Only Bottom left is a number
                                        partNumbers.Add(Search(row + 1, column - 1, false));
                                    }
                                }
                            }
                            else if (IsNumber(grid[row + 1, column]))
                            {
                                // Below is number, Bottom left isn't
                                partNumbers.Add(Search(row + 1, column, true));
                            }
                            else if (IsNumber(grid[row + 1, column + 1]))
                            {
                                // Bottom right corner is a number
                                partNumbers.Add(Search(row + 1, column + 1, true));
                            }
                        }
                        else if (row == input.Length - 1)
                        {
                            // Bottom Row

                            // Left
                            if (IsNumber(grid[row, column - 1]))
                            {
                                partNumbers.Add(Search(row, column - 1, false));
                            }

                            // Right
                            if (IsNumber(grid[row, column + 1]))
                            {
                                partNumbers.Add(Search(row, column + 1, true));
                            }

                            // Above
                            if (IsNumber(grid[row - 1, column - 1]))
                            {
                                // Top left is a number
                                if (IsNumber(grid[row - 1, column]))
                                {
                                    // Top Left and Top Middle is a number
                                    if (IsNumber(grid[row - 1, column + 1]))
                                    {
                                        // Entire top row is a number
                                        partNumbers.Add(Search(row - 1, column + 1, false));
                                    }
                                    else
                                    {
                                        // Above and Top Left is number
                                        partNumbers.Add(Search(row - 1, column, false));
                                    }
                                }
                                else
                                {
                                    if (IsNumber(grid[row - 1, column + 1]))
                                    {
                                        // Two numbers separated by above middle
                                        partNumbers.Add(Search(row - 1, column - 1, false));
                                        partNumbers.Add(Search(row - 1, column + 1, true));
                                    }
                                    else
                                    {
                                        // Only top left is a number
                                        partNumbers.Add(Search(row - 1, column - 1, false));
                                    }
                                }
                            }
                            else if (IsNumber(grid[row - 1, column]))
                            {
                                // Above is number, top left isn't
                                partNumbers.Add(Search(row - 1, column, true));
                            }
                            else if (IsNumber(grid[row - 1, column + 1]))
                            {
                                // Top right corner is a number
                                partNumbers.Add(Search(row - 1, column + 1, true));
                            }
                        }
                        else if (column == 0)
                        {
                            // Left Column

                            // Right
                            if (IsNumber(grid[row, column + 1]))
                            {
                                partNumbers.Add(Search(row, column + 1, true));
                            }

                            // Above
                            if (IsNumber(grid[row - 1, column]))
                            {
                                if (IsNumber(grid[row-1,column + 1]))
                                {
                                    // Both above are numbers
                                    partNumbers.Add(Search(row - 1, column, true));
                                }
                            }
                            else if (IsNumber(grid[row - 1, column + 1]))
                            {
                                partNumbers.Add(Search(row - 1, column + 1, true));
                            }

                            // Below
                            if (IsNumber(grid[row + 1, column]))
                            {
                                if (IsNumber(grid[row + 1, column + 1]))
                                {
                                    partNumbers.Add(Search(row + 1, column, true));
                                }
                            }
                            else if (IsNumber(grid[row + 1, column + 1]))
                            {
                                partNumbers.Add(Search(row + 1, column + 1, true));
                            }
                        }
                        else if (column == len - 1)
                        {
                            // Right Column

                            // Left
                            if (IsNumber(grid[row, column - 1]))
                            {
                                partNumbers.Add(Search(row, column - 1, false));
                            }

                            // Above
                            if (IsNumber(grid[row - 1, column]))
                            {
                                if (IsNumber(grid[row - 1, column - 1]))
                                {
                                    partNumbers.Add(Search(row - 1, column, false));
                                }
                            }
                            else if (IsNumber(grid[row - 1, column - 1]))
                            {
                                partNumbers.Add(Search(row - 1, column - 1, false));
                            }

                            // Below
                            if (IsNumber(grid[row + 1, column]))
                            {
                                if (IsNumber(grid[row + 1, column - 1]))
                                {
                                    partNumbers.Add(Search(row + 1, column, false));
                                }
                            }
                            else if (IsNumber(grid[row + 1, column - 1]))
                            {
                                partNumbers.Add(Search(row + 1, column - 1, false));
                            }
                        }
                        else
                        {
                            throw new Exception("WTF?");
                        }
                    }
                }
                if (partNumbers.Contains(135))
                {
                    ;
                }
                if (partNumbers.Count == 2)
                {
                    p2Sum += partNumbers[0] * partNumbers[1];
                }
            }
            Console.WriteLine("Part 2 - " + p2Sum);
        }

        private static int Search(int x, int y, bool isRight)
        {
            string toReturn = string.Empty;
            try
            {
                while (IsNumber(grid[x, y]))
                {
                    toReturn += grid[x, y];
                    y += isRight ? 1 : -1;
                }
            }
            catch (IndexOutOfRangeException)
            {
                return int.Parse(isRight ? toReturn : Reverse(toReturn));
            }
            return int.Parse(isRight ? toReturn : Reverse(toReturn));
        }

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        private static bool IsNumber(char c)
        {
            return (c >= 48 && c <= 57);
        }

        private static bool IsSymbol(char c)
        {
            return ((c < 48 || c > 57) && c != '.');
        }
    }
}

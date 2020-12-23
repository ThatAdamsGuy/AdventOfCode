using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day11
    {
        public static List<string> inputs { get; set; }

        public enum Directions
        {
            N,
            NE,
            E,
            SE,
            S,
            SW,
            W,
            NW
        }

        public static void Run()
        {
            inputs = File.ReadAllLines("day11Input.txt").ToList();

            int lastFlips = 0;
            while (true)
            {
                HashSet<Tuple<int, int>> flipPos = new HashSet<Tuple<int, int>>();
                for(int i = 0; i < inputs.Count(); i++)
                {
                    string curLine = inputs[i];
                    for(int j = 0; j < curLine.Length; j++)
                    {
                        if (curLine[j] == '.') continue;

                        if(curLine[j] == '#')
                        {
                            if(i - 1 < 0 || i + 1 == inputs.Count() || j - 1 < 0 || j + 1 == curLine.Length)
                            {
                                //Edges and Corners
                                if (EdgeCases(i, j, inputs.Count() - 1, curLine.Length)) flipPos.Add(Tuple.Create(i, j));
                            } else
                            {
                                int count = 0;

                                if (inputs[i - 1][j - 1] == '#') count++;
                                if (inputs[i - 1][j] == '#') count++;
                                if (inputs[i - 1][j + 1] == '#') count++;
                                if (inputs[i][j - 1] == '#') count++;
                                if (inputs[i][j + 1] == '#') count++;
                                if (inputs[i + 1][j - 1] == '#') count++;
                                if (inputs[i + 1][j] == '#') count++;
                                if (inputs[i + 1][j + 1] == '#') count++;

                                if (count >= 4) flipPos.Add(Tuple.Create(i, j));
                                continue;
                            }
                        } else
                        {
                            //Unoccupied Seat - check for ANY occupied seat nearby
                            if (i - 1 < 0 || i + 1 == inputs.Count() || j - 1 < 0 || j + 1 == curLine.Length)
                            {
                                //Edges and Corners
                                if (EdgeCases(i, j, inputs.Count() - 1, curLine.Length)) flipPos.Add(Tuple.Create(i, j));
                            }
                            else
                            {
                                if ((inputs[i - 1][j - 1] == '#') ||
                                (inputs[i - 1][j] == '#') ||
                                (inputs[i - 1][j + 1] == '#') ||
                                (inputs[i][j - 1] == '#') ||
                                (inputs[i][j + 1] == '#') ||
                                (inputs[i + 1][j - 1] == '#') ||
                                (inputs[i + 1][j] == '#') ||
                                (inputs[i + 1][j + 1] == '#')) continue;
                                else flipPos.Add(Tuple.Create(i, j));
                            }
                        }
                    }
                }
                lastFlips = flipPos.Count();
                if (lastFlips == 0) break;
                else
                {
                    foreach(var flip in flipPos)
                    {
                        FlipPosition(flip.Item1, flip.Item2);
                    }
                }
            }

            int occupied = 0;
            foreach(string line in inputs)
            {
                occupied += line.Count(s => s == '#');
                Console.WriteLine(line);
            }
            Console.WriteLine($"Part 1: {occupied} seats occupied.");



            inputs = File.ReadAllLines("day11Input.txt").ToList();
            while (true)
            {
                HashSet<Tuple<int, int>> flipPos = new HashSet<Tuple<int, int>>();
                for (int i = 0; i < inputs.Count(); i++)
                {
                    string line = inputs[i];
                    for(int j = 0; j < line.Length; j++)
                    {
                        var dirs = Enum.GetValues(typeof(Directions)).Cast<Directions>();
                        if (inputs[i][j] == 'L')
                        {
                            bool occupiedSeat = false;
                            foreach (var direction in dirs)
                            {
                                char result = CheckDirection(direction, i, j, line.Length);
                                if (CheckDirection(direction, i, j, line.Length) == '#')
                                {
                                    occupiedSeat = true;
                                    break;
                                }
                            }
                            if (!occupiedSeat)
                            {
                                flipPos.Add(Tuple.Create(i, j));
                            }
                        } else if (inputs[i][j] == '#')
                        {
                            int count = 0;
                            foreach (var direction in dirs)
                            {
                                char result = CheckDirection(direction, i, j, line.Length);
                                if (CheckDirection(direction, i, j, line.Length) == '#')
                                {
                                    count++;
                                    continue;
                                }
                            }
                            if (count >= 5) flipPos.Add(Tuple.Create(i, j));
                        }
                    }
                }
                lastFlips = flipPos.Count();
                if (lastFlips == 0) break;
                else
                {
                    foreach (var flip in flipPos)
                    {
                        FlipPosition(flip.Item1, flip.Item2);
                    }
                }
            }

            occupied = 0;
            foreach (string line in inputs)
            {
                occupied += line.Count(s => s == '#');
                Console.WriteLine(line);
            }
            Console.WriteLine($"Part 2: {occupied} seats occupied.");
        }

        public static void FlipPosition(int row, int col)
        {
            StringBuilder sb = new StringBuilder(inputs[row]);
            if (sb[col] == '#') sb[col] = 'L';
            else if (sb[col] == 'L') sb[col] = '#';
            else Console.WriteLine("We somehow fucked up.");
            inputs[row] = sb.ToString();
        }

        public static char CheckDirection(Directions dir, int startRow, int startCol, int colLimit)
        {
            switch (dir)
            {
                case Directions.N:
                    if (startRow - 1 < 0) return '.';
                    return (inputs[startRow - 1][startCol] == '.') 
                        ? CheckDirection(dir, startRow-1, startCol, colLimit) 
                        : inputs[startRow - 1][startCol] ;
                case Directions.NE:
                    if (startRow - 1 < 0 || startCol + 1 == colLimit) return '.';
                    return (inputs[startRow - 1][startCol + 1] == '.')
                        ? CheckDirection(dir, startRow - 1, startCol + 1, colLimit)
                        : inputs[startRow - 1][startCol + 1];
                case Directions.E:
                    if (startCol + 1 == colLimit) return '.';
                    return (inputs[startRow][startCol + 1] == '.')
                        ? CheckDirection(dir, startRow, startCol + 1, colLimit)
                        : inputs[startRow][startCol + 1];
                case Directions.SE:
                    if (startRow + 1 == inputs.Count() || startCol + 1 == colLimit) return '.';
                    return (inputs[startRow + 1][startCol + 1] == '.')
                        ? CheckDirection(dir, startRow + 1, startCol + 1, colLimit)
                        : inputs[startRow + 1][startCol + 1];
                case Directions.S:
                    if (startRow + 1 == inputs.Count()) return '.';
                    return (inputs[startRow + 1][startCol] == '.')
                        ? CheckDirection(dir, startRow + 1, startCol, colLimit)
                        : inputs[startRow + 1][startCol];
                case Directions.SW:
                    if (startRow + 1 == inputs.Count() || startCol - 1 < 0) return '.';
                    return (inputs[startRow + 1][startCol - 1] == '.')
                        ? CheckDirection(dir, startRow + 1, startCol - 1, colLimit)
                        : inputs[startRow + 1][startCol - 1];
                case Directions.W:
                    if (startCol - 1 < 0) return '.';
                    return (inputs[startRow][startCol - 1] == '.')
                        ? CheckDirection(dir, startRow, startCol - 1, colLimit)
                        : inputs[startRow][startCol - 1];
                case Directions.NW:
                    if (startRow - 1 < 0 || startCol - 1 < 0) return '.';
                    return (inputs[startRow - 1][startCol - 1] == '.')
                        ? CheckDirection(dir, startRow - 1, startCol - 1, colLimit)
                        : inputs[startRow - 1][startCol - 1];
                default:
                    Console.WriteLine("FUCKED UP");
                    return 'X';
            }
        }

        public static bool EdgeCases(int i, int j, int rows, int cols)
        {
            if (i == 0)
            {
                //Top Row
                if (j == 0)
                {
                    //Top Left Corner
                    if (inputs[i][j] == '#') return false;

                    return (inputs[i][j + 1] == '#' || 
                        inputs[i + 1][j] == '#' || 
                        inputs[i + 1][j + 1] == '#') ? false : true;
                }
                else if (j + 1 == cols)
                {
                    //Top Right Corner
                    if (inputs[i][j] == '#') return false;

                    return (inputs[i][j - 1] == '#' || 
                        inputs[i + 1][j] == '#' || 
                        inputs[i + 1][j - 1] == '#') ? false : true;
                }
                else
                {
                    //Along Top Row
                    int count = 0;
                    if(inputs[i][j] == '#')
                    {
                        if (inputs[i][j - 1] == '#') count++;
                        if (inputs[i][j + 1] == '#') count++;
                        if (inputs[i + 1][j - 1] == '#') count++;
                        if (inputs[i + 1][j] == '#') count++;
                        if (inputs[i + 1][j + 1] == '#') count++;
                        return (count >= 4);
                    } else
                    {
                        return (inputs[i][j - 1] == '#' ||
                            inputs[i][j + 1] == '#' ||
                            inputs[i + 1][j - 1] == '#' ||
                            inputs[i + 1][j] == '#' ||
                            inputs[i + 1][j + 1] == '#') ? false : true;
                    }
                }
            }
            else if (i == rows)
            {
                //Bottom Row
                if (j == 0)
                {
                    //Bottom Left Corner
                    if (inputs[i][j] == '#') return false;
                    return (inputs[i][j + 1] == '#' || 
                        inputs[i - 1][j] == '#' || 
                        inputs[i - 1][j + 1] == '#') ? false : true;
                }
                else if (j + 1 == cols)
                {
                    //Bottom Right Corner
                    if (inputs[i][j] == '#') return false;
                    return (inputs[i][j - 1] == '#' || 
                        inputs[i - 1][j] == '#' || 
                        inputs[i - 1][j - 1] == '#') ? false : true;
                }
                else
                {
                    //Along Bottom Row
                    int count = 0;
                    if (inputs[i][j] == '#')
                    {
                        if (inputs[i][j - 1] == '#') count++;
                        if (inputs[i][j + 1] == '#') count++;
                        if (inputs[i - 1][j - 1] == '#') count++;
                        if (inputs[i - 1][j] == '#') count++;
                        if (inputs[i - 1][j + 1] == '#') count++;
                        return (count >= 4);
                    }
                    else
                    {
                        return (inputs[i][j - 1] == '#' ||
                            inputs[i][j + 1] == '#' ||
                            inputs[i - 1][j - 1] == '#' ||
                            inputs[i - 1][j] == '#' ||
                            inputs[i - 1][j + 1] == '#') ? false : true;
                    }
                }
            } else
            {
                //Somewhere in the middle
                if(j == 0)
                {
                    //Left side, middle rows
                    int count = 0;
                    if (inputs[i][j] == '#')
                    {
                        if (inputs[i - 1][j] == '#') count++;
                        if (inputs[i - 1][j + 1] == '#') count++;
                        if (inputs[i][j + 1] == '#') count++;
                        if (inputs[i + 1][j + 1] == '#') count++;
                        if (inputs[i + 1][j] == '#') count++;
                        return (count >= 4);
                    }
                    else
                    {
                        return (inputs[i - 1][j] == '#' ||
                            inputs[i - 1][j + 1] == '#' ||
                            inputs[i][j + 1] == '#' ||
                            inputs[i + 1][j + 1] == '#' ||
                            inputs[i + 1][j] == '#') ? false : true;
                    }
                } else
                {
                    //Right side, middle rows
                    int count = 0;
                    if (inputs[i][j] == '#')
                    {
                        if (inputs[i - 1][j] == '#') count++;
                        if (inputs[i - 1][j - 1] == '#') count++;
                        if (inputs[i][j - 1] == '#') count++;
                        if (inputs[i + 1][j - 1] == '#') count++;
                        if (inputs[i + 1][j] == '#') count++;
                        return (count >= 4);
                    }
                    else
                    {
                        return (inputs[i - 1][j] == '#' ||
                            inputs[i - 1][j - 1] == '#' ||
                            inputs[i][j - 1] == '#' ||
                            inputs[i + 1][j - 1] == '#' ||
                            inputs[i + 1][j] == '#') ? false : true;
                    }
                }
            }
        }
    }
}

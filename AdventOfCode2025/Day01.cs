using System;
using System.IO;

namespace AdventOfCode2025
{
    internal class Day01
    {
        public static void Run()
        {
            int pos = 50;       // dial position
            long part1 = 0;     // end-of-rotation zeros
            long part2 = 0;     // all clicks landing on zero

            var input = File.ReadAllLines("day01input.txt");
            // var input = File.ReadAllLines("day01known.txt");

            foreach (var line in input)
            {
                char direction = line[0];
                int distance = int.Parse(line.Substring(1));

                int zerosThisMove = 0;

                if (direction == 'R')
                {
                    // Right rotations: hits when (pos + t) is a multiple of 100
                    zerosThisMove = (pos + distance) / 100;
                    pos = (pos + distance) % 100;
                }
                else if (direction == 'L')
                {
                    if (pos > 0)
                    {
                        if (distance < pos)
                        {
                            // Never even reach zero
                            zerosThisMove = 0;
                            pos = pos - distance;
                        }
                        else
                        {
                            // First hit at t = pos, then every 100 afterwards
                            int diff = distance - pos;
                            zerosThisMove = (diff / 100) + 1;

                            int newPos = pos - distance;
                            pos = ((newPos % 100) + 100) % 100;
                        }
                    }
                    else // pos == 0
                    {
                        // First hit at t = 100, then every 100
                        zerosThisMove = distance / 100;

                        int newPos = pos - distance;
                        pos = ((newPos % 100) + 100) % 100;
                    }
                }

                // Part 1: count rotations that END at 0
                if (pos == 0)
                {
                    part1++;
                }

                // Part 2: add all zeros hit during this move
                part2 += zerosThisMove;
            }

            Console.WriteLine("Part 1: " + part1);
            Console.WriteLine("Part 2: " + part2);
        }
    }
}

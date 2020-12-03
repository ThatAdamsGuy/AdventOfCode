using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day03
    {
        public static List<string> Rows { get; set; }
        public static int RowLength { get; set; }

        public static void Run()
        {
            Rows = File.ReadAllLines("day03Input.txt").ToList();
            RowLength = Rows.First().Length;

            int threeOne = SlopeCheck(3, 1);
            Console.WriteLine($"Slope (3,1) hits {3_1} trees.");

            int oneOne = SlopeCheck(1, 1);
            int fiveOne = SlopeCheck(5, 1);
            int sevenOne = SlopeCheck(7, 1);
            int oneTwo = SlopeCheck(1, 2);

            Console.WriteLine($"(1,1): {oneOne}");
            Console.WriteLine($"(3,1): {threeOne}");
            Console.WriteLine($"(5,1): {fiveOne}");
            Console.WriteLine($"(7,1): {sevenOne}");
            Console.WriteLine($"(1,2): {oneTwo}");
            Console.WriteLine($"All slopes multiplied provides {oneOne * threeOne * fiveOne * sevenOne * oneTwo}");
        }

        private static int SlopeCheck(int horizontalMove, int verticalMove)
        {
            int treeCount = 0;
            int horizontalPos = horizontalMove;
            for (int i = verticalMove; i < Rows.Count(); i += verticalMove)
            {
                string curRow = Rows[i];
                if (curRow[horizontalPos] == '#') treeCount++;
                for (int k = 0; k < horizontalMove; k++)
                {
                    horizontalPos++;
                    if (horizontalPos == RowLength) horizontalPos = 0;
                }
            }
            return treeCount;
        }
    }
}

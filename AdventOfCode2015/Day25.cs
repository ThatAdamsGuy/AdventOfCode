using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
    class Day25
    {
        private const long startingValue = 20151125;
        private const long multiplier = 252533;
        private const long modulo = 33554393;
        private const int row = 2981;
        private const int column = 3075;
        //private const int row = 6;
        //private const int column = 6;
        public static void Run()
        {
            long[,] array = new long[column*2,column*2];
            long prevValue = startingValue;
            for(int i = 0; i < column * 2; i++)
            {
                int column = 0;
                for(int j = i; j >= 0; j--)
                {
                    if(i == 0)
                    {
                        array[0, 0] = startingValue;
                    }
                    else
                    {
                        long result = ((prevValue * multiplier) % modulo);
                        array[column, j] = result;
                        prevValue = result;
                    }
                    column++;
                }
            }
            Console.WriteLine("Part 1 - " + array[column-1, row-1]);
        }
    }
}

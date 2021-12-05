using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    class Day01
    {
        public static void Run()
        {
            //P1
            var result = File.ReadAllLines("input.txt");
            int prevValue = int.Parse(result[0]);
            int counter = 0;
            for(int i = 1; i < result.Length; i++)
            {
                int curValue = int.Parse(result[i]);
                if(curValue > prevValue)
                {
                    counter++;
                }
                prevValue = curValue;
            }
            Console.WriteLine("P1 - " + counter);

            counter = 0;
            float newPrevValue = (int.Parse(result[0]) + int.Parse(result[1]) + int.Parse(result[2]));
            for (int i = 3; i < result.Length; i++)
            {
                float curValue = (int.Parse(result[i]) + int.Parse(result[i - 1]) + int.Parse(result[i - 2]));
                Console.WriteLine($"Prev - {newPrevValue}, Cur - {curValue}");
                if (curValue > newPrevValue)
                {
                    counter++;
                }
                newPrevValue = curValue;
            }
            Console.WriteLine("P2 - " + counter);
        }
    }
}

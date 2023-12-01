using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019
{
    class Day02
    {
        private static int desiredOutput = 19690720;

        public static void Run()
        {
            string input = File.ReadAllLines("Day02Input.txt").Single();
            Intcode intcode = new Intcode(input);
            intcode.SetPosition(1, 12);
            intcode.SetPosition(2, 2);
            intcode.Run();

            Console.WriteLine("Part 1 - " + intcode.GetValueAtPosition(0));

            bool found = false;
            for (int i = 0; i <= 99; i++)
            {
                for(int j = 0; j <= 99; j++)
                {
                    intcode.SetProgram(input);
                    intcode.SetPosition(1, i);
                    intcode.SetPosition(2, j);
                    intcode.Run();

                    if(intcode.GetValueAtPosition(0) == desiredOutput)
                    {
                        found = true;
                        Console.WriteLine("Part 2 - " + (100 * i + j));
                        break;
                    }
                }
                if (found) break;
            }
        }
    }
}

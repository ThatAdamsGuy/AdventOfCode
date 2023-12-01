using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2019
{
    class Day05
    {
        public static void Run()
        {
            string input = File.ReadAllLines("Day05Input.txt").Single();
            Intcode intcode = new Intcode(input);
            intcode.Run();
        }
    }
}

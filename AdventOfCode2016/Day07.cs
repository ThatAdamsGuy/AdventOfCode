using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AdventOfCode2016
{
    class Day07
    {
        public static void Run()
        {
            List<string> inputs = File.ReadAllLines("day07Input.txt").ToList();
            var regex = new Regex(@"(?[\$).*?(?]\$)");
            foreach (string line in inputs)
            {
                string match = regex.Match(line);
                for (int i = 0; i < line.Length - 4; i++)
                {

                }
            }
        }
    }
}

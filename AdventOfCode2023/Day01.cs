using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day01
    {

        public static void Run()
        {
            Dictionary<string, string> replacements = new Dictionary<string, string>()
            {
                { "one", "o1e" },
                { "two", "t2o" },
                { "three", "t3e" },
                { "four", "f4r" },
                { "five", "f5e" },
                { "six", "s6x" },
                { "seven", "s7n" },
                { "eight", "e8t" },
                { "nine", "n9e" }
            };

            int finalResult = 0;
            foreach (string line in File.ReadLines("Day01Input.txt"))
            {
                List<int> digits = new List<int>();
                foreach(char item in line)
                {
                    if(int.TryParse(item.ToString(), out int result))
                    {
                        digits.Add(result);
                    }
                }
                if(digits.Count == 0)
                {
                    continue;
                }
                else if(digits.Count == 1)
                {
                    finalResult += int.Parse(digits[0].ToString() + digits[0].ToString());
                }
                else
                {
                    finalResult += int.Parse(digits.First().ToString() + digits.Last().ToString());
                }
            }
            Console.WriteLine("Part 1 - " + finalResult);
            finalResult = 0;

            foreach (string line in File.ReadLines("Day01Input.txt"))
            {
                string newline = line;
                foreach (var replacement in replacements)
                {
                    newline = newline.Replace(replacement.Key, replacement.Value);
                }
                List<int> digits = new List<int>();
                foreach (char item in newline)
                {
                    if (int.TryParse(item.ToString(), out int result))
                    {
                        digits.Add(result);
                    }
                }
                if (digits.Count == 0)
                {
                    continue;
                }
                else if (digits.Count == 1)
                {
                    finalResult += int.Parse(digits[0].ToString() + digits[0].ToString());
                }
                else
                {
                    finalResult += int.Parse(digits.First().ToString() + digits.Last().ToString());
                }
            }
            Console.WriteLine("Part 2 - " + finalResult);
        }
    }
}

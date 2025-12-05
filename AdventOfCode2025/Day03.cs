using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025
{
    internal class Day03
    {
        public static void Run()
        {
            var input = File.ReadAllLines("day03input.txt");
            int pt1joltageSum = 0;
            long pt2joltageSum = 0;
            foreach(var line in input)
            {
                int leftDigit = 0;
                int rightDigit = 0;
                for(int i = 0; i < line.Length; i++)
                {
                    int digit = (int.Parse(line[i].ToString()));
                    // First char
                    if (i == 0)
                    {
                        leftDigit = int.Parse(line[i].ToString());
                        continue;
                    }

                    // Last char
                    if(i == line.Length - 1)
                    {
                        rightDigit = GetMax(digit, rightDigit);
                        continue;
                    }

                    if (digit > leftDigit)
                    {
                        leftDigit = digit;
                        rightDigit = 0;
                        continue;                        
                    }

                    if (digit > rightDigit)
                    {
                        rightDigit = digit; 
                    }
                }
                pt1joltageSum += int.Parse(leftDigit.ToString() + rightDigit.ToString());
                pt2joltageSum += long.Parse(GetLargestKDigitNumber(line, 12));
            }
            Console.WriteLine("Part 1 - " + pt1joltageSum);
            Console.WriteLine("Part 2 - " + pt2joltageSum);
        }

        private static string GetLargestKDigitNumber(string s, int k)
        {
            if (s.Length < k) return s;

            var result = new List<char>();
            int n = s.Length;

            for (int i = 0; i < n; i++)
            {
                // While we have digits in result, the current digit is larger than the last digit in result,
                // and we have enough remaining digits (including current) to fill k positions
                while (result.Count > 0 &&
                       s[i] > result[result.Count - 1] &&
                       result.Count - 1 + (n - i) >= k)
                {
                    result.RemoveAt(result.Count - 1);
                }

                // Add current digit if we haven't reached k digits yet
                if (result.Count < k)
                {
                    result.Add(s[i]);
                }
            }

            return new string(result.ToArray());
        }

        private static int GetMax(int a, int b)
        {
            return a > b ? a : b;
        }
    }
}

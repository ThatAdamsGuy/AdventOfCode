using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day06
    {
        public static void Run()
        {
            string input = File.ReadAllLines("Day06Input.txt").Single();
            bool p1Found = false;
            bool p2Found = false;
            string p1Tmp = "";
            string p2Tmp = "";
            for(int i = 0; i < input.Length; i++)
            {
                p1Tmp += input[i];
                p2Tmp += input[i];
                if (p1Tmp.Length == 4 && !p1Found)
                {
                    if (RemoveDuplicateChars(p1Tmp) == p1Tmp)
                    {
                        Console.WriteLine("Part 1: " + (i + 1));
                        p1Found = true;
                    }
                    else
                    {
                        p1Tmp = p1Tmp.Substring(1, 3);
                    }
                }
                if (p2Tmp.Length == 14 && !p2Found)
                {
                    if (RemoveDuplicateChars(p2Tmp) == p2Tmp)
                    {
                        Console.WriteLine("Part 2: " + (i + 1));
                        p2Found = true;
                    }
                    else
                    {
                        p2Tmp = p2Tmp.Substring(1, 13);
                    }
                }

                if (p1Found && p2Found) break;
            }
        }

        private static string RemoveDuplicateChars(string key)
        {
            string table = string.Empty;
            string result = string.Empty;
            foreach (char value in key)
            {
                if (table.IndexOf(value) == -1)
                {
                    table += value;
                    result += value;
                }
            }
            return result;
        }
    }
}

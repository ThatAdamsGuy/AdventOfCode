using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
    class Day10
    {
        private static List<char> invalid = new List<char> { 'i', 'o', 'l' };
        public static void Run()
        {
            string input = "vzbxkghb";
            int part = 1;
            while (true)
            {
                string prevInput = input;
                string newInput = CheckInvalidChars(input);
                if(prevInput != newInput)
                {
                    input = newInput;
                    continue;
                }
                if (CheckSeriesOfChars(input) && CheckOverlappingPairs(input))
                {
                    Console.WriteLine($"Part {part} - {input}");
                    part++;
                    if(part > 2)
                    {
                        break;
                    }
                }
                input = IncrementPassword(input);
            }
        }

        private static bool CheckOverlappingPairs(string input)
        {
            int pairCount = 0;
            for (int i = 0; i < input.Length - 1; i++)
            {
                if(input[i] == input[i+1])
                {
                    pairCount++;
                    i++;
                }
            }
            return pairCount > 1;
        }

        private static bool CheckSeriesOfChars(string input)
        {
            bool valid = false;
            for(int i = 0; i < input.Length - 2; i++)
            {
                if (input[i+1] == (input[i] + 1)
                    && (input[i+2] == (input[i+1] + 1)))
                {
                    valid = true;
                    break;
                }
            }
            return valid;
        }

        private static string CheckInvalidChars(string input)
        {
            bool invalidChar = false;
            for (int i = 0; i < input.Length; i++)
            {
                if (invalid.Contains(input[i]))
                {
                    invalidChar = true;
                }
                if (invalidChar)
                {
                    var newInput = input.Substring(0, i);
                    newInput += (char)(input[i] + 1);
                    for (int k = 0; k < input.Length - i - 1; k++)
                    {
                        newInput += 'a';
                    }
                    input = newInput;
                }
            }
            return input;
        }

        private static string IncrementPassword(string password)
        {
            var charArray = password.ToCharArray();
            for (int i = charArray.Length - 1; i >= 0; i--)
            {
                if (charArray[i] == 'z')
                {
                    charArray[i] = 'a';
                }
                else
                {
                    charArray[i]++;
                    break;
                }
            }
            return new string(charArray);
        }

    }


}

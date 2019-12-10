using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _2015_5
{
    class Program
    {
        static void Main(string[] args)
        {
            PartOne();
            PartTwo();
        }

        public static void PartTwo()
        {
            int niceStrings = 0;
            string input;
            using (TextReader tx = new StreamReader(@"input.txt"))
            {
                while ((input = tx.ReadLine()) != null)
                {
                    bool repeatedLetter = false;
                    bool doubleLetter = false;

                    for(int i = 0; i < input.Length; i++)
                    {
                        //Check for letters with 
                        if (i < input.Length - 2)
                        {
                            if (input[i] == input[i + 2]) repeatedLetter = true;
                        }

                        if (i < input.Length - 1)
                        {
                            if (input.Split(input.Substring(i, 2)).Length - 1 > 1) doubleLetter = true;
                        }
                    }

                    if (repeatedLetter && doubleLetter) niceStrings++;
                }
            }
            Console.WriteLine("Part Two Nice strings: " + niceStrings);
        }

        public static void PartOne()
        {
            List<string> badStrings = new List<string>() { "ab", "cd", "pq", "xy" };
            int niceStrings = 0;

            string input;
            using (TextReader tx = new StreamReader(@"input.txt"))
            {
                while ((input = tx.ReadLine()) != null)
                {
                    int vowelcount = 0;
                    bool doubleLetter = false;

                    if (badStrings.Any(s => input.Contains(s))) continue;

                    for (int i = 0; i < input.Length; i++)
                    {
                        vowelcount += IsVowel(input[i]) ? 1 : 0;
                        if (i != input.Length - 1)
                        {
                            if (input[i] == input[i + 1]) doubleLetter = true;
                        }
                    }

                    niceStrings += (vowelcount > 2 && doubleLetter) ? 1 : 0;
                }
            }
            Console.WriteLine("Part One Nice strings: " + niceStrings);
        }

        public static bool IsVowel(char input)
        {
            return (input == 'a' || input == 'e' || input == 'i' || input == 'o' || input == 'u');
        }
    }
}

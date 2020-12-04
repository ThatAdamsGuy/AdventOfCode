using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2016
{
    class Day04
    {
        public static void Run()
        {
            int sectorIDSum = 0;
            List<string> input = File.ReadAllLines("day04Input.txt").ToList();
            foreach(var line in input)
            {
                string encrypted = line.Substring(0, line.Length - 7);
                Dictionary<char, int> counter = new Dictionary<char, int>();
                foreach(char each in encrypted)
                {
                    if (each < 'a' || each > 'z') continue;

                    if (counter.ContainsKey(each))
                    {
                        counter[each]++;
                    } else
                    {
                        counter.Add(each, 1);
                    }
                }
                counter.Remove('-');
                IOrderedEnumerable < KeyValuePair<char, int> > sortedCollection = counter
                    .OrderByDescending(x => x.Value)
                    .ThenBy(x => x.Key);

                int finalCount = 0;
                char prevLetter = (char)0;
                foreach (var pair in sortedCollection)
                {
                    char curLetter = pair.Key;
                    if (curLetter > prevLetter)
                    {
                        prevLetter = curLetter;
                        finalCount++;

                        if(finalCount == 5)
                        {
                            var split = line.Split('-');
                            int result = int.Parse(split[split.Length - 1].Split('[')[0]);
                            sectorIDSum += result;
                            Console.WriteLine($"Adding {result}, now at {sectorIDSum}");
                            break;
                        }

                    } else
                    {
                        break;
                    }
                }
            }

            Console.WriteLine($"Sector ID Sum: {sectorIDSum}");
        }
    }
}

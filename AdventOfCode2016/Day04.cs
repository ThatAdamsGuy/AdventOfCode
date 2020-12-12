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
            List<string> filteredInput = new List<string>();
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
                string checksum = "";

                int prevCount = 0;
                foreach (var pair in sortedCollection)
                {
                    char curLetter = pair.Key;
                    if (pair.Value < prevCount)
                    {
                        prevLetter = (char)0;
                        prevCount = pair.Value;
                        finalCount++;
                        checksum += pair.Key;
                    }
                    else if (curLetter > prevLetter)
                    {
                        prevLetter = curLetter;
                        prevCount = pair.Value;
                        finalCount++;
                        checksum += pair.Key;
                    } else
                    {
                        break;
                    }

                    if (finalCount == 5)
                    {
                        var split = line.Split('-');
                        int result = int.Parse(split[split.Length - 1].Split('[')[0]);
                        string check = line.Split('[')[1].Split(']')[0];

                        if (check.Equals(checksum))
                        {
                            sectorIDSum += result;
                            filteredInput.Add(line);
                        }
                        break;
                    }
                }
            }
            Console.WriteLine($"Sector ID Sum: {sectorIDSum}");

            foreach(string line in filteredInput)
            {
                string newLine = "";
                var split = line.Split('-');
                int shift = int.Parse(split[split.Length - 1].Split('[')[0]);

                foreach (char each in line)
                {
                    if(each == '-')
                    {
                        newLine += ' ';
                        continue;
                    }
                    newLine += (char)(((each - 'a' + shift) % 26) + 'a');
                }
                if(newLine.Contains("northpole object storage"))
                {
                    Console.WriteLine($"Northpole Object Storage sector ID: {shift}");
                    break;
                }
            }
        }
    }
}

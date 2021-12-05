using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021
{
    class Day03
    {
        public static void Run()
        {
            var input = File.ReadAllLines("Day03Input.txt").ToList();
            int[] zeroCounts = new int[input[0].Length];
            int[] oneCounts = new int[input[0].Length];

            foreach (var line in input)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] == '0')
                    {
                        zeroCounts[i]++;
                    }
                    else if (line[i] == '1')
                    {
                        oneCounts[i]++;
                    }
                }
            }

            string gammaRate = "";
            string epsilonRate = "";
            for (int i = 0; i < zeroCounts.Length; i++)
            {
                if (zeroCounts[i] > oneCounts[i])
                {
                    gammaRate += "0";
                    epsilonRate += "1";
                }
                else
                {
                    gammaRate += "1";
                    epsilonRate += "0";
                }
            }

            Console.WriteLine($"Part 1 - {Convert.ToInt32(gammaRate, 2) * Convert.ToInt32(epsilonRate, 2)}");

            string valOne = "";
            string valTwo = "";
            List<string> tmpInput = input;
            for (int i = 0; i < input[0].Length; i++)
            {
                tmpInput = GetMostOrLeastCommon(true, tmpInput, i);
                if(tmpInput.Count() == 1)
                {
                    valOne = tmpInput.Single();
                    break;
                }
            }

            tmpInput = input;
            for (int i = 0; i < input[0].Length; i++)
            {
                tmpInput = GetMostOrLeastCommon(false, tmpInput, i);
                if (tmpInput.Count() == 1)
                {
                    valTwo = tmpInput.Single();
                    break;
                }
            }
            Console.WriteLine($"Part 2 - {Convert.ToInt32(valOne, 2) * Convert.ToInt32(valTwo, 2)}");
        }

        private static List<string> GetMostOrLeastCommon(bool mostCommon, List<string> input, int checkPos)
        {
            int zeroCount = 0;
            int oneCount = 0;
            List<string> newList = new List<string>();

            foreach (var line in input)
            {
                if (line[checkPos] == '0')
                {
                    zeroCount++;
                }
                else
                {
                    oneCount++;
                }
            }

            foreach (var line in input) {
                if (mostCommon)
                {
                    if (zeroCount > oneCount && line[checkPos] == '0')
                    {
                        newList.Add(line);
                    } else if((oneCount > zeroCount || oneCount == zeroCount) && line[checkPos] == '1')
                    {
                        newList.Add(line);
                    }
                }else
                {
                    if (zeroCount > oneCount && line[checkPos] == '1')
                    {
                        newList.Add(line);
                    }
                    else if ((oneCount > zeroCount || oneCount == zeroCount) && line[checkPos] == '0')
                    {
                        newList.Add(line);
                    }
                }
            }
            return newList;
        }
    }
}

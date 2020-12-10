using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day09
    {
        public static void Run()
        {
            int preambleLength = 25;
            List<string> strings = File.ReadAllLines("day09Input.txt").ToList();
            List<long> inputs = new List<long>();
            foreach(string line in strings)
            {
                inputs.Add(long.Parse(line));
            }
            long targetValue = 0;

            for(int i = preambleLength; i < inputs.Count(); i++)
            {
                long curValue = inputs[i];
                List<long> addables = inputs.GetRange(i - preambleLength, preambleLength);

                bool isMatch = false;
                for(int k = 0; k < preambleLength; k++)
                {
                    for(int j = 0; j < preambleLength; j++)
                    {
                        if(addables[k] + addables[j] == curValue)
                        {
                            isMatch = true;
                            break;
                        }
                    }
                    if (isMatch)
                    {
                        break;
                    }
                }
                if (!isMatch)
                {
                    Console.WriteLine($"First non-matching number: {curValue}");
                    targetValue = curValue;
                    break;
                }
            }

            for(int i = 0; i < inputs.Count(); i++)
            {
                long addedValue = 0;
                int pos = i;
                int lowValue = i;
                int highValue = 0;
                while(addedValue < targetValue)
                {
                    addedValue += inputs[pos];
                    if (addedValue == targetValue)
                    {
                        highValue = pos;
                        break;
                    }
                    pos++;
                }
                if(highValue != 0)
                {
                    List<long> range = inputs.Skip(lowValue).Take(highValue - lowValue + 1).ToList();
                    Console.WriteLine($"Encryption Weakness is {range.Min() + range.Max()}");
                    Console.WriteLine($"Low: {range.Min()}. High: {range.Max()}");
                    break;
                }
            }
        }
    }
}

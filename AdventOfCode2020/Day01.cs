using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day01
    {
        public static void Run()
        {
            string[] inputs = File.ReadAllLines(@"day01Input.txt");
            List<int> values = new List<int>();
            foreach (var input in inputs)
            {
                values.Add(int.Parse(input));
            }

            bool twoValuesFound = false;
            for (int i = 0; i < values.Count; i++)
            {
                for (int k = i; k < values.Count; k++)
                {
                    if (values[k] == values[i]) continue;
                    else if (values[k] + values[i] == 2020)
                    {
                        Console.WriteLine($"TWO Values {values[k]} and {values[i]} sum to 2020. Multiplied: {values[k] * values[i]}");
                        twoValuesFound = true;
                        break;
                    }
                }
                if (twoValuesFound) break;
            }

            bool threeValuesFound = false;
            for (int i = 0; i < values.Count; i++)
            {
                for (int k = i; k < values.Count; k++)
                {
                    for (int j = k; j < values.Count; j++)
                    {
                        if (i == j || i == k || k == j) continue;

                        if(values[i] + values[k] + values[j] == 2020)
                        {
                            Console.WriteLine($"THREE Values {values[k]}, {values[j]}, and {values[i]} sum to 2020. Multiplied: {values[k] * values[i] * values[j]}");
                            threeValuesFound = true;
                            break;
                        }
                    }
                    if (threeValuesFound) break;
                }
                if (threeValuesFound) break;
            }
        }
    }
}

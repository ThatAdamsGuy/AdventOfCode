using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025
{
    internal class Day06
    {
        private static string file = "day06input.txt";   
        private static int rows = 4;
        //private static string file = "day06known.txt";
        //private static int rows = 3;

        public static void Run()
        {
            var input = File.ReadAllLines(file);
            var problems = new List<List<long>>();
            var operation = new List<string>();
            for (int i = 0; i <input.Length; i++){
                if (i != input.Length - 1)
                {
                    var splitLine = input[i].Split(' ').Where(x => x != "").Select(long.Parse).ToList();
                    problems.Add(splitLine);
                }
                else
                {
                    operation = input[i].Split(' ').Where(x => x != "").ToList();
                }
            }
            Console.WriteLine("Part 1 - " + CalculatePt1(problems, operation));

            // Part 2: Right-to-left cephalopod math
            input = File.ReadAllLines(file);
            foreach(var line in input)
            {
                Console.WriteLine(line.Length);
            }

            // Find max length and pad all lines to same length
            int maxLength = input.Max(line => line.Length);
            problems = new List<List<long>>();
            operation = new List<string>();
            var numbers = new List<long>();
            for (int i = maxLength - 1; i >= 0; i--)
            {
                string number = string.Empty;
                for (int j = 0; j < rows; j++)
                {
                    number += input[j][i].ToString();
                }
                if (string.IsNullOrWhiteSpace(number) || i == 0)
                {
                    // New Column
                    if(i == 0)
                    {
                        numbers.Add(long.Parse(number));
                    }
                    problems.Add(numbers.Select(x => long.Parse(x.ToString())).ToList());
                    number = string.Empty;
                    numbers = new List<long>();
                }
                else
                {
                    numbers.Add(long.Parse(number));
                }
                if (!string.IsNullOrWhiteSpace(input[rows][i].ToString()))
                {
                    operation.Add(input[rows][i].ToString());
                }
            }
            Console.WriteLine("Part 2 - " + CalculatePt2(problems, operation));
        }

        private static long CalculatePt1(List<List<long>> problems, List<string> operation)
        {
            long sum = 0;
            for (int i = 0; i < problems[0].Count(); i++)
            {
                var numbers = new List<long>();
                for (int j = 0; j < problems.Count(); j++)
                {
                    numbers.Add(problems[j][i]);
                }
                sum += Aggregate(numbers, operation[i]);
            }
            return sum;
        }

        private static long CalculatePt2(List<List<long>> problems, List<string> operation)
        {
            long sum = 0;
            for(int i = 0; i < problems.Count(); i++)
            {
                var numbers = problems[i];
                sum += Aggregate(numbers, operation[i]);
            }
            return sum;
        }

        private static long Aggregate (List<long> numbers, string operation)
        {
            if (operation == "*")
            {
                return numbers.Aggregate((x, y) => x * y);
            }
            else if (operation == "+")
            {
                return numbers.Aggregate((x, y) => x + y);
            }
            else
            {
                throw new Exception("Unknown operation.");
            }
        }
    }
}

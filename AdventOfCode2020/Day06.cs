using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day06
    {
        public static void Run()
        {
            List<string> input = File.ReadAllLines("day06Input.txt").ToList();
            int sum = 0;
            string continuingLine = "";
            for(int i = 0; i < input.Count(); i++)
            {
                string line = input[i]; 
                continuingLine += line;
                if (string.IsNullOrWhiteSpace(line) || i == input.Count() - 1)
                {
                    HashSet<char> characters = new HashSet<char>();
                    foreach (char each in continuingLine)
                    {
                        characters.Add(each);
                    }
                    sum += characters.Count();
                    continuingLine = "";
                }          
            }
            Console.WriteLine($"Anybody answered yes to {sum} questions.");

            List<char> letters = new List<char>();
            for (int i = 'a'; i <= 'z'; i++) letters.Add((char)i);

            sum = 0;
            List<string> people = new List<string>();
            int counter = 0;
            for (int i = 0; i < input.Count(); i++)
            {
                string line = input[i];
                if (!string.IsNullOrWhiteSpace(line))
                {
                    people.Add(line);
                }

                if(string.IsNullOrWhiteSpace(line) || i == input.Count() - 1)
                {
                    foreach(char item in letters)
                    {
                        if (people.All(s => s.Contains(item)))
                        {
                            sum++;
                        }
                    }
                    counter++;
                    Console.WriteLine(counter + " - " + sum);
                    people.Clear();
                }
            }
            Console.WriteLine($"Everybody answered yes to {sum} questions.");
        }
    }
}

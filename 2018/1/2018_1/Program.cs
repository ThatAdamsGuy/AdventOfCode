using System;
using System.Collections.Generic;
using System.IO;

namespace _2018_1
{
    class Program
    {
        static string line;
        static int frequency = 0;
        static List<int> occurred = new List<int>();
        static int? firstDuplicate;
        static bool duplicateFound = false;

        static void Main(string[] args)
        {
            int? result = RunThroughList();
        }

        private static int? RunThroughList()
        {
            using (TextReader tx = new StreamReader(@"input.txt"))
            {
                //Run through every line in file
                while ((line = tx.ReadLine()) != null)
                {
                    Console.Write(line);
                    int value = int.Parse(line);
                    frequency += value;

                    if (!duplicateFound && occurred.Contains(frequency))
                    {
                        firstDuplicate = frequency;
                        duplicateFound = true;
                    }
                    else
                    {
                        occurred.Add(frequency);
                    }
                    Console.WriteLine(" - " + frequency);
                }
                Console.WriteLine("Final Frequency: " + frequency);
                Console.WriteLine("First Duplicate: " + firstDuplicate);
                Console.WriteLine();
            }

            if (firstDuplicate == null)
            {
                return RunThroughList();
            } else
            {
                return firstDuplicate;
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _9._2
{
    class Program
    {
        static void Main(string[] args)
        {
            bool garbage = false;
            bool skip = false;
            int sum = 0;
            int depth = 0;
            int garbageCount = 0; 

            StreamReader inputFile = new StreamReader("../../input.txt");

            while (!inputFile.EndOfStream)
            {
                char currentChar = (char)inputFile.Read();

                if (skip)
                {
                    skip = false;
                    continue;
                }

                if (currentChar == '{' && !garbage)
                {
                    depth++;
                    continue;
                }

                if (currentChar == '}' && !garbage)
                {
                    sum = sum + depth;
                    depth--;
                    continue;
                }

                if (currentChar == '<' && !garbage)
                {
                    garbage = true;
                    continue;
                }

                if (currentChar == '>' && garbage)
                {
                    garbage = false;
                    continue;
                }

                if (currentChar == '!')
                {
                    skip = true;
                    continue;
                }

                if (garbage)
                {
                    garbageCount++;
                }
            }
            Console.WriteLine("SUM: " + sum);
            Console.WriteLine("GARBAGE: " + garbageCount);
            Console.ReadLine();
        }
    }
}

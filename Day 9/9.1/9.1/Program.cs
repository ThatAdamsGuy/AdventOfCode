using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _9._1
{
    class Program
    {
        static void Main(string[] args)
        {
            bool garbage = false;
            bool skip = false;
            ulong sum = 0;
            ulong depth = 0;

            StreamReader inputFile = new StreamReader("../../input.txt");

            while (!inputFile.EndOfStream)
            {
                char currentChar = (char)inputFile.Read();

                if (skip)
                {
                    skip = false;
                    continue;
                }

                if(currentChar == '{' && !garbage)
                {
                    depth++;
                }

                if (currentChar == '}' && !garbage) { 
                    sum = sum + depth;
                    depth--;
                }

                if (currentChar == '<')
                {
                    garbage = true;
                }

                if (currentChar == '>' && garbage)
                {
                    garbage = false;
                }

                if (currentChar == '!')
                {
                    skip = true;
                }
            }

            Console.WriteLine("SUM: " + sum);
            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _8._1
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> registers = new Dictionary<string, int>();
            int highest = 0;

            StreamReader fileInput = new StreamReader("../../input.txt");
            while (!fileInput.EndOfStream)
            {
                string line = fileInput.ReadLine();
                string[] splitLine = line.Split(' ');

                if (!registers.ContainsKey(splitLine[0]))
                {
                    registers.Add(splitLine[0], 0);
                }

                if (!registers.ContainsKey(splitLine[4]))
                {
                    registers.Add(splitLine[4], 0);
                }

                if (evaluateCondition(registers[splitLine[4]], splitLine[5], Int32.Parse(splitLine[6])))
                    {
                        int returned = getCommand(splitLine[1],Int32.Parse(splitLine[2]));
                        registers[splitLine[0]] += returned;
                    if (registers[splitLine[0]] > highest) highest = registers[splitLine[0]];
                    }
            }

            Console.WriteLine("Highest: " + highest);
            Console.ReadLine();
        }

        private static int getCommand(string command, int value)
        {
           
            return command.Equals("dec") ? -value : value;
        }

        private static bool evaluateCondition(int register, string condition, int value)
        {
            switch (condition)
            {
                case ">":
                    return register > value;
                case "<":
                    return register < value;
                case ">=":
                    return register >= value;
                case "<=":
                    return register <= value;
                case "==":
                    return register == value;
                case "!=":
                    return register != value;
            }
            return false;
        }
    }
}

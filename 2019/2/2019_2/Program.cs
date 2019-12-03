using System;
using System.Collections.Generic;
using System.IO;

namespace _2019_2
{
    class Program
    {
        static int[] instructions;

        static void Main(string[] args)
        {
            string input;
            using (TextReader tx = new StreamReader(@"input.txt"))
            {
                input = tx.ReadLine();
            }

            string[] aftersplit = input.Split(',');
            instructions = new int[aftersplit.Length];
            for(int i = 0; i < aftersplit.Length; i++)
            {
                instructions[i] = Convert.ToInt32(aftersplit[i]);
            }
            Run(0, 0);
        }

        public static void Run(int noun, int verb)
        {
            int[] tempInstructions = new int[instructions.Length];
            Array.Copy(instructions, tempInstructions, instructions.Length);

            tempInstructions[1] = noun;
            tempInstructions[2] = verb;

            for (int i = 0; i < tempInstructions.Length; i += 4)
            {
                int pos1 = tempInstructions[i + 1];
                int pos2 = tempInstructions[i + 2];
                int pos3 = tempInstructions[i + 3];

                bool validInstruction = false;

                if (tempInstructions[i] == 99)
                {
                    break;
                }

                int sum;
                if (tempInstructions[i] == 1)
                {
                    validInstruction = true;
                    sum = tempInstructions[pos1] + tempInstructions[pos2];
                    tempInstructions[pos3] = sum;
                }
                else if (tempInstructions[i] == 2)
                {
                    validInstruction = true;
                    sum = tempInstructions[pos1] * tempInstructions[pos2];
                    tempInstructions[pos3] = sum;
                }

                if (!validInstruction)
                {
                    Console.WriteLine("INVALID OP CODE");
                    break;
                }
            }

            if (tempInstructions[0] == 19690720)
            {
                Console.WriteLine("Part Two: " + ((noun * 100) + verb).ToString());
            } else
            {
                if (verb == 99)
                {
                    Run(noun + 1, 0);
                } else
                {
                    Run(noun, verb + 1);
                }
            }
        }
    }
}

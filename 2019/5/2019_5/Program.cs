using System;
using System.IO;

namespace _2019_5
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
            for (int i = 0; i < aftersplit.Length; i++)
            {
                instructions[i] = Convert.ToInt32(aftersplit[i]);
            }
            Run();
        }

        public static void Run()
        {
            int i = 0;
            while(i < instructions.Length)
            {
                string fullOpcode = instructions[i].ToString();
                while(fullOpcode.Length < 5)
                {
                    fullOpcode = "0" + fullOpcode;
                }
                string opcode = fullOpcode.Substring(3, 2);

                char parameterOneMode = fullOpcode[2];
                char parameterTwoMode = fullOpcode[1];
                char parameterThreeMode = fullOpcode[0];

                switch (opcode)
                {
                    case "01":
                        //Add
                        int destinationAdd = instructions[i + 3];
                        int paramOne = GetCorrectValue(instructions[i + 1], parameterOneMode);
                        int paramTwo = GetCorrectValue(instructions[i + 2], parameterTwoMode);
                        instructions[destinationAdd] = paramOne + paramTwo ;
                        i += 4;
                        break;
                    case "02":
                        //Multiply
                        int destinationMul = instructions[i + 3];
                        instructions[destinationMul] = GetCorrectValue(instructions[i + 1], parameterOneMode) * GetCorrectValue(instructions[i + 2], parameterTwoMode);
                        i += 4;
                        break;
                    case "03":
                        //Input
                        Console.Write("User Input: ");
                        int result = Convert.ToInt32(Console.ReadLine());
                        instructions[instructions[i + 1]] = result;
                        Console.WriteLine();
                        i += 2;
                        break;
                    case "04":
                        //Output
                        Console.WriteLine("OUTPUT: Value - " + GetCorrectValue(instructions[i + 1], parameterOneMode));
                        i += 2;
                        break;
                    case "05":
                        //jump-if-true
                        if (GetCorrectValue(instructions[i + 1], parameterOneMode) != 0) 
                        {
                            i = GetCorrectValue(instructions[i + 2], parameterTwoMode); 
                        } else
                        {
                            i += 3;
                        }
                        break;
                    case "06":
                        //jump-if-false
                        if (GetCorrectValue(instructions[i + 1], parameterOneMode) == 0) {
                            i = GetCorrectValue(instructions[i + 2], parameterTwoMode);
                        } else
                        {
                            i += 3;
                        }
                        break;
                    case "07":
                        //less than
                        int destLessThan = instructions[i + 3];
                        instructions[destLessThan] = (GetCorrectValue(instructions[i + 1], parameterOneMode) < GetCorrectValue(instructions[i + 2], parameterTwoMode)) ? 1 : 0;
                        i += 4;
                        break;
                    case "08":
                        //equals
                        int destEquals = instructions[i + 3];
                        instructions[destEquals] = (GetCorrectValue(instructions[i + 1], parameterOneMode) == GetCorrectValue(instructions[i + 2], parameterTwoMode)) ? 1 : 0;
                        i += 4;
                        break;
                    case "99":
                        i = int.MaxValue;
                        break;
                }

            }
        }

        public static int GetCorrectValue(int parameter, char parameterMode)
        {
            switch (parameterMode)
            {
                case '0':
                    return instructions[parameter];
                case '1':
                    return parameter;
                default:
                    return -999;
            }
        }
    }
}

using System;
using System.IO;

namespace _2015_8
{
    class Program
    {
        static void Main(string[] args)
        {
            PartOne();
            PartTwo();
        }

        public static void PartTwo()
        {
            int stringLiteralCount = 0;
            int newStringCount = 0;

            string input;
            using (TextReader tx = new StreamReader(@"input.txt"))
            {
                while ((input = tx.ReadLine()) != null)
                {
                    string newString = "\"";
                    stringLiteralCount += input.Length;
                    for(int i = 0; i < input.Length; i++)
                    {
                        if (input[i] != '\\' && input[i] != '\"')
                        {
                            newString += input[i];
                        } else
                        {
                            newString += "\\" + input[i];
                        }
                    }
                    newString += "\"";
                    newStringCount += newString.Length;
                    Console.WriteLine(input);
                    Console.WriteLine(newString);
                    Console.WriteLine("~~~~~~~~~");
                }
            }
            Console.WriteLine("PART ONE: ");
            Console.WriteLine("    Total String Literal Count: " + stringLiteralCount);
            Console.WriteLine("    Total New String Count: " + newStringCount);
            Console.WriteLine("    Subtracted: " + (newStringCount - stringLiteralCount) + "\n");
        }

        public static void PartOne()
        {
            int stringLiteralCount = 0;
            int totalMemoryCount = 0;

            string input;
            using (TextReader tx = new StreamReader(@"input.txt"))
            {
                while ((input = tx.ReadLine()) != null)
                {
                    stringLiteralCount += input.Length;

                    int memoryCount = 0;
                    for (int i = 0; i < input.Length; i++)
                    {
                        if (input[i] != '\\')
                        {
                            memoryCount++;
                            continue;
                        }
                        else
                        {
                            if (input[i + 1] == '\\' || input[i + 1] == '"')
                            {
                                memoryCount++;
                                i++;
                                continue;
                            }
                            else if (input[i + 1] == 'x')
                            {
                                memoryCount++;
                                i += 3;
                                continue;
                            }
                        }
                    }
                    // Remove two for start and end quote marks
                    memoryCount -= 2;
                    totalMemoryCount += memoryCount;
                }
            }
            Console.WriteLine("PART ONE: ");
            Console.WriteLine("    Total String Literal Count: " + stringLiteralCount);
            Console.WriteLine("    Total Memory Count: " + totalMemoryCount);
            Console.WriteLine("    Subtracted: " + (stringLiteralCount - totalMemoryCount) + "\n");
        }
    }
}

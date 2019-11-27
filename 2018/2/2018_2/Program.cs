using System;
using System.Collections.Generic;
using System.IO;

namespace _2018_2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> codes = new List<string>();
            int doubles = 0;
            int trebles = 0;

            using (TextReader tx = new StreamReader(@"input.txt"))
            {
                string line;
                while ((line = tx.ReadLine()) != null)
                {
                    codes.Add(line);
                }
            }

            foreach(string code in codes)
            {
                Console.WriteLine("Checking " + code);
                bool doubleCounted = false;
                bool trebleCounted = false;

                for(char i = 'a'; i < 'a' + 26; i++)
                {
                    int counter = 0;
                    for(int j = 0; j < code.Length; j++)
                    {
                        if (code[j] == i)
                        {
                            counter++;
                        }
                    }

                    if (counter == 2 && doubleCounted == false)
                    {
                        Console.WriteLine("    Double!");
                        doubles++;
                        doubleCounted = true;
                    }

                    if (counter == 3 && trebleCounted == false)
                    {
                        Console.WriteLine("    Treble!");
                        trebles++;
                        trebleCounted = true;
                    }
                }
            }

            Console.WriteLine("DOUBLES: " + doubles.ToString());
            Console.WriteLine("TREBLES: " + trebles.ToString());
            Console.WriteLine("CHECKSUM (dbl * trbl): " + (doubles * trebles).ToString());

            Console.WriteLine("Searching for match...");

            string firstCode = "";
            string secondCode = "";
            int finalDiffPos = 0;

            foreach(string code1 in codes)
            {
                foreach(string code2 in codes)
                {
                    if (code1.Equals(code2) || code1.Length != code2.Length)
                    {
                        continue;
                    }

                    bool differenceFound = false;
                    int differencePos = 0;

                    for(int i = 0; i < code1.Length; i++)
                    {
                        if (!code1[i].Equals(code2[i]))
                        {
                            if (differenceFound)
                            {
                                break;
                            } else
                            {
                                differencePos = i;
                                differenceFound = true;
                            }
                        }

                        if ((i == code1.Length - 1) && differenceFound)
                        {
                            firstCode = code1;
                            secondCode = code2;
                            finalDiffPos = differencePos;
                        }
                    }
                }
            }

            Console.WriteLine("CODE ONE: " + firstCode);
            Console.WriteLine("CODE TWO: " + secondCode);
            Console.WriteLine("RESULT: " + firstCode.Remove(finalDiffPos, 1));
        }
    }
}

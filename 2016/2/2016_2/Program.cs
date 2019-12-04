using System;
using System.Collections.Generic;
using System.IO;

namespace _2016_2
{
    class Program
    {
        static void Main(string[] args)
        {
            string passcode = "";
            char prevButton = '5';

            using (TextReader tx = new StreamReader(@"input.txt"))
            {
                string line;
                while ((line = tx.ReadLine()) != null)
                {
                    foreach(char direction in line)
                    {
                        prevButton = GetNextChar(prevButton, direction);
                        if (prevButton == '0')
                        {
                            Console.WriteLine("ERROR");
                            Environment.Exit(1);
                        }
                    }
                    passcode += prevButton.ToString();
                }
            }
            Console.WriteLine("Passcode: " + passcode);
        }

        public static char GetNextChar(char prevButton, char direction)
        {
            switch (direction)
            {
                case 'U':
                    switch (prevButton)
                    {
                        case '5':
                        case '2':
                        case '1':
                        case '4':
                        case '9':
                            return prevButton;
                        case '6':
                            return '2';
                        case 'A':
                            return '6';
                        case '3':
                            return '1';
                        case '7':
                            return '3';
                        case 'B':
                            return '7';
                        case 'D':
                            return 'B';
                        case '8':
                            return '4';
                        case 'C':
                            return '8';
                        default:
                            return '0';
                    }
                case 'D':
                    switch (prevButton)
                    {
                        case '5':
                        case 'A':
                        case 'D':
                        case 'C':
                        case '9':
                            return prevButton;
                        case '6':
                            return 'A';
                        case '2':
                            return '6';
                        case '1':
                            return '3';
                        case '3':
                            return '7';
                        case 'B':
                            return 'D';
                        case '8':
                            return 'C';
                        case '7':
                            return 'B';
                        case '4':
                            return '8';
                        default:
                            return '0';
                    }
                case 'L':
                    switch (prevButton)
                    {
                        case '1':
                        case '2':
                        case '5':
                        case 'A':
                        case 'D':
                            return prevButton;
                        case '3':
                            return '2';
                        case '4':
                            return '3';
                        case '6':
                            return '5';
                        case '7':
                            return '6';
                        case '8':
                            return '7';
                        case '9':
                            return '8';
                        case 'B':
                            return 'A';
                        case 'C':
                            return 'B';
                        default:
                            return '0';
                    }
                case 'R':
                    switch (prevButton)
                    {
                        case '1':
                        case '4':
                        case '9':
                        case 'C':
                        case 'D':
                            return prevButton;
                        case '2':
                            return '3';
                        case '3':
                            return '4';
                        case '5':
                            return '6';
                        case '6':
                            return '7';
                        case '7':
                            return '8';
                        case '8':
                            return '9';
                        case 'A':
                            return 'B';
                        case 'B':
                            return 'C';
                        default:
                            return '0';
                    }
                default:
                    return '0';
            }
        }
    }
}

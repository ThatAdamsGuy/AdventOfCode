using System;
using System.Collections.Generic;
using System.IO;

namespace _2015_6
{
    class Program
    {
        const int GRID_LENGTH = 1000;

        static void Main(string[] args)
        {
            PartOne();
            PartTwo();
        }

        private static void PartTwo()
        {
            int[,] lightArray = new int[GRID_LENGTH, GRID_LENGTH];
            for (int i = 0; i < GRID_LENGTH; i++)
            {
                for (int k = 0; k < GRID_LENGTH; k++)
                {
                    lightArray[i, k] = 0;
                }
            }

            string input;
            using (TextReader tx = new StreamReader(@"input.txt"))
            {
                while ((input = tx.ReadLine()) != null)
                {
                    string[] inputSplit = input.Split(' ');

                    if (inputSplit.Length == 4)
                    {
                        //TOGGLE
                        List<int> coordOne = GetSplitCoord(inputSplit[1]);
                        List<int> coordTwo = GetSplitCoord(inputSplit[3]);

                        for (int i = coordOne[0]; i <= coordTwo[0]; i++)
                        {
                            for (int k = coordOne[1]; k <= coordTwo[1]; k++)
                            {
                                lightArray[i, k] += 2;
                            }
                        }
                    }
                    else
                    {
                        //TURN ON/OFF
                        List<int> coordOne = GetSplitCoord(inputSplit[2]);
                        List<int> coordTwo = GetSplitCoord(inputSplit[4]);

                        for (int i = coordOne[0]; i <= coordTwo[0]; i++)
                        {
                            for (int k = coordOne[1]; k <= coordTwo[1]; k++)
                            {
                                if (inputSplit[1].Equals("on"))
                                {
                                    lightArray[i, k]++;
                                }
                                else if (inputSplit[1].Equals("off"))
                                {
                                    lightArray[i, k] = (lightArray[i,k] == 0) ? 0 : (lightArray[i,k] - 1);
                                }
                            }
                        }
                    }
                }
            }

            int totalBrightness = 0;
            for (int i = 0; i < GRID_LENGTH; i++)
            {
                for (int k = 0; k < GRID_LENGTH; k++)
                {
                    totalBrightness += lightArray[i, k];
                }
            }

            Console.WriteLine("Total Brightness: " + totalBrightness);
        }

        private static void PartOne()
        {
            int[,] lightArray = new int[GRID_LENGTH, GRID_LENGTH];
            for (int i = 0; i < GRID_LENGTH; i++)
            {
                for (int k = 0; k < GRID_LENGTH; k++)
                {
                    lightArray[i, k] = 0;
                }
            }

            string input;
            using (TextReader tx = new StreamReader(@"input.txt"))
            {
                while ((input = tx.ReadLine()) != null)
                {
                    string[] inputSplit = input.Split(' ');

                    if (inputSplit.Length == 4)
                    {
                        //TOGGLE
                        List<int> coordOne = GetSplitCoord(inputSplit[1]);
                        List<int> coordTwo = GetSplitCoord(inputSplit[3]);

                        for (int i = coordOne[0]; i <= coordTwo[0]; i++)
                        {
                            for (int k = coordOne[1]; k <= coordTwo[1]; k++)
                            {
                                lightArray[i, k] = (lightArray[i, k] == 0) ? 1 : 0;
                            }
                        }
                    }
                    else
                    {
                        //TURN ON/OFF
                        List<int> coordOne = GetSplitCoord(inputSplit[2]);
                        List<int> coordTwo = GetSplitCoord(inputSplit[4]);

                        for (int i = coordOne[0]; i <= coordTwo[0]; i++)
                        {
                            for (int k = coordOne[1]; k <= coordTwo[1]; k++)
                            {
                                if (inputSplit[1].Equals("on"))
                                {
                                    lightArray[i, k] = 1;
                                }
                                else if (inputSplit[1].Equals("off"))
                                {
                                    lightArray[i, k] = 0;
                                }
                            }
                        }
                    }
                }
            }

            int lightsOn = 0;
            for (int i = 0; i < GRID_LENGTH; i++)
            {
                for (int k = 0; k < GRID_LENGTH; k++)
                {
                    if (lightArray[i, k] == 1) lightsOn++;
                }
            }
            Console.WriteLine("Lights On: " + lightsOn);
        }

        private static List<int> GetSplitCoord(string input)
        {
            string[] coordString = input.Split(',');
            return new List<int>() { int.Parse(coordString[0]), int.Parse(coordString[1]) };
        }
    }
}

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day05
    {
        public static void Run()
        {
            List<string> input = File.ReadAllLines("day05Input.txt").ToList();
            List<int> seatIDs = new List<int>();
            foreach (var line in input)
            {
                //int rowMid = 64;
                //int seatMid = 4;
                string rowString = "";
                string seatString = "";
                int counter = 0;
                foreach (char each in line)
                {
                    if (counter >= 7)
                    {
                        if (each == 'L')
                        {
                            seatString += '0';
                        }
                        else
                        {
                            seatString += '1';
                        }
                    }
                    else
                    {
                        if (each == 'F')
                        {
                            rowString += '0';
                        }
                        else
                        {
                            rowString += '1';
                        }
                    }
                    counter++;
                }

                int row = Convert.ToInt32(rowString, 2);
                int seat = Convert.ToInt32(seatString, 2);

                int ID = ((row * 8) + seat);
                seatIDs.Add(ID);
            }
            seatIDs.Sort();
            Console.WriteLine($"Highest SeatID: {seatIDs.Last()}");
            for (int i = 1; i < seatIDs.Count(); i++)
            {
                if (seatIDs[i - 1] != seatIDs[i] - 1)
                {
                    Console.WriteLine($"Missing SeatID: {seatIDs[i] - 1}");
                    break;
                } else if (seatIDs[i + 1] != seatIDs[i] + 1)
                {
                    Console.WriteLine($"Missing SeatID: {seatIDs[i] + 1}");
                    break;
                }
            }
        }
    }
}

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day13
    {
        public static bool debug = false;
        public static void Run()
        {
            int timeStamp = 1;
            string busIntervals = "";

            if (!debug)
            {
                timeStamp = 1002632;
                busIntervals = "23,x,x,x,x,x,x,x,x,x,x,x,x,41,x,x,x,x,x,x,x,x,x,829,x,x,x,x,x,x,x,x,x,x,x,x,13,17,x,x,x,x,x,x,x,x,x,x,x,x,x,x,29,x,677,x,x,x,x,x,37,x,x,x,x,x,x,x,x,x,x,x,x,19";
            }
            else
            {
                timeStamp = 939;
                busIntervals = "1789,37,47,1889";
            }

            var split = busIntervals.Split(',');
            List<string> buses = new List<string>();
            foreach (var item in split)
            {
                if (!item.Equals("x"))
                {
                    buses.Add(item);
                }
            }
            int id = 0;
            int incrementingTimestramp = timeStamp;
            bool breakLoop = false;
            while (!breakLoop)
            {
                foreach (var bus in buses)
                {
                    int busID = int.Parse(bus);
                    if (incrementingTimestramp % busID == 0 && incrementingTimestramp > timeStamp)
                    {
                        Console.WriteLine($"Bus ID: {busID}");
                        Console.WriteLine($"Minutes: {incrementingTimestramp - timeStamp}");
                        Console.WriteLine($"Multiplied: {busID * (incrementingTimestramp - timeStamp)}");
                        breakLoop = true;
                    }
                }
                incrementingTimestramp++;
            }
            Console.WriteLine();

            //Part 2

            List<string> part2Inputs = busIntervals.Split(',').ToList();
            int addition = int.Parse(part2Inputs[0]);

            breakLoop = false;
            long newTimestamp = 100000000000000;
            while (newTimestamp % addition != 0) newTimestamp++;
            while (!breakLoop)
            {
                bool validLoop = true;
                int counter = 0;
                for (int i = 0; i < part2Inputs.Count(); i++)
                {
                    if(part2Inputs[i] == "x")
                    {
                        counter++;
                        continue;
                    }
                    else if((newTimestamp + counter) % int.Parse(part2Inputs[i]) == 0)
                    {
                        counter++;
                        continue;
                    } else
                    {
                        validLoop = false;
                        break;
                    }
                }
                if (validLoop)
                {
                    breakLoop = true;
                } else
                {
                    newTimestamp += addition;
                }
            }
            Console.Write($"Timestamp: {newTimestamp}");
        }
    }
}

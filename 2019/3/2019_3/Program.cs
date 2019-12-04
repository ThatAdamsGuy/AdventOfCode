using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace _2019_3
{
    public struct Coordinate
    {
        public bool wireOne;
        public int y;
        public int x;
        public int wireOneSteps;
        public int wireTwoSteps;
    }


    class Program
    {
        static List<Coordinate> wireList;
        static List<Coordinate> duplicates;
        static Coordinate lastValue;

        static int stepCounter = 0;

        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            string wireOne;
            string wireTwo;
            wireList = new List<Coordinate>();
            duplicates = new List<Coordinate>();
            lastValue = new Coordinate { x = 0, y = 0 };

            using (TextReader tx = new StreamReader(@"input.txt"))
            {
                wireOne = tx.ReadLine();
                wireTwo = tx.ReadLine();
            }

            List<string> wireOneInstructions = wireOne.Split(',').OfType<string>().ToList();
            List<string> wireTwoInstructions = wireTwo.Split(',').OfType<string>().ToList();

            foreach (string item in wireOneInstructions)
            {
                Console.WriteLine("Instruction: " + item);
                char direction = item[0];
                int length = Convert.ToInt32(item.Substring(1));
                TraceString(direction, length, true);
            }

            stepCounter = 0;
            lastValue = new Coordinate { x = 0, y = 0, wireOne = true };

            foreach (string item in wireTwoInstructions)
            {
                Console.WriteLine("Instruction: " + item);
                char direction = item[0];
                int length = Convert.ToInt32(item.Substring(1));
                TraceString(direction, length, false);
            }

            List<int> distances = new List<int>();
            List<int> combinedSteps = new List<int>();

            foreach(Coordinate each in duplicates)
            {
                if (each.x == 0 && each.y == 0)
                {
                    continue;
                }
                else
                {
                    combinedSteps.Add(each.wireOneSteps + each.wireTwoSteps);
                    distances.Add(Math.Abs(each.x) + Math.Abs(each.y));
                }
            }

            Console.WriteLine("Lowest: " + distances.Min());
            Console.WriteLine("Fewest combined steps: " + combinedSteps.Min());
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("Memory used: " + System.GC.GetTotalMemory(false));
            Console.WriteLine("RunTime: " + elapsedTime);
        }

            public static void TraceString(char direction, int length, bool isWireOne)
        {
            for(int i = 0; i < length; i++)
            {
                stepCounter++;
                Coordinate newCoord = new Coordinate { x = lastValue.x, y = lastValue.y, wireOne = isWireOne };
                if (isWireOne)
                {
                    newCoord.wireOneSteps = stepCounter;
                } else
                {
                    newCoord.wireTwoSteps = stepCounter;
                }
                switch (direction)
                {
                    case 'U':
                        newCoord.y++;
                        break;
                    case 'D':
                        newCoord.y--;
                        break;
                    case 'R':
                        newCoord.x++;
                        break;
                    case 'L':
                        newCoord.x--;
                        break;
                }
                bool duplicateFound = false;
                foreach(Coordinate duplicate in wireList)
                {
                    if ((duplicate.x == newCoord.x) && (duplicate.y == newCoord.y) && (duplicate.wireOne != newCoord.wireOne))
                    {
                        duplicateFound = true;
                        newCoord.wireOneSteps = duplicate.wireOneSteps;
                        duplicates.Add(newCoord);
                    }
                }

                if (!duplicateFound)
                {
                    wireList.Add(newCoord);
                }

                lastValue = newCoord;
            }
        }
    }
}

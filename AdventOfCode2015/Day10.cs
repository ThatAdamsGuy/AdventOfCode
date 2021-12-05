using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
    class Day10
    {
        public static void Run()
        {
            string input = "1113122113";
            string currentIteration = input;

            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            for (int i = 0; i < 50; i++)
            {
                string currentSection = "";
                char? lastChar = null;
                int count = 1;

                foreach (char item in currentIteration)
                {
                    if (lastChar is null)
                    {
                        lastChar = item;
                        continue;
                    }

                    if (item != lastChar)
                    {
                        currentSection += count.ToString() + lastChar.ToString();
                        count = 0;
                        lastChar = item;
                    }

                    count++;
                }
                currentSection += count.ToString() + lastChar.ToString();
                currentIteration = currentSection;
                if(i == 39)
                {
                    // Get the elapsed time as a TimeSpan value.
                    TimeSpan ts1 = stopWatch.Elapsed;
                    Console.WriteLine("Part 1 - " + currentIteration.Length);
                    // Format and display the TimeSpan value.
                    string elapsedTimeOne = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                        ts1.Hours, ts1.Minutes, ts1.Seconds,
                        ts1.Milliseconds / 10);
                    Console.WriteLine("RunTime " + elapsedTimeOne);
                }
            }
            Console.WriteLine("Part 2 - " + currentIteration.Length);

            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime " + elapsedTime);
            stopWatch.Stop();
        }
    }


}

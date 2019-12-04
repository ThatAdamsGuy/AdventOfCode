using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace _2019_4_attempt2
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("Running");
            int counter = 0;
            for (int i = 245318; i <= 765747; i++)
            {
                bool adjacentDigits = false;
                bool increasing = true;
                int matchCount = 0;
                int[] splitNumbers = NumbersIn(i).ToArray();
                Array.Reverse(splitNumbers);

                for (int j = 0; j < splitNumbers.Length - 1; j++)
                {
                    if (splitNumbers[j] == splitNumbers[j + 1])
                        matchCount += 1;
                    else
                    {
                        if (matchCount == 1)
                            adjacentDigits = true;

                        matchCount = 0;
                    }

                    if (splitNumbers[j] > splitNumbers[j + 1])
                    {
                        increasing = false;
                        break;
                    }
                }
                if (matchCount == 1)
                    adjacentDigits = true;

                if (adjacentDigits && increasing)
                {
                    counter++;
                }
            }
            Console.WriteLine("Valid answers: " + counter);
            stopwatch.Stop();

            TimeSpan ts = stopwatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("RunTime: " + elapsedTime);
        }

        public static Stack<int> NumbersIn(int value)
        {
            if (value == 0) return new Stack<int>();
            var numbers = NumbersIn(value / 10);
            numbers.Push(value % 10);
            return numbers;
        }
    }
}

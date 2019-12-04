using System;
using System.Diagnostics;

namespace _2019_4
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Console.WriteLine("Running");
            int counter = 0;
            for(int i = 245318; i <= 765747; i++)
            {
                string password = i.ToString();
                bool adjacentDigits = false;
                bool increasing = true;

                for(int j = 0; j < 6; j++)
                {
                    try
                    {
                        if (password[j] == password[j] + 1)
                        {
                            adjacentDigits = true;
                        }

                        if (Convert.ToInt32(password[j]) < Convert.ToInt32(password[j - 1]))
                        {
                            increasing = false;
                        }

                    } catch (IndexOutOfRangeException){
                        
                    }
                }
                if(adjacentDigits && increasing)
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
    }
}

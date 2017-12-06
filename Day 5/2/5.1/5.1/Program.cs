using System;
using System.Collections.Generic;
using System.IO;

namespace _5._1
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> inputs = new List<int>();
            StreamReader sr = new StreamReader("../../input.txt");
        
            while (!sr.EndOfStream)
            {
                inputs.Add(Int32.Parse(sr.ReadLine()));
            }

            int currentPosition = 0;
            int toGo = 0;
            int jumps = 0;

            bool outOfArray = false;

            while(!outOfArray){
                try
                {
                    toGo = currentPosition + inputs[currentPosition];
                    if (inputs[currentPosition] >= 3)
                    {
                        inputs[currentPosition]--;
                    }
                    else
                    {
                        inputs[currentPosition]++;
                    }
                    currentPosition = toGo;
                    jumps++;
                } catch
                {
                    outOfArray = true;
                }
            }

            Console.WriteLine("TOTAL JUMPS: " + jumps);
            Console.ReadLine();
        }
    }
}

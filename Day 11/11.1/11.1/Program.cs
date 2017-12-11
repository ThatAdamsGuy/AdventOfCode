using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _11._1
{
    class Program
    {
        static void Main(string[] args)
        {
            //int[0] is north, [1] NE, etc.
            int sum = 0;
            int[] counter = new int[6] { 0, 0, 0, 0, 0, 0 };
            List<string> input = new List<string>();
            StreamReader stream = new StreamReader("../../input.txt");
            while (!stream.EndOfStream)
            {
                string line = stream.ReadLine();
                input.AddRange(line.Split(','));
            }

            foreach (string direction in input)
            {
                switch (direction)
                {
                    case "n":
                        counter[0]++;
                        break;
                    case "ne":
                        counter[1]++;
                        break;
                    case "se":
                        counter[2]++;
                        break;
                    case "s":
                        counter[3]++;
                        break;
                    case "sw":
                        counter[4]++;
                        break;
                    case "nw":
                        counter[5]++;
                        break;
                }
            }

            if (counter[0] > counter[3])
            {
                sum += counter[0] - counter[3];
            }
            else if (counter[0] < counter[3])
            {
                sum += counter[3] - counter[0];
            }

            if (counter[1] > counter[4])
            {
                sum += counter[1] - counter[4];
            }
            else if (counter[1] < counter[4])
            {
                sum += counter[4] - counter[1];
            }

            if (counter[2] > counter[5])
            {
                sum += counter[2] - counter[5];
            }
            else if (counter[2] < counter[5])
            {
                sum += counter[5] - counter[2];
            }

            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine("Counter " + i + ": " + counter[i]);
            }
            Console.WriteLine("SUM: " + sum);
            Console.ReadLine();
        }
    }
}

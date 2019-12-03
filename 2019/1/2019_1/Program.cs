using System;
using System.IO;

namespace _2019_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int basicSum = 0;
            int complexSum = 0;

            using (TextReader tx = new StreamReader(@"input.txt"))
            {
                string line;
                //Run through every line in file
                while ((line = tx.ReadLine()) != null)
                {
                    int value = int.Parse(line);
                    basicSum += Convert.ToInt32(Math.Floor(value / 3.0)) - 2;
                    Console.WriteLine("STARTING VALUE: " + value);
                    complexSum += GetAdditionalFuel(value, 0);
                }
            }
            Console.WriteLine(basicSum);
            Console.WriteLine(complexSum);
        }

        public static int GetAdditionalFuel(int value, int totalFuel)
        {
            int total = totalFuel;
            int fuelConversion = Convert.ToInt32(Math.Floor(value / 3.0)) - 2;
            Console.WriteLine(fuelConversion.ToString());

            if (fuelConversion <= 0)
            {
                return total;
            } else
            {
                total += fuelConversion;
                return GetAdditionalFuel(fuelConversion, total);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _15._1
{
    class Program
    {
        static void Main(string[] args)
        {
            long generatorA = 883;
            long generatorB = 879;
            long factorA = 16807;
            long factorB = 48271;
            long division = 2147483647;
            int sum = 0;

            for (int i = 0; i < 5000000; i++)
            {
                bool validA = false;
                bool validB = false;

                while (!validA)
                {
                    generatorA = (generatorA * factorA) % division;
                    if(generatorA % 4 == 0)
                    {
                        validA = true;
                    }
                }

                while (!validB)
                {
                    generatorB = (generatorB * factorB) % division;
                    if (generatorB % 8 == 0)
                    {
                        validB = true;
                    }
                }

                string binaryA = Convert.ToString(generatorA, 2);
                string binaryB = Convert.ToString(generatorB, 2);

                if (binaryA.Substring(Math.Max(0, binaryA.Length - 16)).Equals(binaryB.Substring(Math.Max(0, binaryB.Length - 16)))){
                    sum++;
                }
                if(i % 50000 == 0)
                {
                    Console.WriteLine(i);
                }
            }
            Console.WriteLine("SUM: " + sum);
            Console.ReadLine();
        }
    }
}

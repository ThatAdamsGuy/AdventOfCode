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

            for (int i = 0; i < 40000000; i++)
            {
                long curValueA = (generatorA * factorA) % division;
                long curValueB = (generatorB * factorB) % division;

                string binaryA = Convert.ToString(curValueA, 2);
                string binaryB = Convert.ToString(curValueB, 2);

                if (binaryA.Substring(Math.Max(0, binaryA.Length - 16)).Equals(binaryB.Substring(Math.Max(0, binaryB.Length - 16)))){
                    sum++;
                }
                generatorA = curValueA;
                generatorB = curValueB;
                if(i % 100000 == 0)
                {
                    Console.WriteLine(i);
                }
            }
            Console.WriteLine("SUM: " + sum);
            Console.ReadLine();
        }
    }
}

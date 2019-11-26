using System;
using System.IO;

namespace _2015_1
{
    class Program
    {
        static void Main(string[] args)
        {
            string input;
            int position = 0;
            int positionForBasement = -1;
            bool hasReachedBasement = false;

            using (TextReader tx = new StreamReader(@"input.txt"))
            {
                input = tx.ReadLine();
            }
            Console.WriteLine("INPUT: " + input + '\n');
            for(int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(')
                {
                    position++;
                } else if (input[i] == ')')
                {
                    position--;
                }

                if (!hasReachedBasement && position == -1)
                {
                    positionForBasement = i + 1;
                    hasReachedBasement = true;
                }
            }

            Console.WriteLine("Final floor: " + position);
            Console.WriteLine("First basement entry: " + positionForBasement.ToString());
            Console.ReadLine();
        }
    }
}

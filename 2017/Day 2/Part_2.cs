using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _2._1
{
    class Program
    {
        static void Main(string[] args)
        {
            int sum = 0;
            List<string> file = File.ReadAllLines("../../input.csv").ToList();
            foreach(string line in file)
            {
                List<string> words = line.Split(',').ToList();
                foreach(string word in words)
                {
                    int position = words.IndexOf(word);

                    foreach(string secondWord in words)
                    {
                        int secondPosition = words.IndexOf(secondWord);

                        if((position != secondPosition) && (Int32.Parse(word) % Int32.Parse(secondWord) == 0)){
                            sum += Int32.Parse(word) / Int32.Parse(secondWord);
                        }
                    }
                }
            }

            Console.WriteLine("SUM: " + sum);
            Console.ReadLine();

        }
    }
}

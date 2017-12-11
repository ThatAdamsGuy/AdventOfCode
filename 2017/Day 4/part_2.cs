using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4._1
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            List<string> stringtxt = new List<string>();
            StreamReader sr = new StreamReader("../../input.txt");

            int valid = 0;
            string[] lines = File.ReadAllLines("../../input.txt");
            foreach (string line in lines)
            {
                List<string> segments = line.Split(' ').ToList();
                bool anagram = false;
                foreach (string word in segments)
                {
                    foreach (string secondWord in segments)
                    {
                        if (!word.Equals(secondWord) && IsAnagramSimple(word, secondWord)){
                            anagram = true;
                        }
                    }
                }


                if (!anagram && segments.Distinct().Count() == segments.Count)
                {
                    valid++;
                }
            }
            Console.WriteLine(valid);
            Console.ReadLine();
            


        }

        private static bool IsAnagramSimple(string a, string b)
        {
            return a.OrderBy(c => c).SequenceEqual(b.OrderBy(c => c));
        }
    }
}

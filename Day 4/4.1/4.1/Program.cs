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
            foreach (var line in lines)
            {
                List<string> segments = line.Split(' ').ToList();

                if (segments.Distinct().Count() == segments.Count)
                {
                    valid++;
                }
            }
            Console.WriteLine(valid);

            while (!sr.EndOfStream)
            {
                stringtxt = sr.ReadLine().Split(' ').ToList<string>();
            }
            count = stringtxt.Distinct().Count();

            Console.WriteLine("VALID NUMBER: " + count);
            Console.ReadLine();


        }
    }
}

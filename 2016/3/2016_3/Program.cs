using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _2016_3
{
    class Program
    {
        static void Main(string[] args)
        {
            int validTriangles = 0;
            Console.WriteLine("Running");
            List<string> triangles = new List<string>();

            using (TextReader tx = new StreamReader(@"input.txt"))
            {
                string line;
                while ((line = tx.ReadLine()) != null)
                {
                    triangles.Add(line);
                }
            }

            foreach(string item in triangles)
            {
                List<string> sides = item.Split(' ').OfType<string>().ToList().Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
                List<int> sidesInts = sides.Select(int.Parse).ToList();
                sidesInts.Sort();
                if (sidesInts.Count != 0 && (sidesInts[0] + sidesInts[1]) > sidesInts[2])
                {
                    validTriangles++;
                }
            }

            Console.WriteLine("PART 1 Valid triangles: " + validTriangles);
            validTriangles = 0;

            for(int i = 0; i < triangles.Count; i += 3)
            {
                List<int> sidesOne = triangles[i].Split(' ').OfType<string>().ToList().Where(s => !string.IsNullOrWhiteSpace(s)).ToList().Select(int.Parse).ToList();
                List<int> sidesTwo = triangles[i+1].Split(' ').OfType<string>().ToList().Where(s => !string.IsNullOrWhiteSpace(s)).ToList().Select(int.Parse).ToList();
                List<int> sidesThree = triangles[i+2].Split(' ').OfType<string>().ToList().Where(s => !string.IsNullOrWhiteSpace(s)).ToList().Select(int.Parse).ToList();

                for(int k = 0; k < 3; k++)
                {
                    List<int> vertTriangle = new List<int>() { sidesOne[k], sidesTwo[k], sidesThree[k] };
                    vertTriangle.Sort();
                    if ((vertTriangle[0] + vertTriangle[1]) > vertTriangle[2])
                    {
                        validTriangles++;
                    }
                }
            }

            Console.WriteLine("PART 2 Valid triangles: " + validTriangles);
        }
    }
}

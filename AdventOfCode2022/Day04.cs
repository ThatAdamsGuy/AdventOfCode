using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day04
    {
        public static void Run()
        {
            int p1Sum = 0;
            int p2Sum = 0;
            foreach (string line in File.ReadLines("Day04Input.txt"))
            {
                var pairs = line.Split(',');
                var one = pairs[0].Split('-');
                var two = pairs[1].Split('-');

                var oneStart = int.Parse(one[0]);
                var twoStart = int.Parse(two[0]);
                var oneFin = int.Parse(one[1]);
                var twoFin = int.Parse(two[1]);

                if((oneStart >= twoStart && oneFin <= twoFin)
                    || (twoStart >= oneStart && twoFin <= oneFin))
                {
                    p1Sum++;
                }
                
                if((oneStart >= twoStart && oneStart <= twoFin)
                    || (oneFin >= twoStart && oneFin <= twoFin)
                    || (twoStart >= oneStart && twoStart <= oneFin)
                    || (twoFin >= oneStart && twoFin <= oneFin))
                {
                    p2Sum++;
                }
            }
            Console.WriteLine("Part 1 - " + p1Sum);
            Console.WriteLine("Part 2 - " + p2Sum);
        }
    }
}

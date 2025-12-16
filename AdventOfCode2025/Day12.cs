using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025
{
    internal class Day12
    {
        public static void Run()
        {
            var input = File.ReadAllLines("day12input.txt");
            int count = 0;

            // Skip the first 24 lines (pattern definitions) and process coordinate lines
            string[] lines = input.Skip(24).ToArray();

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] parts = line.Split(':');
                if (parts.Length < 2) continue;

                // Parse coordinates from "47x41" format
                string[] coords = parts[0].Split('x');
                if (coords.Length < 2) continue;

                // Safely parse width and height
                if (!int.TryParse(coords[0], out int width) || 
                    !int.TryParse(coords[1], out int height))
                    continue;

                // Parse the numbers after the colon
                string[] nums = parts[1].Trim().Split(' ');

                int amnt = (width / 3) * (height / 3);

                foreach (string num in nums)
                {
                    if (int.TryParse(num, out int value))
                    {
                        amnt -= value;
                    }
                }

                if (amnt >= 0) count++;
            }

            Console.WriteLine(count);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
    class Day16
    {
        private static int children = 3;
        private static int cats = 7;
        private static int samoyeds = 2;
        private static int pomeranians = 3;
        private static int akitas = 0;
        private static int vizslas = 0;
        private static int goldfish = 5;
        private static int trees = 3;
        private static int cars = 2;
        private static int perfumes = 1;

        public static void Run()
        {
            List<int> sues = new List<int>();
            var lines = System.IO.File.ReadAllLines("day16input.txt");
            for(int i = 1; i <= lines.Length; i++)
            {
                var line = lines[i - 1];
                var parts = line.Replace(":", "").Replace(",", "").Split(' ');
                var sueNumber = int.Parse(parts[1]);
                var attributes = new Dictionary<string, int>();
                for(int j = 2; j < parts.Length; j += 2)
                {
                    attributes[parts[j]] = int.Parse(parts[j + 1]);
                }
                bool isPt1Match = true;
                foreach(var attribute in attributes)
                {
                    switch (attribute.Key)
                    {
                        case "children":
                            if(attribute.Value != children) isPt1Match = false;
                            break;
                        case "cats":
                            if(attribute.Value != cats) isPt1Match = false;
                            break;
                        case "samoyeds":
                            if(attribute.Value != samoyeds) isPt1Match = false;
                            break;
                        case "pomeranians":
                            if(attribute.Value != pomeranians) isPt1Match = false;
                            break;
                        case "akitas":
                            if(attribute.Value != akitas) isPt1Match = false;
                            break;
                        case "vizslas":
                            if(attribute.Value != vizslas) isPt1Match = false;
                            break;
                        case "goldfish":
                            if(attribute.Value != goldfish) isPt1Match = false;
                            break;
                        case "trees":
                            if(attribute.Value != trees) isPt1Match = false;
                            break;
                        case "cars":
                            if(attribute.Value != cars) isPt1Match = false;
                            break;
                        case "perfumes":
                            if(attribute.Value != perfumes) isPt1Match = false;
                            break;
                    }
                }
                bool isPt2Match = true;
                foreach (var attribute in attributes)
                {
                    switch (attribute.Key)
                    {
                        case "children":
                            if (attribute.Value != children) isPt2Match = false;
                            break;
                        case "cats":
                            if (attribute.Value <= cats) isPt2Match = false;
                            break;
                        case "samoyeds":
                            if (attribute.Value != samoyeds) isPt2Match = false;
                            break;
                        case "pomeranians":
                            if (attribute.Value >= pomeranians) isPt2Match = false;
                            break;
                        case "akitas":
                            if (attribute.Value != akitas) isPt2Match = false;
                            break;
                        case "vizslas":
                            if (attribute.Value != vizslas) isPt2Match = false;
                            break;
                        case "goldfish":
                            if (attribute.Value >= goldfish) isPt2Match = false;
                            break;
                        case "trees":
                            if (attribute.Value <= trees) isPt2Match = false;
                            break;
                        case "cars":
                            if (attribute.Value != cars) isPt2Match = false;
                            break;
                        case "perfumes":
                            if (attribute.Value != perfumes) isPt2Match = false;
                            break;
                    }
                }
                if (isPt1Match)
                {
                    Console.WriteLine("Part 1 - " + i); 
                }
                if (isPt2Match)
                {
                    Console.WriteLine("Part 2 - " + i);
                }
            }
            foreach(var sue in sues)
            {
                Console.WriteLine("Matching Sue: " + sue);
            }
        }
    }
}

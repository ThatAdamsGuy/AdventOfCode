using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day07
    {
        public static List<string> inputs { get; set; }

        public static void Run()
        {
            inputs = File.ReadAllLines("day07Input.txt").ToList();
            int counter = 0;
            foreach(string line in inputs)
            {
                if (TraceBag(line)) counter++;
            }
            Console.WriteLine($"{counter} bags can eventually contain shiny gold");

            string goldBag = inputs.Where(s => s.StartsWith("shiny gold bags")).Single();
            Console.WriteLine($"Shiny gold bags contain {CountBags(goldBag)} bags");
        }

        public static bool TraceBag(string bag)
        {
            var split = bag.Split(new string[] { " contain " }, StringSplitOptions.None);
            if(split[1].Contains("shiny gold"))
            {
                return true;
            } else if (split[1].Contains("no other bags"))
            {
                return false;
            } else
            {
                if (split[1].Contains(','))
                {
                    var bagsToCheck = split[1].Trim('.').Split(new string[] { ", " }, StringSplitOptions.None);
                    foreach(string newBag in bagsToCheck)
                    {
                        if (TraceBag(inputs.Where(s => s.StartsWith(newBag.Substring(2))).Single()))
                        {
                            return true;
                        }
                    }
                    return false;
                } else
                {
                    return TraceBag(inputs.Where(s => s.StartsWith(split[1].Substring(2).Trim('.'))).Single());
                }
            }
        }

        public static int CountBags(string bag)
        {
            if (bag.Contains("no other bags.")) return 0;
            var split = bag.Split(new string[] { " contain " }, StringSplitOptions.None);

            int sum = 0;
            if (split[1].Contains(','))
            {
                var bags = split[1].Split(new string[] { ", " }, StringSplitOptions.None);
                foreach(var newBag in bags)
                {
                    int num = int.Parse(newBag[0].ToString());
                    sum += num + (num * CountBags(inputs.Where(s => s.StartsWith(newBag.Substring(2).Trim('.'))).Single()));
                }
                return sum;
            } else
            {
                int num = int.Parse((split[1])[0].ToString());
                return num + (num * CountBags(inputs.Where(s => s.StartsWith(split[1].Substring(2).Trim('.'))).Single()));
            }
        }
    }
}

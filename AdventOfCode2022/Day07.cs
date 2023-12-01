using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day07
    {
        public class Item
        {
            public string Name { get; set; }
            public int Size { get; set; }
        }

        public class Directory
        {
            public Directory Parent { get; set; }
            public string Name { get; set; }
            public List<Directory> Children { get; set; }
            public List<Item > Files { get; set; }
        }
        public static void Run()
        {
            Directory rootDir = new Directory()
            {
                Name = "/"
            };
            Directory curDir = rootDir;

            var lines = File.ReadAllLines("Day07Input.txt");

            bool listing = false;
            foreach (var line in lines)
            {

            }
        }
    }
}

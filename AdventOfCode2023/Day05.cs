using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal class Day05
    {
        public static void Run()
        {
            // Parse Inputs
            var lines = new Queue<string>(File.ReadAllLines("Day05Input.txt"));
            var seeds = lines.Dequeue().Split(':')[1].Trim().Split(' ').Select(x => int.Parse(x));

            // Skip Empty and Title
            lines.Dequeue();
            lines.Dequeue();

            while (true)
            {
                var vals = lines.Dequeue().Split(' ').Select(x => int.Parse(x));
            }
        }

        private class Category
        {
            public List<Range> Ranges = new List<Range>();
        }

        private class Range
        {
            public int source;
            public int destination;
            public int length;

            public Range(int start, int end, int length)
            {
                this.source = start;
                this.destination = end;
                this.length = length;
            }
        }
    }
}
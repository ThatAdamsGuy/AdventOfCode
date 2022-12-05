using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day05
    {
        internal class CrateStack
        {
            public string Name { get; set; }
            public Stack<string> P1Crates { get; set; }
            public Stack<string> P2Crates { get; set; }

            public CrateStack(string name)
            {
                Name = name;
                P1Crates = new Stack<string>();
                P2Crates = new Stack<string>();
            }
        }

        public static void Run()
        {
            var input = File.ReadAllLines("Day05Input.txt").ToList();
            List<string> startingConfig = new List<string>();
            List<CrateStack> stacks = new List<CrateStack>();

            //Separate the starting config
            for (int i = 0; i < input.Count(); i++)
            {
                if (string.IsNullOrEmpty(input[0]))
                {
                    input.RemoveAt(0);
                    break;
                }

                startingConfig.Add(input.First());
                input.RemoveAt(0);
            }                                     
            stacks = PopulateStacks(startingConfig);

            foreach (var line in input)
            {
                var split = line.Split(' ');
                var tmpStack = new Stack<string>();
                for (int i = 0; i < int.Parse(split[1]); i++)
                {
                    var stack = stacks.Single(x => x.Name == split[3]); 

                    var p1Crate = stack.P1Crates.Pop();
                    var p2Crate = stack.P2Crates.Pop();
                    stacks.Single(x => x.Name == split[5]).P1Crates.Push(p1Crate);
                    tmpStack.Push(p2Crate);
                }

                tmpStack = new Stack<string>(tmpStack.Reverse());
                foreach (var crate in tmpStack)
                {
                    stacks.Single(x => x.Name == split[5]).P2Crates.Push(crate);
                }
            }

            string p1message = "";
            string p2message = "";
            foreach (var crate in stacks)
            {
                p1message += crate.P1Crates.Pop();
                p2message += crate.P2Crates.Pop();
            }

            Console.WriteLine("Part 1 - " + p1message);
            Console.WriteLine("Part 2 - " + p2message);
        }

        private static List<CrateStack> PopulateStacks(List<string> startingConfig)
        {
            List<CrateStack> stacks = new List<CrateStack>();
            for (int i = startingConfig.Count() - 1; i >= 0; i--)
            {
                if (i == startingConfig.Count() - 1)
                {
                    // First line, get the number of stacks
                    var tmp = startingConfig[i].Split(' ').ToList();
                    tmp.RemoveAll(x => string.IsNullOrEmpty(x));

                    foreach (var item in tmp)
                    {
                        stacks.Add(new CrateStack(item));
                    }
                }
                else
                {
                    int lineLength = (startingConfig.First().Length + 1);
                    int counter = 0;
                    for (int j = 0; j < lineLength; j += 4)
                    {
                        if (startingConfig[i][j + 1] != ' ')
                        {
                            stacks[counter].P1Crates.Push(startingConfig[i][j + 1].ToString());
                            stacks[counter].P2Crates.Push(startingConfig[i][j + 1].ToString());
                        }
                        counter++;
                    }
                }
            }

            return stacks;
        }
    }
}

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2020
{
    class Day08
    {
        public static List<string> Inputs { get; set; }

        public static void Run()
        {
            Inputs = File.ReadAllLines("day08Input.txt").ToList();
            Tuple<int, int> result = RunProgram(Inputs);
            Console.WriteLine($"Initial Accumulator Value: {result.Item2}");
            for(int i = 0; i < Inputs.Count(); i++)
            {
                string line = Inputs[i];
                if(line.Split(' ')[0].Equals("jmp"))
                {
                    List<string> modifiedCode = new List<string>(Inputs);
                    modifiedCode[i] = modifiedCode[i].Replace("jmp", "nop");
                    result = RunProgram(modifiedCode);
                    if(result.Item1 == -1)
                    {
                        continue;
                    } else
                    {
                        break;
                    }
                } else if (line.Split(' ')[0].Equals("nop"))
                {
                    List<string> modifiedCode = new List<string>(Inputs);
                    modifiedCode[i] = modifiedCode[i].Replace("nop", "jmp");
                    result = RunProgram(modifiedCode);
                    if (result.Item1 == -1)
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            Console.WriteLine($"Fixed Accumulator Value: {result.Item2}");
        }

        public static Tuple<int,int> RunProgram(List<string> code)
        {
            Dictionary<int, string> performedInstructions = new Dictionary<int, string>();
            int acc = 0;
            int pos = 0;
            while (true)
            {
                if (pos >= code.Count())
                {
                    return Tuple.Create(1, acc);
                }
                else if (performedInstructions.Contains(new KeyValuePair<int, string>(pos, code[pos])))
                {
                    return Tuple.Create(-1, acc);
                }
                performedInstructions.Add(pos, code[pos]);
                string[] currentInstruction = code[pos].Split(' ');
                switch (currentInstruction[0])
                {
                    case "nop":
                        pos++;
                        break;
                    case "acc":
                        acc += int.Parse(currentInstruction[1]);
                        pos++;
                        break;
                    case "jmp":
                        pos += int.Parse(currentInstruction[1]);
                        break;
                }
            }
        }
    }
}

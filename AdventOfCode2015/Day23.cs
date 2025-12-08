using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
    enum InstructionDef
    {
        hlf,
        tpl,
        inc,
        jmp,
        jie,
        jio
    }
    class Day23
    {
        private static int a = 0;
        private static int b = 0;
        private static int pointer = 0;

        public static void Run()
        {
            List<string> input = File.ReadAllLines("day23input.txt").ToList();
            var program = LoadProgram(input);
            RunProgram(program);
            Console.WriteLine("Part 1 - " + b);

            ResetProgram();
            a = 1;
            program = LoadProgram(input);
            RunProgram(program);
            Console.WriteLine("Part 2 - " + b);
        }

        private static void ResetProgram()
        {
            a = 0;
            b = 0;
            pointer = 0;
        }

        private static void RunProgram(List<Instruction> program)
        {
            do
            {
                var instruction = program[pointer];
                switch (instruction.InstructionDef)
                {
                    case InstructionDef.hlf:
                        SetRegister[instruction.Target](GetRegister[instruction.Target]() / 2);
                        pointer++;
                        break;
                    case InstructionDef.tpl:
                        SetRegister[instruction.Target](GetRegister[instruction.Target]() * 3);
                        pointer++;
                        break;
                    case InstructionDef.inc:
                        SetRegister[instruction.Target](GetRegister[instruction.Target]() + 1);
                        pointer++;
                        break;
                    case InstructionDef.jmp:
                        pointer += instruction.Offset;
                        break;
                    case InstructionDef.jie:
                        if (GetRegister[instruction.Target]() % 2 == 0)
                            pointer += instruction.Offset;
                        else
                            pointer++;
                        break;
                    case InstructionDef.jio:
                        if (GetRegister[instruction.Target]() == 1)
                            pointer += instruction.Offset;
                        else
                            pointer++;
                        break;
                }
            }
            while (pointer >= 0 && pointer < program.Count);
        }

        private static List<Instruction> LoadProgram(List<string> input)
        {
            List<Instruction> result = new List<Instruction>();
            foreach (string line in input)
            {
                var splitLine = line.Split(' ');
                switch (splitLine[0])
                {
                    case "hlf":
                        result.Add(new Instruction { InstructionDef = InstructionDef.hlf, Target = splitLine[1] });
                        break;
                    case "tpl":
                        result.Add(new Instruction { InstructionDef = InstructionDef.tpl, Target = splitLine[1] });
                        break;
                    case "inc":
                        result.Add(new Instruction { InstructionDef = InstructionDef.inc, Target = splitLine[1] });
                        break;
                    case "jmp":
                        if (splitLine[1][0] == '+')
                        {
                            result.Add(new Instruction { InstructionDef = InstructionDef.jmp, Offset = int.Parse(splitLine[1].Trim('+')) });
                        }
                        else if (splitLine[1][0] == '-')
                        {
                            result.Add(new Instruction { InstructionDef = InstructionDef.jmp, Offset = int.Parse(splitLine[1].Trim('-')) * -1 });
                        }
                        else
                        {
                            throw new ArgumentException("Unknown operator");
                        }
                            break;
                    case "jie":
                        result.Add(new Instruction { InstructionDef = InstructionDef.jie, Target = splitLine[1].Trim(','), Offset = int.Parse(splitLine[2]) });
                        break;
                    case "jio":
                        result.Add(new Instruction { InstructionDef = InstructionDef.jio, Target = splitLine[1].Trim(','), Offset = int.Parse(splitLine[2]) });
                        break;
                }
            }
            return result;
        }

        // Dictionary to map string names to variable references
        private static Dictionary<string, Func<int>> GetRegister = new Dictionary<string, Func<int>>
        {
            { "a", () => a },
            { "b", () => b }
        };

        private static Dictionary<string, Action<int>> SetRegister = new Dictionary<string, Action<int>>
        {
            { "a", value => a = value },
            { "b", value => b = value }
        };

        class Instruction
        {
            public InstructionDef InstructionDef {  get; set; }
            public string Target { get; set; }
            public int Offset { get; set; }

        }
    }
}

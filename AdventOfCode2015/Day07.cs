using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2015
{
    enum Instruction
    {
        Assign,
        And,
        LShift,
        RShift,
        Or,
        Not,

    }

    class Wire
    {
        public string Name { get; set; }
        public ushort Value { get; set; }
    }

    class Day07
    {
        public static void Run()
        {
            List<Wire> wires = new List<Wire>();
            Queue<string> instructions = new Queue<string>(File.ReadAllLines("Day07Input.txt"));
            Queue<string> p2Instructions = new Queue<string>(instructions);
            while (instructions.Count > 0)
            {
                string instruction = instructions.Dequeue();
                if (!ProcessInstruction(wires, instruction))
                {
                    instructions.Enqueue(instruction);
                }
            }

            var wireA = wires.SingleOrDefault(x => x.Name == "a")?.Value;
            Console.WriteLine("Part 1 - " + wireA);

            wires.Clear();
            wires.Add(new Wire { Name = "b", Value = wireA.Value });
            while (p2Instructions.Count > 0)
            {
                string instruction = p2Instructions.Dequeue();
                //Prevent the P2 requirement being overridden
                if (instruction == "19138 -> b") continue;
                if (!ProcessInstruction(wires, instruction))
                {
                    p2Instructions.Enqueue(instruction);
                }
            }
            wireA = wires.SingleOrDefault(x => x.Name == "a")?.Value;
            Console.WriteLine("Part 2 - " + wireA);
        }

        private static bool ProcessInstruction(List<Wire> wires, string instruction)
        {
            var split = instruction.Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries);
            bool success = false;
            var items = GetInstruction(split[0]);
            string destination = split[1].Trim();
            switch (items.instruction)
            {
                case Instruction.And:
                case Instruction.Or:
                case Instruction.LShift:
                case Instruction.RShift:
                    success = ProcessTwoOperands(wires, items.instruction, items.operand1, items.operand2, destination);
                    break;
                case Instruction.Assign:
                    success = ProcessAssign(wires, items.operand1, destination);
                    break;
                case Instruction.Not:
                    success = ProcessNot(wires, items.operand1, destination);
                    break;
            }
            return success;
        }

        private static bool ProcessAssign(List<Wire> wires, string input, string destination)
        {
            Wire destinationWire = GetWire(wires, destination);
            Wire inputWire = GetWire(wires, input);

            ushort val;
            if (inputWire is null)
            {
                if (ushort.TryParse(input, out ushort res))
                {
                    val = res;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                val = inputWire.Value;
            }
            

            if (destinationWire is null) {
                destinationWire = new Wire();
                destinationWire.Name = destination;
                wires.Add(destinationWire);
            }

            destinationWire.Value = val;

            return true;
        }

        private static bool ProcessNot(List<Wire> wires, string operand1, string destination)
        {
            ushort val1;
            
            Wire wire1 = GetWire(wires, operand1);
            Wire wire2 = GetWire(wires, destination);

            if(wire1 is null)
            {
                if(ushort.TryParse(operand1, out ushort res))
                {
                    val1 = res;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                val1 = wire1.Value;
            }

            if (wire2 is null)
            {
                wire2 = new Wire();
                wire2.Name = destination;
                wires.Add(wire2);
            }
            wire2.Value = (ushort)~val1;
            return true;
        }

        private static bool ProcessTwoOperands(List<Wire> wires, Instruction instruction, string operand1, string operand2, string destination)
        {
            Wire wire1 = GetWire(wires, operand1);
            Wire wire2 = GetWire(wires, operand2);
            ushort result = 0;

            //One of the wires doesn't yet have a value
            if (instruction == Instruction.Or || instruction == Instruction.And)
            {
                ushort val1;
                ushort val2;
                if (wire1 is null)
                {
                    if (!ushort.TryParse(operand1, out ushort res1))
                    {
                        return false;
                    }
                    else
                    {
                        val1 = res1;
                    }
                }
                else
                {
                    val1 = wire1.Value;
                }

                if (wire2 is null)
                {
                    if (!ushort.TryParse(operand2, out ushort res2))
                    {
                        return false;
                    }
                    else
                    {
                        val2 = res2;
                    }
                }
                else
                {
                    val2 = wire2.Value;
                }

                if (instruction == Instruction.Or)
                {
                    result = (ushort)(val1 | val2);
                } 
                else if (instruction == Instruction.And)
                {
                    result = (ushort)(val1 & val2);
                }
            }
            else
            {
                if (wire1 is null) return false;
                if (instruction == Instruction.LShift)
                {
                    result = (ushort)(wire1.Value << ushort.Parse(operand2));
                }
                else if (instruction == Instruction.RShift)
                {
                    result = (ushort)(wire1.Value >> ushort.Parse(operand2));
                }
                else
                {
                    throw new ArgumentException();
                }
            }

            Wire wire3 = GetWire(wires, destination);
            
            if (wire3 is null)
            {
                wire3 = new Wire();
                wire3.Name = destination;
                wires.Add(wire3);
            }
            wire3.Value = result;
            return true;
        }

        private static Wire GetWire(List<Wire> wires, string name)
        {
            return wires.SingleOrDefault(x => x.Name == name);
        }

        private static (Instruction instruction, string operand1, string operand2) GetInstruction(string instruction)
        {
            if (instruction.Contains("AND"))
            {
                var split = instruction.Split(new string[] { " AND " }, StringSplitOptions.RemoveEmptyEntries);
                return (Instruction.And, split[0].Trim(), split[1].Trim());
            }
            else if (instruction.Contains("NOT"))
            {
                return (Instruction.Not, instruction.Remove(0, 3).Trim(), null);
            }
            else if (instruction.Contains("LSHIFT"))
            {
                var split = instruction.Split(new string[] { " LSHIFT " }, StringSplitOptions.RemoveEmptyEntries);
                return (Instruction.LShift, split[0].Trim(), split[1].Trim());
            }
            else if (instruction.Contains("RSHIFT"))
            {
                var split = instruction.Split(new string[] { " RSHIFT " }, StringSplitOptions.RemoveEmptyEntries);
                return (Instruction.RShift, split[0].Trim(), split[1].Trim());
            }
            else if (instruction.Contains("OR"))
            {
                var split = instruction.Split(new string[] { " OR " }, StringSplitOptions.RemoveEmptyEntries);
                return (Instruction.Or, split[0].Trim(), split[1].Trim());
            }
            else
            {
                return (Instruction.Assign, instruction.Trim(), null);
            }
        }
    }
}

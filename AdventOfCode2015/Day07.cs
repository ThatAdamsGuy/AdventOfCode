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
            while (instructions.Count > 0)
            {
                string instruction = instructions.Dequeue();
                if (!ProcessInstruction(wires, instruction))
                {
                    instructions.Enqueue(instruction);
                }
            }
            Console.WriteLine("Part 1: " + wires.Where(x => x.Name == "a").Single().Value);
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
            if (destinationWire is null) {
                destinationWire = new Wire();
                destinationWire.Name = destination;
                wires.Add(destinationWire);
            }

            if(ushort.TryParse(input, out ushort result))
            {
                destinationWire.Value = result;
            }
            else
            {
                Wire sourceWire = GetWire(wires, input);
                if (sourceWire is null) return false;
                destinationWire.Value = sourceWire.Value;
            }

            return true;
        }

        private static bool ProcessNot(List<Wire> wires, string operand1, string destination)
        {
            Wire wire1 = GetWire(wires, operand1);
            Wire wire2 = GetWire(wires, destination);

            if (wire1 is null) return false;
            else
            {
                if (wire2 is null)
                {
                    wire2 = new Wire();
                    wire2.Name = destination;
                    wires.Add(wire2);
                }
                wire2.Value = (ushort)~wire1.Value;
                return true;
            }
        }

        private static bool ProcessTwoOperands(List<Wire> wires, Instruction instruction, string operand1, string operand2, string destination)
        {
            Wire wire1 = GetWire(wires, operand1);
            Wire wire2 = GetWire(wires, operand2);

            //One of the wires doesn't yet have a value
            if (wire1 is null)  return false;
            if ((instruction == Instruction.Or || instruction == Instruction.And) && wire2 is null) return false;
            else
            {
                Wire wire3 = GetWire(wires, destination);
                ushort result = 0;

                switch (instruction)
                {
                    case Instruction.And:
                        result = (ushort)(wire1.Value & wire2.Value);
                        break;
                    case Instruction.Or:
                        result = (ushort)(wire1.Value | wire2.Value);
                        break;
                    case Instruction.LShift:
                        result = (ushort)(wire1.Value << ushort.Parse(operand2));
                        break;
                    case Instruction.RShift:
                        result |= (ushort)(wire1.Value >> ushort.Parse(operand2));
                        break;
                    default:
                        throw new ArgumentException();
                }

                if (wire3 is null)
                {
                    wire3 = new Wire();
                    wire3.Name = destination;
                    wires.Add(wire3);
                }
                wire3.Value = result;
                return true;
            }
        }

        private static Wire GetWire(List<Wire> wires, string name)
        {
            return wires.Where(x => x.Name == name).SingleOrDefault();
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

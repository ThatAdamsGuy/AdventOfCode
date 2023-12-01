using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AdventOfCode2019.Util;

namespace AdventOfCode2019
{
    internal class Intcode
    {
        public int[] Program { get; set; }

        public Intcode(string input)
        {
            SetProgram(input);
        }

        public void SetProgram(string input)
        {
            Program = input.Split(',').Select(x => int.Parse(x)).ToArray();
        }

        public void SetPosition(int position, int value)
        {
            Program[position] = value;
        }

        public int GetValueAtPosition(int position)
        {
            return Program[position];
        }

        public void Run()
        {
            if(Program == null || Program.Length == 0)
            {
                throw new Exception("Program not set!");
            }

            int position = 0;
            bool complete = false;
            bool jump = false;
            OpCode startingCode;

            while (!complete)
            {
                Command cmd = Command.ParseIntcodeInstruction(Program[position], position);
                startingCode = cmd.Instruction;
                switch (cmd.Instruction)
                {
                    case OpCode.Add:
                        SetParam(cmd, position, 1);
                        SetParam(cmd, position, 2);
                        SetParam(cmd, position, 3);
                        Add(cmd);
                        break;
                    case OpCode.Multiply:
                        SetParam(cmd, position, 1);
                        SetParam(cmd, position, 2);
                        SetParam(cmd, position, 3);
                        Multiply(cmd);
                        break;
                    case OpCode.Input:
                        SetParam(cmd, position, 1);
                        Input(cmd);
                        break;
                    case OpCode.Output:
                        SetParam(cmd, position, 1);
                        Output(cmd);
                        break;
                    case OpCode.Jump_If_True:
                        SetParam(cmd, position, 1);
                        SetParam(cmd, position, 2);
                        int result = JumpIfTrue(cmd);
                        if(result != -1)
                        {
                            position = result;
                            jump = true;
                        }
                        break;
                    case OpCode.Jump_If_False:
                        SetParam(cmd, position, 1);
                        SetParam(cmd, position, 2);
                        result = JumpIfFalse(cmd);
                        if (result != -1)
                        {
                            position = result;
                            jump = true;
                        }
                        break;
                    case OpCode.Less_Than:
                        SetParam(cmd, position, 1);
                        SetParam(cmd, position, 2);
                        SetParam(cmd, position, 3);
                        LessThan(cmd);
                        break;
                    case OpCode.Greater_Than:
                        SetParam(cmd, position, 1);
                        SetParam(cmd, position, 2);
                        SetParam(cmd, position, 3);
                        GreaterThan(cmd);
                        break;
                    case OpCode.End:
                        complete = true;
                        break;
                    default:
                        throw new Exception("Invalid OpCode");
                }
                if (!jump)
                {
                    position += UpdatePosition(startingCode, position);
                }
            }
        }

        private void Add(Command cmd)
        {
            Program[cmd.ParamThree] = GetParam(cmd, 1) + GetParam(cmd, 2);
        }

        private void Multiply(Command cmd)
        {
            Program[cmd.ParamThree] = GetParam(cmd, 1) * GetParam(cmd, 2);
        }

        private int JumpIfTrue(Command cmd)
        {
            if(GetParam(cmd, 1) != 0)
            {
                return GetParam(cmd, 2);
            }
            else
            {
                return -1;
            }
        }

        private int JumpIfFalse(Command cmd)
        {
            if (GetParam(cmd, 1) == 0)
            {
                return GetParam(cmd, 2);
            }
            else
            {
                return -1;
            }
        }

        private void LessThan(Command cmd)
        {
            Program[cmd.ParamThree] = GetParam(cmd, 1) < GetParam(cmd, 2) ? 1 : 0;
        }

        private void GreaterThan(Command cmd)
        {
            Program[cmd.ParamThree] = GetParam(cmd, 1) > GetParam(cmd, 2) ? 1 : 0;
        }

        private void Input(Command cmd)
        {
            Console.Write("Input: ");
            if (!int.TryParse(Console.ReadLine(), out int result))
            {
                Console.WriteLine("Invalid Input.");
                throw new Exception();
            }
            else
            {
                Program[cmd.ParamOne] = result;
            }
        }

        private void Output(Command cmd)
        {
            Console.WriteLine("Output: " + Program[cmd.ParamOne]);
        }

        public void SetParam(Command cmd, int position, int param)
        {
            switch (param)
            {
                case 1:
                    cmd.ParamOne = Program[position + 1];
                    break;
                case 2:
                    cmd.ParamTwo = Program[position + 2];
                    break;
                case 3:
                    cmd.ParamThree = Program[position + 3];
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        public int GetParam(Command cmd, int param)
        {
            switch (param)
            {
                case 1:
                    switch (cmd.ParamOneMode)
                    {
                        case ParameterMode.Position:
                            return Program[cmd.ParamOne];
                        case ParameterMode.Value:
                            return cmd.ParamOne;
                        default:
                            throw new ArgumentException();
                    }
                case 2:
                    switch (cmd.ParamTwoMode)
                    {
                        case ParameterMode.Position:
                            return Program[cmd.ParamTwo];
                        case ParameterMode.Value:
                            return cmd.ParamTwo;
                        default:
                            throw new ArgumentException();
                    }
                case 3:
                    switch (cmd.ParamThreeMode)
                    {
                        case ParameterMode.Position:
                            return Program[cmd.ParamThree];
                        case ParameterMode.Value:
                            return cmd.ParamThree;
                        default:
                            throw new ArgumentException();
                    }
                default:
                    throw new ArgumentException();
            }
        }

        private int UpdatePosition(OpCode starting, int position)
        {
            switch (starting)
            {
                case OpCode.Add:
                case OpCode.Multiply:
                case OpCode.Less_Than:
                case OpCode.Greater_Than:
                    return 4;
                case OpCode.Jump_If_True:
                case OpCode.Jump_If_False:
                    return 3;
                case OpCode.Input:
                case OpCode.Output:
                    return 2;
                case OpCode.End:
                    return 0;
                default:
                    throw new ArgumentException();
            }
        }
    }

    public class Command
    {
        public OpCode Instruction { get; set; }
        public int InstructionPosition { get; set; }
        public ParameterMode? ParamOneMode { get; set; }
        public ParameterMode? ParamTwoMode { get; set; }
        public ParameterMode? ParamThreeMode { get; set; }
        public int ParamOne { get; set; }
        public int ParamTwo { get; set; }
        public int ParamThree { get; set; }

        public Command(OpCode instruction, int instructionPosition)
        {
            Instruction = instruction;
            InstructionPosition = instructionPosition;
            if (ParamOneMode is null) ParamOneMode = ParameterMode.Position;
            if (ParamTwoMode is null) ParamTwoMode = ParameterMode.Position;
            if (ParamThreeMode is null) ParamThreeMode = ParameterMode.Position;
        }

        public Command (OpCode instruction, int instructionPosition, ParameterMode paramOneMode) 
            : this(instruction, instructionPosition)
        {
            ParamOneMode = paramOneMode;
        }

        public Command(OpCode instruction, int instructionPosition, ParameterMode paramOneMode, ParameterMode paramTwoMode)
            : this(instruction, instructionPosition, paramOneMode)
        {
            ParamTwoMode = paramTwoMode;
        }

        public Command(OpCode instruction, int instructionPosition, ParameterMode paramOneMode, ParameterMode paramTwoMode, ParameterMode paramThreeMode)
            : this(instruction, instructionPosition, paramOneMode, paramTwoMode)
        {
            ParamThreeMode = paramThreeMode;
        }

        public static Command ParseIntcodeInstruction(int instruction, int instructionPosition)
        {
            return ParseParameterModes(instruction.ToString(), instructionPosition);
        }

        public static Command ParseIntcodeInstruction(string instruction, int instructionPosition)
        {
            return Enum.TryParse(instruction, out OpCode opcode) ? new Command(opcode, instructionPosition) : ParseParameterModes(instruction, instructionPosition);
        }

        public static Command ParseParameterModes(string instruction, int instructionPosition)
        {
            if (int.Parse(instruction) >= 100)
            {
                OpCode code = (OpCode)Enum.Parse(typeof(OpCode), instruction.Substring(instruction.Length - 2, 2));
                Command cmd = new Command(code, instructionPosition);
                if (instruction.Length == 2) return cmd;

                cmd.ParamOneMode = (ParameterMode)int.Parse((instruction[instruction.Length - 3]).ToString());
                if (instruction.Length == 3) return cmd;

                cmd.ParamTwoMode = (ParameterMode)int.Parse((instruction[instruction.Length - 4]).ToString());
                if (instruction.Length == 4) return cmd;

                cmd.ParamThreeMode = (ParameterMode)int.Parse((instruction[instruction.Length - 5]).ToString());
                return cmd;
            }
            else
            {
                return new Command((OpCode)int.Parse(instruction), instructionPosition);
            }
        }
    }

    public static class Util
    {
        public enum ParameterMode
        {
            Position = 0,
            Value = 1
        }

        public enum OpCode
        {
            Add = 1,
            Multiply = 2,
            Input = 3,
            Output = 4,
            Jump_If_True = 5,
            Jump_If_False = 6,
            Less_Than = 7,
            Greater_Than = 8,
            End = 99
        }
    }
}

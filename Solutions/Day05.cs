using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Solutions
{
    public class Day05
    {
        public static int Puzzle1()
        {
            var program = FromString(GetInputFromFile());
            return EvaluateProgram(program, diagnosticInput: 1);
        }

        public static int Puzzle2()
        {
            var program = FromString(GetInputFromFile());
            return EvaluateProgram(program, diagnosticInput: 5);
        }

        private static int EvaluateProgram(int[] program, int diagnosticInput)
        {
            var lastOutput = -1;
            for (var ip = 0; ip < program.Length;)
            {
                var opcode = (Opcode)(program[ip] % 100);

                if (opcode == Opcode.Halt)
                    return lastOutput;

                if (opcode == Opcode.Input)
                {
                    var writeTo = program[ip + 1];
                    program[writeTo] = diagnosticInput;
                    ip += 2;
                    continue;
                }

                var (mode1, mode2) = GetParameterModes(program[ip]);
             
                var leftIndex = mode1 == 0 ? program[ip + 1] : ip + 1;

                if (opcode == Opcode.Output)
                {
                    lastOutput = program[leftIndex];
                    ip += 2;
                    continue;
                }

                var rightIndex = mode2 == 0 ? program[ip + 2] : ip + 2;

                switch (opcode)
                {
                    case Opcode.Add:
                        program[program[ip + 3]] = program[leftIndex] + program[rightIndex];
                        break;
                    case Opcode.Multiply:
                        program[program[ip + 3]] = program[leftIndex] * program[rightIndex];
                        break;
                    case Opcode.JumpIfTrue:
                        ip = program[leftIndex] != 0 ? program[rightIndex] : ip + 3;
                        continue;
                    case Opcode.JumpIfFalse:
                        ip = program[leftIndex] == 0 ? program[rightIndex] : ip + 3;
                        continue;
                    case Opcode.LessThan:
                        program[program[ip + 3]] = program[leftIndex] < program[rightIndex] ? 1 : 0;
                        break;
                    case Opcode.Equals:
                        program[program[ip + 3]] = program[leftIndex] == program[rightIndex] ? 1 : 0;
                        break;
                }

                ip += 4;
            }
            return program[0];
        }

        private static (int, int) GetParameterModes(int instruction)
        {
            // There has to be a cleaner and easier way with math...
            var opcodeInfo = instruction.ToString();
            if (opcodeInfo.Length > 2)
            {
                var modeInfo = opcodeInfo.Substring(0, opcodeInfo.Length - 2).ToCharArray().Reverse().ToArray();
                var mode1 = modeInfo.Length > 0 ? modeInfo[0] - 48 : 0;
                var mode2 = modeInfo.Length > 1 ? modeInfo[1] - 48 : 0;
                return (mode1, mode2);
            }

            return (0, 0);
        }

        private enum Opcode
        {
            Add = 1,
            Multiply = 2,
            Input = 3,
            Output = 4,
            JumpIfTrue = 5,
            JumpIfFalse = 6,
            LessThan = 7,
            Equals = 8,
            Halt = 99
        }

        private static string GetInputFromFile() => File.ReadAllText("Input/Day05.txt");
        private static int[] FromString(string input) => input.Split(',').Select(int.Parse).ToArray();
    }
}

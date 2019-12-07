using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions
{
    public class Day07
    {
        public static int Puzzle1()
        {
            var input = GetInputFromFile();
            var permutations = GetPermutations(Enumerable.Range(0, 5), 5);
            var results = new List<int>();

            foreach (var permutation in permutations)
            {
                var inputSignal = 0;

                foreach (var phaseSetting in permutation)
                {
                    var program = FromString(input);
                    var intCode = new IntcodeComputer(phaseSetting, inputSignal);
                    intCode.OnOutput += output => inputSignal = output;
                    intCode.EvaluateProgram(program).Wait();
                }

                results.Add(inputSignal);
            }

            return results.Max();
        }

        public static int Puzzle2()
        {
            var input = GetInputFromFile();
            var permutations = GetPermutations(Enumerable.Range(5, 5), 5);
            var results = new List<int>();

            foreach (var permutation in permutations)
            {
                var lastOutput = -1;
                var ampA = new IntcodeComputer(permutation.ElementAt(0), 0);
                var ampB = new IntcodeComputer(permutation.ElementAt(1));
                var ampC = new IntcodeComputer(permutation.ElementAt(2));
                var ampD = new IntcodeComputer(permutation.ElementAt(3));
                var ampE = new IntcodeComputer(permutation.ElementAt(4));

                ampA.OnOutput += outputA => ampB.InputStream.Enqueue(outputA);
                ampB.OnOutput += outputB => ampC.InputStream.Enqueue(outputB);
                ampC.OnOutput += outputC => ampD.InputStream.Enqueue(outputC);
                ampD.OnOutput += outputD => ampE.InputStream.Enqueue(outputD);
                ampE.OnOutput += outputE =>
                {
                    ampA.InputStream.Enqueue(outputE);
                    lastOutput = outputE;
                };

                Task.WaitAll(
                    ampA.EvaluateProgram(FromString(input)),
                    ampB.EvaluateProgram(FromString(input)),
                    ampC.EvaluateProgram(FromString(input)),
                    ampD.EvaluateProgram(FromString(input)),
                    ampE.EvaluateProgram(FromString(input))
                );

                results.Add(lastOutput);
            }

            return results.Max();
        }

        // https://stackoverflow.com/a/10630026
        private static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1)
                .SelectMany(
                    t => list.Where(e => !t.Contains(e)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }

        private static string GetInputFromFile() => File.ReadAllText("Input/Day07.txt");
        private static int[] FromString(string input) => input.Split(',').Select(int.Parse).ToArray();
    }

    public class IntcodeComputer
    {
        public IntcodeComputer(params int[] inputs)
        {
            foreach (var input in inputs)
            {
                this.InputStream.Enqueue(input);
            }
        }

        public ConcurrentQueue<int> InputStream { get; } = new ConcurrentQueue<int>();

        public event Action<int> OnOutput;

        public async Task EvaluateProgram(int[] program)
        {
            for (var ip = 0; ip < program.Length;)
            {
                var opcode = (Opcode)(program[ip] % 100);

                if (opcode == Opcode.Halt)
                    return;

                if (opcode == Opcode.Input)
                {
                    var writeTo = program[ip + 1];
                    while (!this.InputStream.Any())
                    {
                        await Task.Delay(1);
                    }

                    if (!this.InputStream.TryDequeue(out var input))
                    {
                        throw new Exception("Threading...");
                    }

                    program[writeTo] = input;
                    ip += 2;
                    continue;
                }

                var (mode1, mode2) = GetParameterModes(program[ip]);

                var leftIndex = mode1 == 0 ? program[ip + 1] : ip + 1;

                if (opcode == Opcode.Output)
                {
                    this.OnOutput?.Invoke(program[leftIndex]);
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
    }
}

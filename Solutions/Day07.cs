using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AdventOfCode.Solutions.Shared;

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
                    var computer = IntcodeComputer.LoadProgramFromString(input, phaseSetting, inputSignal);
                    computer.OnOutput += output => inputSignal = (int)output;
                    computer.EvaluateProgram().Wait();
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
                var ampA = IntcodeComputer.LoadProgramFromString(input, permutation.ElementAt(0), 0);
                var ampB = IntcodeComputer.LoadProgramFromString(input, permutation.ElementAt(1));
                var ampC = IntcodeComputer.LoadProgramFromString(input, permutation.ElementAt(2));
                var ampD = IntcodeComputer.LoadProgramFromString(input, permutation.ElementAt(3));
                var ampE = IntcodeComputer.LoadProgramFromString(input, permutation.ElementAt(4));

                ampA.OnOutput += outputA => ampB.InputStream.Enqueue(outputA);
                ampB.OnOutput += outputB => ampC.InputStream.Enqueue(outputB);
                ampC.OnOutput += outputC => ampD.InputStream.Enqueue(outputC);
                ampD.OnOutput += outputD => ampE.InputStream.Enqueue(outputD);
                ampE.OnOutput += outputE =>
                {
                    ampA.InputStream.Enqueue(outputE);
                    lastOutput = (int)outputE;
                };

                Task.WaitAll(
                    ampA.EvaluateProgram(),
                    ampB.EvaluateProgram(),
                    ampC.EvaluateProgram(),
                    ampD.EvaluateProgram(),
                    ampE.EvaluateProgram()
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
    }
}

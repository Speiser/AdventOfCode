using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdventOfCode2019.Solutions.Shared;

namespace AdventOfCode2019.Solutions
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
                    var inQueue = new Queue<long>();
                    inQueue.Enqueue(phaseSetting);
                    inQueue.Enqueue(inputSignal);
                    var computer = IntcodeComputer.LoadProgramFromString(input);
                    computer.EvaluateProgram(() => inQueue.Dequeue(), output => inputSignal = (int)output);
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

                var inA = new Queue<long>();
                var inB = new Queue<long>();
                var inC = new Queue<long>();
                var inD = new Queue<long>();
                var inE = new Queue<long>();

                inA.Enqueue(permutation.ElementAt(0));
                inA.Enqueue(0);
                inB.Enqueue(permutation.ElementAt(1));
                inC.Enqueue(permutation.ElementAt(2));
                inD.Enqueue(permutation.ElementAt(3));
                inE.Enqueue(permutation.ElementAt(4));

                var ampA = IntcodeComputer.LoadProgramFromString(input);
                var ampB = IntcodeComputer.LoadProgramFromString(input);
                var ampC = IntcodeComputer.LoadProgramFromString(input);
                var ampD = IntcodeComputer.LoadProgramFromString(input);
                var ampE = IntcodeComputer.LoadProgramFromString(input);

                Task.WaitAll(
                    Task.Run(() => ampA.EvaluateProgram(() => WaitForDequeue(inA), output => inB.Enqueue(output))),
                    Task.Run(() => ampB.EvaluateProgram(() => WaitForDequeue(inB), output => inC.Enqueue(output))),
                    Task.Run(() => ampC.EvaluateProgram(() => WaitForDequeue(inC), output => inD.Enqueue(output))),
                    Task.Run(() => ampD.EvaluateProgram(() => WaitForDequeue(inD), output => inE.Enqueue(output))),
                    Task.Run(() => ampE.EvaluateProgram(
                        () => WaitForDequeue(inE),
                        output =>
                        {
                            inA.Enqueue(output);
                            lastOutput = (int)output;
                        }))
                );

                results.Add(lastOutput);
            }

            return results.Max();
        }

        private static T WaitForDequeue<T>(Queue<T> queue)
        {
            while (queue.Count == 0)
                Task.Delay(1).Wait();
            return queue.Dequeue();
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

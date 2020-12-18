using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.Solutions.Shared;

namespace AdventOfCode2020.Solutions
{
    public class Day09 : ISolution
    {
        public void Solve()
        {
            var input = Input.Lines(nameof(Day09)).Select(x => long.Parse(x)).ToArray();
            var p1 = this.Puzzle1(input);
            Console.WriteLine(p1);
            Console.WriteLine(this.Puzzle2(input, p1));
        }

        private long Puzzle1(long[] input)
        {
            for (var i = 25; i < input.Length; i++)
            {
                var combinations = GetLastCombinations(i - 25, input);
                if (!combinations.Contains(input[i]))
                    return input[i];
            }

            throw new InvalidProgramException();
        }

        private long Puzzle2(long[] input, long invalid)
        {
            for (var i = 0; i < input.Length; i++)
            {
                var current = new List<long> { input[i] };
                var sum = input[i];
                for (var j = i + 1; j < input.Length; j++)
                {
                    sum += input[j];
                    current.Add(input[j]);
                    if (sum == invalid)
                    {
                        return current.Min() + current.Max();
                    }
                }
            }

            throw new InvalidProgramException();
        }

        private static List<long> GetLastCombinations(int start, long[] input)
        {
            var combinations = new List<long>();

            for (var x = start; x < start + 25; x++)
            {
                for (var y = x + 1; y < start + 25; y++)
                {
                    combinations.Add(input[x] + input[y]);
                }
            }

            return combinations;
        }
    }
}

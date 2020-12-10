using System;
using System.Linq;
using AdventOfCode2015.Solutions.Shared;
using NUnit.Framework;

namespace AdventOfCode2015.Solutions
{
    public class Day02 : ISolution
    {
        public void Solve()
        {
            var input = Input.Lines(nameof(Day02));
            Console.WriteLine(this.Puzzle1(input));
            Console.WriteLine(this.Puzzle2(input));
        }

        private int Puzzle1(string[] input)
        {
            var dimensions = Parse(input);
            var squarefeet = 0;
            foreach (var (l, w, h) in dimensions)
            {
                var calc = new int[]
                {
                    l * w,
                    w * h,
                    h * l
                };
                var min = calc.Min();
                squarefeet += calc.Sum(x => x * 2) + min;
            }
            return squarefeet;
        }

        private int Puzzle2(string[] input)
        {
            var dimensions = Parse(input);
            var length = 0;
            foreach (var (l, w, h) in dimensions)
            {
                var calc = new int[]
                {
                    l * 2 + w * 2,
                    w * 2 + h * 2,
                    h * 2 + l * 2
                };
                length += calc.Min() + l * w * h;
            }
            return length;
        }

        private static Dimension[] Parse(string[] input)
        {
            var result = new Dimension[input.Length];
            for (var i = 0; i < result.Length; i++)
            {
                var split = input[i].Split("x");
                result[i] = new Dimension(int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2]));
            }
            return result;
        }

        private record Dimension(int L, int W, int H);
    }
}

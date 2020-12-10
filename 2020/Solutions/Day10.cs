using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.Solutions.Shared;
using NUnit.Framework;

namespace AdventOfCode2020.Solutions
{
    public class Day10 : ISolution
    {
        public void Solve()
        {
            var input = Input.LinesAsInt(nameof(Day10));
            Console.WriteLine(this.Puzzle1(input));
            Console.WriteLine(this.Puzzle2(input));
        }

        private int Puzzle1(IEnumerable<int> input)
        {
            var ordered = GetSorted(input);
            var skip1 = 0;
            var skip3 = 0;

            for (var i = 1; i < ordered.Count; i++)
            {
                var diff = ordered[i] - ordered[i - 1];
                if (diff == 1) skip1++;
                else if (diff == 3) skip3++;
            }

            return skip1 * skip3;
        }

        private long Puzzle2(IEnumerable<int> input)
        {
            var ordered = GetSorted(input);

            var ways = new long[ordered.Count];
            ways[0] = 1;

            for (var i = 1; i < ways.Length; i++)
            {
                long differentWays = 0;

                for (var j = 0; j < i; j++)
                {
                    if (ordered[i] - ordered[j] > 3)
                        continue;

                    differentWays += ways[j];
                }

                ways[i] = differentWays;
            }

            return ways.Last();
        }

        private static List<int> GetSorted(IEnumerable<int> input)
        {
            var ordered = input.OrderBy(x => x).ToList();
            ordered.Insert(0, 0);
            ordered.Add(ordered.Max() + 3);
            return ordered;
        }

        private class Tests
        {
            [Test]
            public void Puzzle1()
            {
                var actual = new Day10().Puzzle1(TestInput);
                Assert.AreEqual(220, actual);
            }

            [Test]
            public void Puzzle2()
            {
                var actual = new Day10().Puzzle2(TestInput);
                Assert.AreEqual(19208, actual);
            }

            private readonly IEnumerable<int> TestInput
                = "28 33 18 42 31 14 46 20 48 47 24 23 49 45 19 38 39 11 1 32 25 35 8 17 7 9 4 2 34 10 3"
                .Split(" ").Select(x => int.Parse(x));
        }
    }
}

using System;
using System.Linq;
using AdventOfCode2020.Solutions.Shared;
using NUnit.Framework;

namespace AdventOfCode2020.Solutions
{
    public class Day05 : ISolution
    {
        public void Solve()
        {
            var input = Input.Lines(nameof(Day05));
            Console.WriteLine(this.Puzzle1(input));
            Console.WriteLine(this.Puzzle2(input));
        }

        private int Puzzle1(string[] input) => input.Max(GetSeatId);

        private int Puzzle2(string[] input)
        {
            var all = input.Select(GetSeatId).OrderBy(x => x).ToArray();

            for (var i = 0; i < all.Length; i++)
            {
                if (all[i + 1] == all[i] + 2)
                    return all[i] + 1;
            }

            throw new InvalidProgramException();
        }

        private static int GetSeatId(string line)
        {
            var row = GetRow(line);
            var col = GetColumn(line);
            return row * 8 + col;
        }

        private static int GetRow(string line) => ReadFromBoardingPass(line, 0, 7, 128);

        private static int GetColumn(string line) => ReadFromBoardingPass(line, 7, 10, 8);

        private static int ReadFromBoardingPass(string line, int from, int to, int currentUpper)
        {
            var currentLower = 0;
            for (var i = from; i < to; i++)
            {
                if (line[i] == 'F' || line[i] == 'L')
                {
                    currentUpper -= (currentUpper - currentLower) / 2;
                }
                else if (line[i] == 'B' || line[i] == 'R')
                {
                    currentLower += (currentUpper - currentLower) / 2;
                }
            }

            return currentLower;
        }

        private class Tests
        {
            [TestCase("FBFBBFFRLR", 44)]
            [TestCase("BFFFBBFRRR", 70)]
            [TestCase("FFFBBBFRRR", 14)]
            [TestCase("BBFFBBFRLL", 102)]
            public void GetRow(string line, int expected)
            {
                var res = Day05.GetRow(line);
                Assert.AreEqual(expected, res);
            }

            [TestCase("FBFBBFFRLR", 5)]
            [TestCase("BFFFBBFRRR", 7)]
            [TestCase("FFFBBBFRRR", 7)]
            [TestCase("BBFFBBFRLL", 4)]
            public void GetColumn(string line, int expected)
            {
                var res = Day05.GetColumn(line);
                Assert.AreEqual(expected, res);
            }
        }
    }
}
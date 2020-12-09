using System;
using AdventOfCode2020.Solutions.Shared;
using NUnit.Framework;

namespace AdventOfCode2020.Solutions
{
    public class Day10 : ISolution
    {
        public void Solve()
        {
            var input = Input.Text(nameof(Day10));
            Console.WriteLine(this.Puzzle1(input));
            Console.WriteLine(this.Puzzle2(input));
        }

        private int Puzzle1(string input)
        {
            return -1;
        }

        private int Puzzle2(string input)
        {
            return -1;
        }

        private class Tests
        {
            [Test]
            public void Puzzle1()
            {
                var actual = new Day10().Puzzle1(TestInput);
                Assert.AreEqual(-1, actual);
            }

            [Test]
            public void Puzzle2()
            {
                var actual = new Day10().Puzzle2(TestInput);
                Assert.AreEqual(-1, actual);
            }

            private readonly string TestInput = @"";
        }
    }
}

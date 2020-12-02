using System;
using AdventOfCode2020.Solutions.Shared;
using NUnit.Framework;

namespace AdventOfCode2020.Solutions
{
    public class Day03 : ISolution
    {
        public void Solve()
        {
            var input = Input.Text(nameof(Day03));
            Console.WriteLine(this.Puzzle1());
            Console.WriteLine(this.Puzzle2());
        }

        private string Puzzle1()
        {
            return "P1";
        }

        private string Puzzle2()
        {
            return "P2";
        }

        private class Tests
        {
            [Test]
            public void Puzzle1()
            {
                const string expected = "";
                var actual = new Day03().Puzzle1();
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void Puzzle2()
            {
                const string expected = "";
                var actual = new Day03().Puzzle2();
                Assert.AreEqual(expected, actual);
            }
        }
    }
}
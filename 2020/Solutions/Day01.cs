using System;
using System.Linq;
using AdventOfCode2020.Solutions.Shared;
using NUnit.Framework;

namespace AdventOfCode2020.Solutions
{
    public class Day01 : ISolution
    {
        public void Solve()
        {
            var input = Input.LinesAsInt(nameof(Day01)).ToArray();
            Console.WriteLine(this.Puzzle1(input));
            Console.WriteLine(this.Puzzle2(input));
        }

        private string Puzzle1(int[] input)
        {
            for (var i = 0; i < input.Length - 1; i++)
            {
                var left = input[i];
                for (var j = i + 1; j < input.Length; j++)
                {
                    var right = input[j];
                    if (left + right == 2020)
                        return (left * right).ToString();
                }
            }
            throw new InvalidProgramException();
        }

        private string Puzzle2(int[] input)
        {
            for (var a = 0; a < input.Length - 2; a++)
            {
                var first = input[a];
                for (var b = a + 1; b < input.Length - 1; b++)
                {
                    var second = input[b];
                    for (var c = b + 1; c < input.Length; c++)
                    {
                        var third = input[c];
                        if (first + second + third == 2020)
                            return (first * second * third).ToString();
                    }
                }
            }
            throw new InvalidProgramException();
        }

        private class Tests
        {
            private readonly int[] testInput = new[] { 1721, 979, 366, 299, 675, 1456 };

            [Test]
            public void Puzzle1()
            {
                const string expected = "514579";
                var actual = new Day01().Puzzle1(this.testInput);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void Puzzle2()
            {
                const string expected = "241861950";
                var actual = new Day01().Puzzle2(this.testInput);
                Assert.AreEqual(expected, actual);
            }
        }
    }
}
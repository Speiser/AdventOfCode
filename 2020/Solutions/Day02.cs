using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.Solutions.Shared;
using NUnit.Framework;

namespace AdventOfCode2020.Solutions
{
    public class Day02 : ISolution
    {
        public void Solve()
        {
            var input = Input.Lines(nameof(Day02));
            Console.WriteLine(this.Puzzle1(input));
            Console.WriteLine(this.Puzzle2(input));
        }

        private int Puzzle1(string[] lines)
        {
            return this.SplitInput(lines).Count(entry =>
            {
                var count = entry.password.Count(c => c.ToString() == entry.letter);
                return count >= entry.min && count <= entry.max;
            });
        }

        private int Puzzle2(string[] lines)
        {
            return this.SplitInput(lines).Count(entry =>
            {
                var left = this.IsCharAtPos(entry.password, entry.letter, entry.min);
                var right = this.IsCharAtPos(entry.password, entry.letter, entry.max);
                return left ^ right;
            });
        }

        private bool IsCharAtPos(string password, string letter, int position)
            => password[position - 1].ToString() == letter;

        private IEnumerable<(int min, int max, string letter, string password)> SplitInput(string[] lines)
        {
            return lines.Select(line =>
            {
                var split = line.Split(":");
                var password = split[1].Trim();
                var policy = split[0].Split(" ");
                var letter = policy[1];
                var letterAmounts = policy[0].Split("-");
                return (int.Parse(letterAmounts[0]), int.Parse(letterAmounts[1]), letter, password);                
            });
        }

        private class Tests
        {
            private string[] testInput = new[] { "1-3 a: abcde", "1-3 b: cdefg", "2-9 c: ccccccccc" };

            [Test]
            public void Puzzle1()
            {
                const int expected = 2;
                var actual = new Day02().Puzzle1(testInput);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void Puzzle2()
            {
                const int expected = 1;
                var actual = new Day02().Puzzle2(testInput);
                Assert.AreEqual(expected, actual);
            }
        }
    }
}

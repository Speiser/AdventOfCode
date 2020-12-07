using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.Solutions.Shared;
using NUnit.Framework;

namespace AdventOfCode2020.Solutions
{
    public class Day06 : ISolution
    {
        public void Solve()
        {
            var input = Input.Text(nameof(Day06)).Blocks();
            Console.WriteLine(this.Puzzle1(input));
            Console.WriteLine(this.Puzzle2(input));
        }

        private int Puzzle1(string[] input) => input
            .Select(group => group.Replace("\r\n", string.Empty))
            .Sum(group => new HashSet<char>(group).Count);

        private int Puzzle2(string[] input)
        {
            var count = 0;
            foreach (var group in input)
            {
                var members = group.Split("\r\n");
                var resDict = new Dictionary<char, int>();
                foreach (var member in members)
                {
                    foreach (var letter in member)
                    {
                        if (resDict.ContainsKey(letter))
                        {
                            resDict[letter]++;
                        }
                        else
                        {
                            resDict.Add(letter, 1);
                        }
                    }
                }
                
                count += resDict.Count(x => x.Value == members.Length);
            }

            return count;
        }

        private class Tests
        {
            [Test]
            public void Puzzle1()
            {
                const int expected = 11;
                var actual = new Day06().Puzzle1(TestInput.Blocks());
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void Puzzle2()
            {
                const int expected = 6;
                var actual = new Day06().Puzzle2(TestInput.Blocks());
                Assert.AreEqual(expected, actual);
            }

            private const string TestInput = @"abc

a
b
c

ab
ac

a
a
a
a

b";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.Solutions.Shared;
using NUnit.Framework;

namespace AdventOfCode2020.Solutions
{
    public class Day22 : ISolution
    {
        public void Solve()
        {
            var input = Input.Text(nameof(Day22));
            Console.WriteLine(this.Puzzle1(input));
            Console.WriteLine(this.Puzzle2(input));
        }

        private int Puzzle1(string input)
        {
            var (p1, p2) = GetDecks(input);

            while (p1.Any() && p2.Any())
            {
                var card1 = p1.First();
                var card2 = p2.First();
                p1.RemoveAt(0);
                p2.RemoveAt(0);

                if (card1 > card2)
                {
                    p1.Add(card1);
                    p1.Add(card2);
                }
                else if (card2 > card1)
                {
                    p2.Add(card2);
                    p2.Add(card1);
                }
                else
                {
                    throw new InvalidProgramException();
                }
            }

            return (p1.Any() ? p1 : p2)
                .Reverse<int>()
                .Select((card, i) => card * (i + 1))
                .Sum();
        }

        private static (List<int> p1, List<int> p2) GetDecks(string input)
        {
            var split = input.Split("\r\n\r\n");
            return (GetDeck(0), GetDeck(1));
            List<int> GetDeck(int i) => split[i]
                .Split("\r\n", StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)
                .Select(int.Parse)
                .ToList();
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
                var actual = new Day22().Puzzle1(TestInput);
                Assert.AreEqual(-1, actual);
            }

            [Test]
            public void Puzzle2()
            {
                var actual = new Day22().Puzzle2(TestInput);
                Assert.AreEqual(-1, actual);
            }

            private readonly string TestInput = @"";
        }
    }
}

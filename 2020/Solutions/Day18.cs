using System;
using System.Linq;
using AdventOfCode2020.Solutions.Shared;
using NUnit.Framework;

namespace AdventOfCode2020.Solutions
{
    public class Day18 : ISolution
    {
        public void Solve()
        {
            var input = Input.Lines(nameof(Day18));
            Console.WriteLine(this.Puzzle1(input));
            Console.WriteLine(this.Puzzle2(input));
        }

        private long Puzzle1(string[] input) => input.Sum(EvaluateLine);

        private int Puzzle2(string[] input)
        {
            return -1;
        }

        private static long EvaluateLine(string line)
        {
            line = line.Replace(" ", string.Empty);
            long result = 0;
            for (var i = 0; i < line.Length; i++)
            {
                var current = line[i];
                if (current == '+' || current == '*')
                {
                    continue;
                }

                var prev = i > 0 ? line[i - 1] : '+';
                if (current == '(')
                {
                    var closing = FindClosing(line, i + 1);
                    var subExpr = EvaluateLine(line.Substring(i + 1, closing - i - 1));
                    if (prev == '+')
                    {
                        result += subExpr;
                    }
                    else if (prev == '*')
                    {
                        result *= subExpr;
                    }
                    i = closing + 1;
                    continue;
                }

                if (char.IsDigit(current))
                {
                    var value = current - '0';

                    if (prev == '+')
                    {
                        result += value;
                    }
                    else if (prev == '*')
                    {
                        result *= value;
                    }
                }
            }
            return result;
        }

        private static int FindClosing(string line, int starting)
        {
            var opening = 0;
            for (var i = starting; i < line.Length; i++)
            {
                if (line[i] == ')')
                {
                    if (opening == 0)
                    {
                        return i;
                    }

                    opening--;
                }
                if (line[i] == '(')
                {
                    opening++;
                }
            }
            throw new InvalidOperationException();
        }

        private class Tests
        {
            [TestCase("2 * 3 + (4 * 5)", 26)]
            [TestCase("1 + 2 * 3 + 4 * 5 + 6", 71)]
            [TestCase("1 + (2 * 3) + (4 * (5 + 6))", 51)]
            [TestCase("5 + (8 * 3 + 9 + 3 * 4 * 3)", 437)]
            [TestCase("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", 12240)]
            [TestCase("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 13632)]
            public void EvaluateLine(string input, int expected)
            {
                Assert.AreEqual(expected, Day18.EvaluateLine(input));
            }
        }
    }
}

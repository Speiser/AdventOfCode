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

        private long Puzzle1(string[] input) => input.Sum(EvaluateLine1);
        private long Puzzle2(string[] input) => input.Sum(EvaluateLine2);

        private static long EvaluateLine1(string line)
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
                    var subExpr = EvaluateLine1(line.Substring(i + 1, closing - i - 1));
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

        private static long EvaluateLine2(string line)
        {
            line = line.Replace(" ", string.Empty);
            while (line.Contains("("))
            {
                for (var i = 0; i < line.Length; i++)
                {
                    if (line[i] == '(')
                    {
                        var closing = FindClosing(line, i + 1);
                        var subExpr = EvaluateLine2(line.Substring(i + 1, closing - i - 1));
                        var l = line.Substring(0, i);
                        var r = line[(closing + 1)..];
                        line = $"{l}{subExpr}{r}";
                        break;
                    }
                }
            }
            while (line.Contains("+"))
            {
                for (var i = 0; i < line.Length; i++)
                {
                    if (line[i] == '+')
                    {
                        var left = GetLeftValue(line, i - 1);
                        var right = GetRightValue(line, i + 1);
                        var l = line.Substring(0, i - left.Length);
                        var r = line[(i + 1 + right.Length)..];
                        var subExpr = long.Parse(left) + long.Parse(right);
                        line = $"{l}{subExpr}{r}";
                        break;
                    }
                }
            }
            long prod = 1;
            foreach (var value in line.Split("*").Select(long.Parse))
            {
                prod *= value;
            }
            return prod;
        }

        private static string GetLeftValue(string line, int start)
        {
            var value = string.Empty;
            for (var i = start; i > -1; i--)
            {
                if (line[i] == '+' || line[i] == '*')
                {
                    break;
                }

                value = $"{line[i]}{value}";
            }
            return value;
        }

        private static string GetRightValue(string line, int start)
        {
            var value = string.Empty;
            for (var i = start; i < line.Length; i++)
            {
                if (line[i] == '+' || line[i] == '*')
                {
                    break;
                }

                value += line[i];
            }
            return value;
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
            public void EvaluateLine1(string input, int expected)
            {
                Assert.AreEqual(expected, Day18.EvaluateLine1(input));
            }

            [TestCase("1 + 2 * 3 + 4 * 5 + 6", 231)]
            [TestCase("2 * 3 + (4 * 5)", 46)]
            [TestCase("1 + (2 * 3) + (4 * (5 + 6))", 51)]
            [TestCase("5 + (8 * 3 + 9 + 3 * 4 * 3)", 1445)]
            [TestCase("5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", 669060)]
            [TestCase("((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 23340)]
            public void EvaluateLine2(string input, int expected)
            {
                Assert.AreEqual(expected, Day18.EvaluateLine2(input));
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode2020.Solutions.Shared;

namespace AdventOfCode2020.Solutions
{
    public class Day11 : ISolution
    {
        public void Solve()
        {
            var input = Input.Lines(nameof(Day11));
            Console.WriteLine(this.Puzzle1(input));
            Console.WriteLine(this.Puzzle2(input));
        }

        private int Puzzle1(string[] input) => Solve(input, GetAdjacent, 3);
        private int Puzzle2(string[] input) => Solve(input, GetVisible, 4);

        private static int Solve(string[] input, Func<string[], int, int, List<char>> getNeighbors, int maxOccupied)
        {
            while (true)
            {
                var afterStep = Step(input, getNeighbors, maxOccupied);
                if (Enumerable.SequenceEqual(input, afterStep))
                {
                    return afterStep.Sum(line => line.Count(c => c == '#'));
                }
                input = afterStep;
            }
        }

        private static string[] Step(string[] input, Func<string[], int, int, List<char>> getNeighbors, int maxOccupied)
        {
            var afterStep = new string[input.Length];

            for (var d = 0; d < input.Length; d++)
            {
                var newLine = new StringBuilder();
                for (var r = 0; r < input[d].Length; r++)
                {
                    var current = input[d][r] switch
                    {
                        'L' => getNeighbors(input, d, r).Any(seat => seat == '#') ? 'L' : '#',
                        '#' => getNeighbors(input, d, r).Count(seat => seat == '#') > maxOccupied ? 'L' : '#',
                        _ => input[d][r]
                    };
                    newLine.Append(current);
                }
                afterStep[d] = newLine.ToString();
            }

            return afterStep;
        }

        private static List<char> GetAdjacent(string[] input, int d, int r)
        {
            var result = new List<char>();
            for (var y = d - 1; y <= d + 1; y++)
            {
                for (var x = r - 1; x <= r + 1; x++)
                {
                    if (x == r && y == d)
                        continue;
                    if (y >= 0 && y < input.Length && x >= 0 && x < input[d].Length)
                        result.Add(input[y][x]);
                }
            }

            return result;
        }

        private static List<char> GetVisible(string[] input, int d, int r)
        {
            var result = new List<char>();
            bool AddIf(int y, int x)
            {
                if (input[y][x] == '.')
                    return false;
                result.Add(input[y][x]);
                return true;
            }
            // -1 -1
            var x = r - 1;
            var y = d - 1;
            while (y >= 0 && x >= 0)
            {
                if (AddIf(y, x))
                    break;
                y--;
                x--;
            }
            // -1 0
            for (y = d - 1; y >= 0; y--)
            {
                if (AddIf(y, r))
                    break;
            }
            // -1 1
            y = d - 1;
            x = r + 1;
            while (y >= 0 && x < input[d].Length)
            {
                if (AddIf(y, x))
                    break;
                y--;
                x++;
            }
            // 0 -1
            for (x = r - 1; x >= 0; x--)
            {
                if (AddIf(d, x))
                    break;
            }
            // 0 1
            for (x = r + 1; x < input[d].Length; x++)
            {
                if (AddIf(d, x))
                    break;
            }
            // 1 -1
            y = d + 1;
            x = r - 1;
            while (y < input.Length && x >= 0)
            {
                if (AddIf(y, x))
                    break;
                y++;
                x--;
            }
            // 1 0
            for (y = d + 1; y < input.Length; y++)
            {
                if (AddIf(y, r))
                    break;
            }
            // 1 1
            y = d + 1;
            x = r + 1;
            while (y < input.Length && x < input[d].Length)
            {
                if (AddIf(y, x))
                    break;
                y++;
                x++;
            }
            return result;
        }
    }
}

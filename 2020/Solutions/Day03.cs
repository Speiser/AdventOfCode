using System;
using AdventOfCode2020.Solutions.Shared;

namespace AdventOfCode2020.Solutions
{
    public class Day03 : ISolution
    {
        public void Solve()
        {
            var input = Input.Lines(nameof(Day03));
            Console.WriteLine(this.Puzzle1(input));
            Console.WriteLine(this.Puzzle2(input));
        }

        private long Puzzle1(string[] lines) => this.CountTrees(lines, 3, 1);

        private long Puzzle2(string[] lines) =>
            CountTrees(lines, 1, 1) *
            CountTrees(lines, 3, 1) *
            CountTrees(lines, 5, 1) *
            CountTrees(lines, 7, 1) *
            CountTrees(lines, 1, 2);

        private long CountTrees(string[] lines, int addX, int addY)
        {
            var x = 0;
            var y = 0;
            var treeCount = 0;

            while (y < lines.Length - 1)
            {
                x += addX;
                y += addY;

                var currentLine = lines[y];
                if (x >= currentLine.Length)
                {
                    x -= currentLine.Length;
                }

                var elementAt = lines[y][x];
                if (elementAt == '#')
                {
                    treeCount++;
                }
            }

            return treeCount;
        }
    }
}
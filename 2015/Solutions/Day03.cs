using System;
using System.Collections.Generic;
using AdventOfCode2015.Solutions.Shared;
using NUnit.Framework;

namespace AdventOfCode2015.Solutions
{
    public class Day03 : ISolution
    {
        public void Solve()
        {
            var input = Input.Text(nameof(Day03));
            Console.WriteLine(this.Puzzle1(input));
            Console.WriteLine(this.Puzzle2(input));
        }

        private int Puzzle1(string input)
        {
            var set = new HashSet<(int, int)>();
            var x = 0;
            var y = 0;
            set.Add((x, y));
            foreach (var c in input)
            {
                switch (c)
                {
                    case '^': y--; break;
                    case 'v': y++; break;
                    case '<': x--; break;
                    case '>': x++; break;
                }
                set.Add((x, y));
            }
            return set.Count;
        }

        private int Puzzle2(string input)
        {
            var set = new HashSet<(int, int)>();
            var sx = 0;
            var sy = 0;
            var rx = 0;
            var ry = 0;
            set.Add((0, 0));
            for (var i = 0; i < input.Length; i++)
            {
                switch (input[i])
                {
                    case '^': if (i % 2 == 0) sy--; else ry--; break;
                    case 'v': if (i % 2 == 0) sy++; else ry++; break;
                    case '<': if (i % 2 == 0) sx--; else rx--; break;
                    case '>': if (i % 2 == 0) sx++; else rx++; break;
                }
                set.Add((sx, sy));
                set.Add((rx, ry));
            }
            return set.Count;
        }
    }
}

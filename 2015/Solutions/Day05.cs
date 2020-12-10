using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2015.Solutions.Shared;

namespace AdventOfCode2015.Solutions
{
    public class Day05 : ISolution
    {
        public void Solve()
        {
            var input = Input.Lines(nameof(Day05));
            Console.WriteLine(this.Puzzle1(input));
            Console.WriteLine(this.Puzzle2(input));
        }

        private int Puzzle1(string[] input)
        {
            var dup = new List<string>();
            for (var c = 'a'; c <= 'z'; c++)
            {
                dup.Add($"{c}{c}");
            }
            var dont = new[] { "ab", "cd", "pq", "xy" };
            var should = new[] { 'a', 'e', 'i', 'o', 'u' };

            return input.Count(line =>
            {
                if (dont.Any(x => line.Contains(x))) return false;
                if (!dup.Any(x => line.Contains(x))) return false;

                var vowels = new List<char>();
                foreach (var c in line)
                {
                    if (should.Contains(c))
                        vowels.Add(c);
                }

                return vowels.Count > 2;
            });
        }

        private int Puzzle2(string[] input)
        {
            return input.Select((line, i) =>
            {
                var pairs = new List<string>();
                var repeat = false;
                for (var x = 0; x < line.Length - 1; x++)
                {
                    var left = line[x];
                    var right = line[x + 1];
                    var overlap = false;
                    if (left == right)
                    {
                        if (x < line.Length - 2)
                            overlap = left == line[x + 2];
                        if (x > 0)
                            overlap = overlap || left == line[x - 1];
                    }
                    if (overlap)
                    {
                        repeat = true;
                    }
                    else
                    {
                        pairs.Add($"{line[x]}{line[x + 1]}");
                        if (x < line.Length - 2)
                            repeat = repeat || left == line[x + 2];
                    }
                }
                var dups = pairs.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList();
                return dups.Any() && repeat;

            }).Count(x => x);
        }
    }
}

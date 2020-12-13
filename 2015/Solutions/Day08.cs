using System;
using System.Linq;
using System.Linq.Expressions;
using AdventOfCode2015.Solutions.Shared;
using NUnit.Framework;

namespace AdventOfCode2015.Solutions
{
    public class Day08 : ISolution
    {
        public void Solve()
        {
            var input = Input.Lines(nameof(Day08));
            Console.WriteLine(this.Puzzle1(input));
            Console.WriteLine(this.Puzzle2(input));
        }

        private int Puzzle1(string[] input)
        {
            var charCount = 0;
            var memCount = 0;

            foreach (var line in input)
            {
                charCount += line.Length;
                for (var i = 1; i < line.Length - 1; i++)
                {
                    if (line[i] == '\\')
                    {
                        i += line[i + 1] == 'x' ? 3 : 1;
                    }
                    memCount++;
                }
            }

            return charCount - memCount;
        }

        private int Puzzle2(string[] input)
        {
            var charCount = 0;
            var memCount = 0;

            foreach (var line in input)
            {
                charCount += line.Length;
                memCount += 2;
                for (var i = 0; i < line.Length; i++)
                {
                    if (line[i] == '\\') memCount++;
                    if (line[i] == '"') memCount++;
                    memCount++;
                }
            }

            return memCount - charCount;
        }
    }
}

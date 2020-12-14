using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.Solutions.Shared;

namespace AdventOfCode2020.Solutions
{
    public class Day14 : ISolution
    {
        public void Solve()
        {
            var input = Input.Lines(nameof(Day14));
            Console.WriteLine(this.Puzzle1(input));
            Console.WriteLine(this.Puzzle2(input));
        }

        private long Puzzle1(string[] input)
        {
            var currentMask = string.Empty;
            var memory = new Dictionary<int, string>();

            foreach (var line in input)
            {
                var split = line.Split(" = ");

                if (split[0] == "mask")
                {
                    currentMask = split[1];
                    continue;
                }

                var index = int.Parse(split[0].Replace("mem[", string.Empty).TrimEnd(']'));
                var value = Convert.ToString(int.Parse(split[1]), toBase: 2).PadLeft(36, '0').ToCharArray();

                for (var i = 0; i < value.Length; i++)
                {
                    if (currentMask[i] != 'X')
                    {
                        value[i] = currentMask[i];
                    }
                }

                memory[index] = string.Join(string.Empty, value);
            }

            return memory.Values.Sum(x => Convert.ToInt64(x, fromBase: 2));
        }

        private int Puzzle2(string[] input)
        {
            return -1;
        }
    }
}

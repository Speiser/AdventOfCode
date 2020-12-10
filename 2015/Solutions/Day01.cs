using System;
using AdventOfCode2015.Solutions.Shared;

namespace AdventOfCode2015.Solutions
{
    public class Day01 : ISolution
    {
        public void Solve()
        {
            var input = Input.Text(nameof(Day01));
            Console.WriteLine(this.Puzzle1(input));
            Console.WriteLine(this.Puzzle2(input));
        }

        private int Puzzle1(string input)
        {
            var count = 0;
            for (var i = 0; i < input.Length; i++)
            {
                if (input[i] == '(') count++;
                else if (input[i] == ')') count--;
            }
            return count;
        }

        private int Puzzle2(string input)
        {
            var count = 0;
            for (var i = 0; i < input.Length; i++)
            {
                if (input[i] == '(') count++;
                else if (input[i] == ')') count--;
                if (count == -1) return i + 1;
            }
            throw new InvalidOperationException();
        }
    }
}
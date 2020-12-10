using System;
using System.Linq;
using AdventOfCode2015.Solutions.Shared;

namespace AdventOfCode2015.Solutions
{
    public class Day06 : ISolution
    {
        public void Solve()
        {
            var input = Input.Lines(nameof(Day06));
            Console.WriteLine(this.Puzzle1(input));
            Console.WriteLine(this.Puzzle2(input));
        }

        private int Puzzle1(string[] input)
        {
            var grid = new bool[1000, 1000];
            var instructions = Parse(input);
            foreach (var instruction in instructions)
            {
                for (var x = instruction.From.x; x <= instruction.To.x; x++)
                {
                    for (var y = instruction.From.y; y <= instruction.To.y; y++)
                    {
                        switch (instruction.Operation)
                        {
                            case Operation.TurnOn:
                                grid[x, y] = true;
                                break;
                            case Operation.TurnOff:
                                grid[x, y] = false;
                                break;
                            case Operation.Toggle:
                                grid[x, y] = !grid[x, y];
                                break;
                        }
                    }
                }
            }

            return grid.Cast<bool>().Count(x => x);
        }

        private int Puzzle2(string[] input)
        {
            var grid = new int[1000, 1000];
            var instructions = Parse(input);
            foreach (var instruction in instructions)
            {
                for (var x = instruction.From.x; x <= instruction.To.x; x++)
                {
                    for (var y = instruction.From.y; y <= instruction.To.y; y++)
                    {
                        switch (instruction.Operation)
                        {
                            case Operation.TurnOn:
                                grid[x, y]++;
                                break;
                            case Operation.TurnOff:
                                if (grid[x, y] > 0) grid[x, y]--;
                                break;
                            case Operation.Toggle:
                                grid[x, y] += 2;
                                break;
                        }
                    }
                }
            }

            return grid.Cast<int>().Sum();
        }
     
        private static Instruction[] Parse(string[] input)
        {
            var result = new Instruction[input.Length];
            for (var i = 0; i < input.Length; i++)
            {
                var op = Operation.Undefined;
                var line = input[i];

                if (line.StartsWith("turn on"))
                {
                    op = Operation.TurnOn;
                    line = line.Replace("turn on ", string.Empty);
                }
                else if (line.StartsWith("turn off"))
                {
                    op = Operation.TurnOff;
                    line = line.Replace("turn off ", string.Empty);
                }
                else if (line.StartsWith("toggle"))
                {
                    op = Operation.Toggle;
                    line = line.Replace("toggle ", string.Empty);
                }

                var split = line.Split(" through ");
                result[i] = new Instruction(op, ReadRange(split[0]), ReadRange(split[1]));
            }
            return result;
        }

        private static (int, int) ReadRange(string range)
        {
            var split = range.Split(",");
            return (int.Parse(split[0]), int.Parse(split[1]));
        }

        private record Instruction(Operation Operation, (int x, int y) From, (int x, int y) To);
        private enum Operation { Undefined, TurnOn, TurnOff, Toggle }
    }
}

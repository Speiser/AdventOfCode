using System;
using AdventOfCode2020.Solutions.Shared;
using NUnit.Framework;

namespace AdventOfCode2020.Solutions
{
    public class Day12 : ISolution
    {
        public void Solve()
        {
            var input = Input.Lines(nameof(Day12));
            Console.WriteLine(this.Puzzle1(input));
            Console.WriteLine(this.Puzzle2(input));
        }

        private int Puzzle1(string[] input)
        {
            var facing = 90;
            var east = 0;
            var north = 0;

            foreach (var line in input)
            {
                var value = int.Parse(line[1..]);
                switch (line[0])
                {
                    case 'N': north += value; break;
                    case 'S': north -= value; break;
                    case 'E': east += value; break;
                    case 'W': east -= value; break;
                    case 'L':
                        facing -= value;
                        if (facing < 0)
                        {
                            facing = 360 + facing;
                        }
                        break;
                    case 'R':
                        facing += value;
                        facing %= 360;
                        break;
                    case 'F':
                        switch (facing)
                        {
                            case 0: north += value; break;
                            case 90: east += value; break;
                            case 180: north -= value; break;
                            case 270: east -= value; break;
                        }
                        break;
                }
            }

            return Math.Abs(north) + Math.Abs(east);
        }

        private int Puzzle2(string[] input)
        {
            var wayEast = 10;
            var wayNorth = 1;
            var shipEast = 0;
            var shipNorth = 0;

            foreach (var line in input)
            {
                var value = int.Parse(line[1..]);
                switch (line[0])
                {
                    case 'N': wayNorth += value; break;
                    case 'S': wayNorth -= value; break;
                    case 'E': wayEast += value; break;
                    case 'W': wayEast -= value; break;
                    case 'L':
                        for (var i = 0; i < value / 90; i++)
                        {
                            var oldEast = wayEast;
                            wayEast = -wayNorth;
                            wayNorth = oldEast;
                        }
                        break;
                    case 'R':
                        for (var i = 0; i < value / 90; i++)
                        {
                            var oldEast = wayEast;
                            wayEast = wayNorth;
                            wayNorth = -oldEast;
                        }
                        break;
                    case 'F':
                        shipEast += wayEast * value;
                        shipNorth += wayNorth * value;
                        break;
                }
            }

            return Math.Abs(shipNorth) + Math.Abs(shipEast);
        }

        private class Tests
        {
            [Test] public void Puzzle1() => Assert.AreEqual(25, new Day12().Puzzle1(this.testInput));
            [Test] public void Puzzle2() => Assert.AreEqual(286, new Day12().Puzzle2(this.testInput));
            private readonly string[] testInput = @"F10
N3
F7
R90
F11".Split("\r\n");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AdventOfCode2019.Solutions.Shared;

namespace AdventOfCode2019.Solutions
{
    public class Day13
    {
        public static int Puzzle1()
        {
            var computer = IntcodeComputer.LoadProgramFromFile("Input/Day13.txt");
            var arcade = new Arcade(computer);
            arcade.Run();
            return arcade.Tiles.Values.Count(t => t == TileType.Block);
        }

        public static int Puzzle2()
        {
            var computer = IntcodeComputer.LoadProgramFromFile("Input/Day13.txt");
            computer.Memory[0] = 2;
            var arcade = new Arcade(computer);
            arcade.Run();

            var remainingBlocks = arcade.Tiles.Values.Count(t => t == TileType.Block);
            if (remainingBlocks > 0)
                throw new InvalidProgramException();

            return arcade.Score;
        }

        private class Arcade
        {
            private readonly IntcodeComputer computer;
            private int ball;
            private int paddle;

            public Arcade(IntcodeComputer computer)
            {
                this.computer = computer;
            }

            public Dictionary<(int, int), TileType> Tiles { get; } = new Dictionary<(int, int), TileType>();
            public int Score { get; private set; }

            private void Step(int x, int y, int info)
            {
                if (x == -1 && y == 0)
                {
                    this.Score = info;
                }
                else
                {
                    var type = (TileType)info;
                    if (this.Tiles.ContainsKey((x, y)))
                    {
                        this.Tiles[(x, y)] = type;
                    }
                    else
                    {
                        this.Tiles.Add((x, y), type);
                    }

                    if (type == TileType.Ball)
                    {
                        this.ball = x;
                       
                    }
                    else if (type == TileType.Paddle)
                    {
                        this.paddle = x;
                    }
                }
            }

            public void Run()
            {
                var newOutputs = new List<int>();
                this.computer.EvaluateProgram(
                    () => JoystickInput(this.ball, this.paddle),
                    output =>
                    {
                        newOutputs.Add((int)output);
                        if (newOutputs.Count == 3)
                        {
                            this.Step(newOutputs[0], newOutputs[1], newOutputs[2]);
                            newOutputs.Clear();
                        }
                    });
            }

            private static int JoystickInput(int ball, int paddle)
            {
                if (ball > paddle) return 1;
                if (ball < paddle) return -1;
                return 0;
            }
        }

        [DebuggerDisplay("{X}-{Y} - {TileId}")]
        private class Tile
        {
            public int X { get; set; }
            public int Y { get; set; }
            public TileType Type { get; set; }
        }

        private enum TileType
        {
            Empty = 0,
            Wall = 1,
            Block = 2,
            Paddle = 3,
            Ball = 4
        }
    }
}

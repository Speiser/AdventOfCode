using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode.Solutions.Shared;

namespace AdventOfCode.Solutions
{
    public class Day11
    {
        public static int Puzzle1() => RunRobotWithInput(0).Count;

        public static string Puzzle2()
        {
            var result = RunRobotWithInput(1);

            // Normalize positions (x and y >= 0)
            var minX = result.Min(p => p.Position.X);
            var minY = result.Min(p => p.Position.Y);
            var sizeWidth = result.Max(p => p.Position.X - minX);
            var sizeHeight = result.Max(p => p.Position.Y - minY);

            var buffer = new bool[sizeHeight + 1, sizeWidth + 1];

            foreach (var paintJob in result)
            {
                var height = paintJob.Position.Y - minY;
                var width = paintJob.Position.X - minX;
                if (paintJob.Color == 1)
                {
                    buffer[height, width] = true;
                }
            }

            var builder = new StringBuilder();
            for (var h = 0; h < sizeHeight + 1; h++)
            {
                for (var w = 0; w < sizeWidth + 1; w++)
                {
                    builder.Append(buffer[h, w] ? "." : " ");
                }
                builder.AppendLine();
            }

            return builder.ToString();
        }

        private static List<PaintJob> RunRobotWithInput(long input)
        {
            var brain = IntcodeComputer.LoadProgramFromFile("Input/Day11.txt", input);
            var robot = new EmergencyHullPaintingRobot(brain);
            return robot.Run();
        }

        private class EmergencyHullPaintingRobot
        {
            private enum Direction { Up = 1, Right = 2, Down = 3, Left = 4 };
            private readonly IntcodeComputer brain;
            private Position currentPosition = new Position();
            private Direction currentDirection = Direction.Up;
            private readonly List<PaintJob> painted = new List<PaintJob>();

            public EmergencyHullPaintingRobot(IntcodeComputer brain)
            {
                this.brain = brain;
                var outputs = new int[] { -1, -1 };
                brain.OnOutput += output =>
                {
                    if (outputs[0] == -1)
                    {
                        outputs[0] = (int)output;
                        return;
                    }

                    outputs[1] = (int)output;

                    // Handle actions
                    this.Handle(outputs[0], outputs[1]);

                    // Reset outputs
                    outputs[0] = -1;
                };
            }

            public List<PaintJob> Run()
            {
                this.brain.EvaluateProgram().Wait();
                return this.painted;
            }

            private void Handle(int color, int rotate)
            {
                var paintJob = this.GetPaintJobOfCurrentPosition();
                if (paintJob is null)
                {
                    paintJob = new PaintJob { Position = this.currentPosition };
                    this.painted.Add(paintJob);
                }

                paintJob.Color = color;
                var direction = rotate == 0 ? ((int)this.currentDirection) - 1 : (((int)this.currentDirection) + 1) % 4;
                if (direction == 0)
                    direction = 4;

                this.currentDirection = (Direction)direction;

                switch (this.currentDirection)
                {
                    case Direction.Up:
                        this.currentPosition.Y++;
                        break;
                    case Direction.Right:
                        this.currentPosition.X++;
                        break;
                    case Direction.Down:
                        this.currentPosition.Y--;
                        break;
                    case Direction.Left:
                        this.currentPosition.X--;
                        break;
                }

                paintJob = this.GetPaintJobOfCurrentPosition();
                this.brain.InputStream.Enqueue(paintJob is null ? 0 : paintJob.Color);
            }

            private PaintJob GetPaintJobOfCurrentPosition() => this.painted.SingleOrDefault(p => p.Position.X == this.currentPosition.X && p.Position.Y == this.currentPosition.Y);
        }

        private class PaintJob
        {
            public int Color { get; set; }
            public Position Position { get; set; }
        }
        private struct Position
        {
            public int X { get; set; }
            public int Y { get; set; }
        }
    }
}
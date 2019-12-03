using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AdventOfCode.Solutions
{
    public class Day03
    {
        public static int Puzzle1()
        {
            var input = GetPuzzleInput();
            var intersections = GetIntersections(input);
            return intersections.Min(i => CalculateManhattanDistance(new Position(), i.Position));
        }

        public static int Puzzle2()
        {
            var input = GetPuzzleInput();
            var intersections = GetIntersections(input);
            return intersections.Min(i => i.TotalStepsToReach);
        }

        private static IEnumerable<Intersection> GetIntersections(string[] input)
        {
            var firstWirePath = GetWirePath(input[0]);
            var secondWirePath = GetWirePath(input[1]);

            var intersections = new List<Intersection>();

            foreach (var firstWireEdge in firstWirePath)
            {
                foreach (var secondWireEdge in secondWirePath)
                {
                    if (firstWireEdge.IntersectsWith(secondWireEdge, out var intersection))
                    {
                        intersections.Add(intersection);
                    }
                }
            }

            return intersections.Where(i => i.Position.X != 0 && i.Position.Y != 0);
        }

        private static string[] GetPuzzleInput() => File.ReadAllLines("Input/Day03.txt");

        private static IEnumerable<Edge> GetWirePath(string input)
        {
            var currentX = 0;
            var currentY = 0;
            var newX = 0;
            var newY = 0;

            var movements = input.Split(',');
            var path = new List<Edge>(movements.Length);

            foreach (var movement in movements)
            {
                var (direction, distance) = ParseMovement(movement);
                switch (direction)
                {
                    case 'L':
                        newX = currentX - distance;
                        break;
                    case 'R':
                        newX = currentX + distance;
                        break;
                    case 'U':
                        newY = currentY + distance;
                        break;
                    case 'D':
                        newY = currentY - distance;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException($"Unknown direction: {direction}");
                }

                path.Add(new Edge(
                    new Position { X = currentX, Y = currentY },
                    new Position { X = newX, Y = newY }) 
                    { Previous = path.LastOrDefault() });

                currentX = newX;
                currentY = newY;
            }

            return path;
        }

        private static int CalculateManhattanDistance(Position left, Position right) => Math.Abs(right.X - left.X) + Math.Abs(right.Y - left.Y);

        [DebuggerDisplay("{Start.X}:{Start.Y} - {End.X}:{End.Y}")]
        private class Edge
        {
            public Edge(Position start, Position end)
            {
                this.Start = start;
                this.End = end;
                this.IsHorizontal = this.Start.Y == this.End.Y;
            }

            public Edge Previous { get; set; }
            public Position Start { get; }
            public Position End { get; }
            public bool IsHorizontal { get; }

            public int GetDistanceToOrigin(Position from)
            {
                var distance = CalculateManhattanDistance(this.Start, from);

                var temp = this.Previous;
                while (temp != null)
                {
                    distance += CalculateManhattanDistance(temp.Start, temp.End);
                    temp = temp.Previous;
                }
                return distance;
            }

            public bool IntersectsWith(Edge other, out Intersection intersection)
            {
                intersection = null;

                if (this.IsHorizontal && other.IsHorizontal)
                    return false;

                Position intersectionPosition;

                if (this.IsHorizontal)
                {
                    var min = Math.Min(this.Start.X, this.End.X);
                    var max = Math.Max(this.Start.X, this.End.X);
                    
                    if (other.Start.X < min || other.Start.X > max)
                        return false;

                    var otherMin = Math.Min(other.Start.Y, other.End.Y);
                    var otherMax = Math.Max(other.Start.Y, other.End.Y);

                    if (this.Start.Y < otherMin || this.Start.Y > otherMax)
                        return false;
                    
                    intersectionPosition = new Position { X = other.Start.X, Y = this.Start.Y };
                }
                else
                {
                    var min = Math.Min(this.Start.Y, this.End.Y);
                    var max = Math.Max(this.Start.Y, this.End.Y);

                    if (other.Start.Y < min || other.Start.Y > max)
                        return false;

                    var otherMin = Math.Min(other.Start.X, other.End.X);
                    var otherMax = Math.Max(other.Start.X, other.End.X);

                    if (this.Start.X < otherMin || this.Start.X > otherMax)
                        return false;

                    intersectionPosition = new Position { X = this.Start.X, Y = other.Start.Y };
                }

                intersection = this.GetIntersection(intersectionPosition, other);
                return true;
            }

            private void AAAA(int a1, int a2, )

            private Intersection GetIntersection(Position intersectionPosition, Edge other)
            {
                var distance = this.GetDistanceToOrigin(intersectionPosition);
                var otherDistance = other.GetDistanceToOrigin(intersectionPosition);

                return new Intersection
                {
                    Position = intersectionPosition,
                    TotalStepsToReach = distance + otherDistance
                };
            }
        }

        [DebuggerDisplay("{X}:{Y}")]
        private class Position
        {
            public int X { get; set; }
            public int Y { get; set; }
        }

        private class Intersection
        {
            public Position Position { get; set; }
            public int TotalStepsToReach { get; set; }
        }

        private static (char, int) ParseMovement(string movement)
        {
            var direction = movement[0];
            var distance = int.Parse(movement.Substring(1));
            return (direction, distance);
        }
    }
}

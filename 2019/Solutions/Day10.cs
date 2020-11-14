using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Solutions
{
    public class Day10
    {
        public static int Puzzle1()
        {
            var asteroids = GetAsteroidPositions(GetPuzzleInput());
            return GetAngleAsteroidMapOfBest(asteroids, out var _).Count;
        }

        public static int Puzzle2()
        {
            var asteroids = GetAsteroidPositions(GetPuzzleInput());
            var asteroidMap = GetAngleAsteroidMapOfBest(asteroids, out var origin);
            SortAsteroidMapByDistance(origin, asteroidMap);
            var keyOrder = asteroidMap.Keys.OrderBy(x => x).ToArray();
            var target = new Position();

            for (var i = 0; i < 200; i++)
            {
                var positions = asteroidMap[keyOrder[i]];
                target = positions[0];
                positions.RemoveAt(0);
            }

            return target.X * 100 + target.Y;
        }

        private static Dictionary<double, List<Position>> GetAngleAsteroidMapOfBest(IEnumerable<Position> asteroids, out Position positionOfBest)
        {
            var best = new Dictionary<double, List<Position>>();
            positionOfBest = new Position();
            foreach (var asteroid in asteroids)
            {
                var current = GetAngleAsteroidMap(asteroid, asteroids);
                if (current.Count > best.Count)
                {
                    best = current;
                    positionOfBest = asteroid;
                }
            }

            return best;
        }

        private static Dictionary<double, List<Position>> GetAngleAsteroidMap(Position current, IEnumerable<Position> asteroids)
        {
            var anglesAsteroidsMap = new Dictionary<double, List<Position>>();
            foreach (var asteroid in asteroids)
            {
                if (current.X == asteroid.X && current.Y == asteroid.Y)
                    continue;

                var angle = Math.Atan2(-(asteroid.X - current.X), asteroid.Y - current.Y);

                var degs = angle * (180.0 / Math.PI) - 180;
                if (degs < 0)
                    degs += 360;

                if (!anglesAsteroidsMap.ContainsKey(degs))
                    anglesAsteroidsMap.Add(degs, new List<Position> { asteroid });
                else
                    anglesAsteroidsMap[degs].Add(asteroid);
            }

            return anglesAsteroidsMap;
        }

        private static void SortAsteroidMapByDistance(Position origin, Dictionary<double, List<Position>> map)
        {
            foreach (var (_, positions) in map)
            {
                positions.Sort((left, right) => origin.GetDistance(left).CompareTo(origin.GetDistance(right)));
            }
        }

        private static IEnumerable<Position> GetAsteroidPositions(string[] input)
        {
            var result = new List<Position>();
            for (var y = 0; y < input.Length; y++)
            {
                for (var x = 0; x < input[y].Length; x++)
                {
                    if (input[y][x] == '#')
                    {
                        result.Add(new Position { X = x, Y = y });
                    }
                }
            }
            return result;
        }

        private static string[] GetPuzzleInput() => File.ReadAllLines("Input/Day10.txt");

        private struct Position
        {
            public int X { get; set; }
            public int Y { get; set; }
            public double GetDistance(Position other) => Math.Sqrt(Math.Pow(other.X - this.X, 2) + Math.Pow(other.Y - this.Y, 2));
        }
    }
}

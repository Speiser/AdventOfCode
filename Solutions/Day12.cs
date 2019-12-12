using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;

namespace AdventOfCode.Solutions
{
    public class Day12
    {
        public static int Puzzle1()
        {
            var moons = GetMoons(GetPuzzleInput());
            for (var step = 0; step < 1000; step++)
            {
                ApplyGravity(moons);
                ApplyVelocity(moons);
            }

            return moons.Sum(m => m.TotalEnergy());
        }

        public static long Puzzle2()
        {
            var moons = GetMoons(GetPuzzleInput());
            
            var xSteps = CalculateStepsPerAxis(moons, 0);
            var ySteps = CalculateStepsPerAxis(moons, 1);
            var zSteps = CalculateStepsPerAxis(moons, 2);

            return GCF(xSteps, GCF(ySteps, zSteps));
        }

        private static int CalculateStepsPerAxis(Moon[] moons, int axis)
        {
            var states = new HashSet<string>();
            var steps = 0;
            var calcX = axis == 0;
            var calcY = axis == 1;
            var calcZ = axis == 2;

            while (true)
            {
                var state = GetState(moons, axis);
                if (states.Contains(state))
                {
                    break;
                }
                else
                {
                    states.Add(state);
                    ApplyGravity(moons, calcX, calcY, calcZ);
                    ApplyVelocity(moons, calcX, calcY, calcZ);
                    steps++;
                }
            }

            return steps;
        }

        private static string GetState(Moon[] moons, int axis)
        {
            var builder = new StringBuilder();

            foreach (var moon in moons)
            {
                builder.Append(GetAxisValue(moon.Position, axis));
                builder.Append(GetAxisValue(moon.Velocity, axis));
            }

            return builder.ToString();
        }

        private static int GetAxisValue(Vector3 vector, int axis)
        {
            float value = 0;
            if (axis == 0)
                value = vector.X;
            else if (axis == 1)
                value = vector.Y;
            else if (axis == 2)
                value = vector.Z;
            return (int)value;
        }

        private static long GCF(long a, long b) => a * b / GCD(a, b);

        private static long GCD(long a, long b)
        {
            while (a != b)
            {
                if (a < b) b -= a;
                else a -= b;
            }

            return a;
        }

        private static void ApplyGravity(Moon[] moons, bool calcX = true, bool calcY = true, bool calcZ = true)
        {
            foreach (var left in moons)
            {
                foreach (var right in moons)
                {
                    if (left == right)
                        continue;

                    left.Velocity = new Vector3(
                        x: calcX ? left.Velocity.X - left.Position.X.CompareTo(right.Position.X) : left.Velocity.X,
                        y: calcY ? left.Velocity.Y - left.Position.Y.CompareTo(right.Position.Y) : left.Velocity.Y,
                        z: calcZ ? left.Velocity.Z - left.Position.Z.CompareTo(right.Position.Z) : left.Velocity.Z);
                }
            }
        }

        private static void ApplyVelocity(Moon[] moons, bool calcX = true, bool calcY = true, bool calcZ = true)
        {
            foreach (var moon in moons)
            {
                moon.Position = new Vector3(
                    x: calcX ? moon.Position.X + moon.Velocity.X : moon.Position.X,
                    y: calcY ? moon.Position.Y + moon.Velocity.Y : moon.Position.Y,
                    z: calcZ ? moon.Position.Z + moon.Velocity.Z : moon.Position.Z);
            }
        }

        private static Moon[] GetMoons(string[] input)
        {
            var moons = new Moon[input.Length];
            for (var i = 0; i < input.Length; i++)
            {
                var coords = input[i].Replace(" ", "").Trim('<', '>').Split(',');
                moons[i] = new Moon
                {
                    Position = new Vector3(
                        x: int.Parse(coords[0].Replace("x=", "")),
                        y: int.Parse(coords[1].Replace("y=", "")),
                        z: int.Parse(coords[2].Replace("z=", "")))
                };
            }

            return moons;
        }

        private static string[] GetPuzzleInput() => File.ReadAllLines("Input/Day12.txt");

        private class Moon
        {
            public Vector3 Position { get; set; }
            public Vector3 Velocity { get; set; }

            public int PotentialEnergy() => EnergyOfVector(this.Position);
            public int KineticEnergy() => EnergyOfVector(this.Velocity);
            public int TotalEnergy() => this.PotentialEnergy() * this.KineticEnergy();

            private static int EnergyOfVector(Vector3 vec) => (int)(Math.Abs(vec.X) + Math.Abs(vec.Y) + Math.Abs(vec.Z));
        }
    }
}

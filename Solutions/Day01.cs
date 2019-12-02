using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Solutions
{
    public class Day01
    {
        public static int Puzzle1()
            => GetPuzzleInput().Sum(CalculateFuel);

        public static int Puzzle2()
        {
            var acc = 0;
            foreach (var input in GetPuzzleInput())
            {
                var result = CalculateFuel(input);
                while (result > 0)
                {
                    acc += result;
                    result = CalculateFuel(result);
                }
            }
            return acc;
        }

        private static int CalculateFuel(int i) => (i / 3) - 2;

        private static IEnumerable<int> GetPuzzleInput()
            => File.ReadAllLines("Input/Day01.txt").Select(int.Parse);
    }
}
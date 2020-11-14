using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2019.Solutions
{
    public class Day04
    {
        private const string PuzzleInput = "264793-803935";

        public static int Puzzle1() => GetPossibleSolutionCount();
        public static int Puzzle2() => GetPossibleSolutionCount(checkForThreeAdjacent: true);

        private static int GetPossibleSolutionCount(bool checkForThreeAdjacent = false)
        {
            var (lower, upper) = GetSolutionRange();
            var possibleSolutions = new List<int>();
            for (; lower <= upper; lower++)
            {
                if (CompliesWithRules(lower, checkForThreeAdjacent))
                    possibleSolutions.Add(lower);
            }
            return possibleSolutions.Count;
        }

        private static bool CompliesWithRules(int current, bool checkForThreeAdjacent)
        {
            var digits = ToDigits(current);
            var twoAdjacent = false;

            for (var i = 0; i < digits.Length; i++)
            {
                var digit = digits[i];
                if (i < digits.Length - 1)
                {
                    var next = digits[i + 1];
                    if (digit == next)
                    {
                        if (checkForThreeAdjacent)
                        {
                            if (i < digits.Length - 2 && digit == digits[i + 2])
                            {
                                i++;
                                continue;
                            }
                            if (i > 0 && digit == digits[i - 1])
                            {
                                continue;
                            }
                        }

                        twoAdjacent = true;
                    }
                    else if (digit > next)
                    {
                        return false;
                    }
                }
            }

            return twoAdjacent;
        }

        private static int[] ToDigits(int number) => number.ToString().Select(c => c - 48).ToArray();

        private static (int, int) GetSolutionRange()
        {
            var input = PuzzleInput.Split('-').Select(int.Parse).ToArray();
            return (input[0], input[1]);
        }
    }
}

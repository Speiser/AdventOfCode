using AdventOfCode.Solutions.Shared;

namespace AdventOfCode.Solutions
{
    public class Day05
    {
        public static int Puzzle1() => (int)RunWithInput(1);

        public static int Puzzle2() => (int)RunWithInput(5);

        private static long RunWithInput(long input)
        {
            long result = 0;
            var computer = IntcodeComputer.LoadProgramFromFile("Input/Day05.txt");
            computer.EvaluateProgram(() => input, output => result = output);
            return result;
        }
    }
}

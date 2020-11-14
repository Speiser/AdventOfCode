using AdventOfCode2019.Solutions.Shared;

namespace AdventOfCode2019.Solutions
{
    public class Day09
    {
        public static long Puzzle1() => RunWithInput(1);
        public static long Puzzle2() => RunWithInput(2);

        private static long RunWithInput(long input)
        {
            long result = 0;
            var computer = IntcodeComputer.LoadProgramFromFile("Input/Day09.txt");
            computer.EvaluateProgram(() => input, output => result = output);
            return result;
        }
    }
}

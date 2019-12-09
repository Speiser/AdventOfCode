using AdventOfCode.Solutions.Shared;

namespace AdventOfCode.Solutions
{
    public class Day09
    {
        public static long Puzzle1() => RunWithInput(1);
        public static long Puzzle2() => RunWithInput(2);

        private static long RunWithInput(long input)
        {
            long result = 0;
            var computer = IntcodeComputer.LoadProgramFromFile("Input/Day09.txt", input);
            computer.OnOutput += output => result = output;
            computer.EvaluateProgram().Wait();
            return result;
        }
    }
}

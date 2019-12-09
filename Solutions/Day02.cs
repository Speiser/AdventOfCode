using System.IO;
using AdventOfCode.Solutions.Shared;

namespace AdventOfCode.Solutions
{
    public class Day02
    {
        public static int Puzzle1()
        {
            var computer = IntcodeComputer.LoadProgramFromFile("Input/Day02.txt");

            // Set 1202 Program Alarm
            computer.Memory[1] = 12;
            computer.Memory[2] = 2;

            computer.EvaluateProgram().Wait();

            return (int)computer.Memory[0];
        }

        public static int Puzzle2()
        {
            var input = GetInputFromFile();

            for (var noun = 0; noun < 100; noun++)
            {
                for (var verb = 0; verb < 100; verb++)
                {
                    var computer = IntcodeComputer.LoadProgramFromString(input);
                    computer.Memory[1] = noun;
                    computer.Memory[2] = verb;
                    computer.EvaluateProgram().Wait();
                    if (computer.Memory[0] == 19690720)
                        return noun * 100 + verb;
                }
            }
            return -1;
        }

        private static string GetInputFromFile() => File.ReadAllText("Input/Day02.txt");
    }
}

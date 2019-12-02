using System.IO;
using System.Linq;

namespace AdventOfCode.Solutions
{
    public class Day02
    {
        public static int Puzzle1()
        {
            var program = FromString(GetInputFromFile());

            // Set 1202 Program Alarm
            program[1] = 12;
            program[2] = 2;

            return EvaluateProgram(program);
        }

        public static int Puzzle2()
        {
            var input = GetInputFromFile();

            for (var noun = 0; noun < 100; noun++)
            {
                for (var verb = 0; verb < 100; verb++)
                {
                    var program = FromString(input);
                    program[1] = noun;
                    program[2] = verb;
                    var result = EvaluateProgram(program);
                    if (result == 19690720)
                        return noun * 100 + verb;
                }
            }
            return -1;
        }

        private static int EvaluateProgram(int[] program)
        {
            for (var i = 0; i < program.Length; i += 4)
            {
                var opcode = program[i];
                if (opcode == 99) // halt
                    break;
                var leftIndex = program[i + 1];
                var rightIndex = program[i + 2];
                var resultIndex = program[i + 3];
                if (opcode == 1)
                    program[resultIndex] = program[leftIndex] + program[rightIndex];
                else if (opcode == 2)
                    program[resultIndex] = program[leftIndex] * program[rightIndex];
            }
            return program[0];
        }

        private static string GetInputFromFile() => File.ReadAllText("Input/Day02.txt");
        private static int[] FromString(string input) => input.Split(',').Select(int.Parse).ToArray();
    }
}

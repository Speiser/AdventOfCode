namespace AdventOfCode2021;

public class Day03 : Solution
{
    public override void Solve()
    {
        var input = Input.Lines(nameof(Day03));
        Console.WriteLine(this.Puzzle1(input));
        Console.WriteLine(this.Puzzle2(input));
    }

    private int Puzzle1(string[] input)
    {
        var gamma = "";
        var epsilon = "";

        for (var i = 0; i < input[0].Length; i++)
        {
            var ones = input.Count(x => x[i] == '1');
            if (ones >= (input.Length / 2))
            {
                gamma += "1";
                epsilon += "0";
            }
            else
            {
                gamma += "0";
                epsilon += "1";
            }
        }

        return gamma.ToInt() * epsilon.ToInt();
    }

    private int Puzzle2(string[] input)
    {
        return GetRemainingValue(input, keepMostCommon: true) * GetRemainingValue(input, keepMostCommon: false);
    }

    private static int GetRemainingValue(string[] input, bool keepMostCommon)
    {
        var list = input.ToList();
        for (var i = 0; i < input[0].Length; i++)
        {
            var ones = list.Count(x => x[i] == '1');
            var keepOnes = !((ones >= (list.Count / 2.0)) ^ keepMostCommon);
            list = list.Where(x => x[i] == (keepOnes ? '1' : '0')).ToList();

            if (list.Count == 1)
            {
                return list[0].ToInt();
            }
        }

        throw new InvalidProgramException();
    }
}

internal static class StringExtensions
{
    public static int ToInt(this string binary) => Convert.ToInt32(binary, 2);
}
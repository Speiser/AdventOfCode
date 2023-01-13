namespace AdventOfCode2022;

public class Day01 : Solution
{
    public override void Solve()
    {
        var input = Input.Lines(nameof(Day01));
        Console.WriteLine(this.Puzzle1(input));
        Console.WriteLine(this.Puzzle2(input));
    }

    private int Puzzle1(string[] input) => GetCalories(input).Max();

    private int Puzzle2(string[] input) => GetCalories(input)
        .OrderByDescending(x => x)
        .Take(3)
        .Sum();

    private List<int> GetCalories(string[] input)
    {
        var cals = new List<int>();
        var current = 0;

        foreach (var line in input)
        {
            if (line == "")
            {
                cals.Add(current);
                current = 0;
                continue;
            }

            current += int.Parse(line);
        }

        cals.Add(current);
        return cals;
    }
}
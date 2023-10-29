namespace AdventOfCode2022;

public class Day04 : Solution
{
    public override void Solve()
    {
        var input = Input.Lines(nameof(Day04));
        Console.WriteLine(this.Puzzle1(input));
        Console.WriteLine(this.Puzzle2(input));
    }

    private int Puzzle1(string[] input)
    {
        var count = 0;
        var parsed = input.Select(i =>
        {
            var s = i.Split(',');
            var l = s[0].Split('-');
            var r = s[1].Split('-');
            return (int.Parse(l[0]), int.Parse(l[1]), int.Parse(r[0]), int.Parse(r[1]));
        });

        foreach (var line in parsed)
        {
            if (line.Item1 <= line.Item3 && line.Item2 >= line.Item4)
            {
                count++;
                continue;
            }

            if (line.Item3 <= line.Item1 && line.Item4 >= line.Item2)
            {
                count++;
            }
        }

        return count;
    }

    private int Puzzle2(string[] input)
    {
        var count = 0;
        var parsed = input.Select(i =>
        {
            var s = i.Split(',');
            var l = s[0].Split('-');
            var r = s[1].Split('-');
            return (int.Parse(l[0]), int.Parse(l[1]), int.Parse(r[0]), int.Parse(r[1]));
        });

        foreach (var line in parsed)
        {
            if (line.Item2 >= line.Item3 && line.Item1 <= line.Item4)
            {
                count++;
            }
        }

        return count;
    }
}
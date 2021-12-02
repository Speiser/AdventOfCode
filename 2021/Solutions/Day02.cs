namespace AdventOfCode2021;

public class Day02 : Solution
{
    public override void Solve()
    {
        var input = Input.Lines(nameof(Day02)).ToArray();
        Console.WriteLine(this.Puzzle1(input));
        Console.WriteLine(this.Puzzle2(input));
    }

    private int Puzzle1(string[] input)
    {
        var hPos = 0;
        var depth = 0;

        foreach (var line in input)
        {
            var split = line.Trim().Split(" ");
            var val = int.Parse(split[1]);
            switch (split[0])
            {
                case "forward":
                    hPos += val;
                    break;
                case "up":
                    depth -= val;
                    break;
                case "down":
                    depth += val;
                    break;
            }
        }

        return hPos * depth;
    }

    private int Puzzle2(string[] input)
    {
        var hPos = 0;
        var depth = 0;
        var aim = 0;

        foreach (var line in input)
        {
            var split = line.Trim().Split(" ");
            var val = int.Parse(split[1]);
            switch (split[0])
            {
                case "forward":
                    hPos += val;
                    depth += aim * val;
                    break;
                case "up":
                    aim -= val;
                    break;
                case "down":
                    aim += val;
                    break;
            }
        }

        return hPos * depth;
    }
}
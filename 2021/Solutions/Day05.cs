namespace AdventOfCode2021;

public class Day05 : Solution
{
    public override void Solve()
    {
        var input = Input.Lines(nameof(Day05));
        Console.WriteLine(this.Puzzle1(input));
        Console.WriteLine(this.Puzzle2(input));
    }

    private int Puzzle1(string[] input)
    {
        var points = new Dictionary<(int, int), int>();
        foreach (var line in input)
        {
            var (x1, y1, x2, y2) = ParseLine(line);

            if (x1 == x2 || y1 == y2)
            {
                Magic(points, x1, y1, x2, y2);
            }
        }

        return points.Values.Count(x => x > 1);
    }

    private int Puzzle2(string[] input)
    {
        var points = new Dictionary<(int, int), int>();
        foreach (var line in input)
        {
            var (x1, y1, x2, y2) = ParseLine(line);

            Magic(points, x1, y1, x2, y2);
        }

        return points.Values.Count(x => x > 1);
    }

    private static void Magic(Dictionary<(int, int), int> points, int x1, int y1, int x2, int y2)
    {
        var dx = x1 == x2 ? 0 : Math.Sign(x2 - x1);
        var yx = y1 == y2 ? 0 : Math.Sign(y2 - y1);
        points.Increment((x1, y1));

        while (!(x1 == x2 && y1 == y2))
        {
            x1 += dx;
            y1 += yx;
            points.Increment((x1, y1));
        }
    }

    private static (int x1, int y1, int x2, int y2) ParseLine(string line)
    {
        var split = line.Split(" -> ");
        var (x1, y1) = SplitSide(split[0]);
        var (x2, y2) = SplitSide(split[1]);
        return (x1, y1, x2, y2);

        static (int x, int y) SplitSide(string side)
        {
            var split = side.Split(',').Select(int.Parse).ToArray();
            return (split[0], split[1]);
        }
    }
}

internal static class DictionaryExtensions
{
    public static void Increment<T>(this Dictionary<T, int> dict, T key)
    {
        if (dict.ContainsKey(key))
        {
            dict[key]++;
            return;
        }

        dict.Add(key, 1);
    }
}

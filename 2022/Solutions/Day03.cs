using System.Diagnostics;

namespace AdventOfCode2022;

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
        //Console.WriteLine(((int)'A') - 38);
        //Console.WriteLine(((int)'a') - 96);

        var sum = 0;

        foreach (var line in input)
        {
            var len = line.Length / 2;
            var l = line.Substring(0, len);
            var r = line.Substring(len);
            Debug.Assert(r.Length == l.Length);

            foreach (var c in l)
            {
                if (r.Contains(c))
                {
                    sum += char.IsUpper(c) ? c - 38 : c - 96;
                    break;
                }
            }
        }

        return sum;
    }

    private int Puzzle2(string[] input)
    {
        var sum = 0;

        for (var i = 0; i < input.Length - 2; i += 3)
        {
            foreach (var c in input[i])
            {
                if (input[i + 1].Contains(c) && input[i + 2].Contains(c))
                {
                    sum += char.IsUpper(c) ? c - 38 : c - 96;
                    break;
                }
            }
        }

        return sum;
    }
}
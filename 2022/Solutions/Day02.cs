namespace AdventOfCode2022;

public class Day02 : Solution
{
    public override void Solve()
    {
        var input = Input.Lines(nameof(Day02));
        Console.WriteLine(this.Puzzle1(input));
        Console.WriteLine(this.Puzzle2(input));
    }

    private int Puzzle1(string[] input)
    {
        // A for Rock, B for Paper, and C for Scissors
        // X for Rock, Y for Paper, and Z for Scissors
        // 1 for Rock, 2 for Paper, and 3 for Scissors
        // 0 if you lost, 3 if the round was a draw, and 6 if you won
        var draws = new[] { "A X", "B Y", "C Z" };
        var wins = new[] { "A Y", "B Z", "C X" };
        // var losses = new[] { "A Z", "B X", "C Y" };
        var totalScore = 0;

        foreach (var line in input)
        {
            if (draws.Contains(line)) totalScore += 3;
            else if (wins.Contains(line)) totalScore += 6;

            totalScore += line[2] switch
            {
                'X' => 1,
                'Y' => 2,
                'Z' => 3,
                _ => throw new ArgumentOutOfRangeException(),
            };
        }

        return totalScore;
    }

    private int Puzzle2(string[] input)
    {
        // A for Rock, B for Paper, and C for Scissors
        // X lose, Y draw, Z win
        // 1 for Rock, 2 for Paper, and 3 for Scissors
        // 0 if you lost, 3 if the round was a draw, and 6 if you won
        var totalScore = 0;

        foreach (var line in input)
        {
            switch (line[2])
            {
                case 'X':
                    totalScore += line[0] switch
                    {
                        'A' => 3,
                        'B' => 1,
                        'C' => 2,
                        _ => throw new ArgumentOutOfRangeException(),
                    };
                    break;
                case 'Y':
                    totalScore += 3 + line[0] switch
                    {
                        'A' => 1,
                        'B' => 2,
                        'C' => 3,
                        _ => throw new ArgumentOutOfRangeException(),
                    };
                    break;
                case 'Z':
                    totalScore += 6 + line[0] switch
                    {
                        'A' => 2,
                        'B' => 3,
                        'C' => 1,
                        _ => throw new ArgumentOutOfRangeException(),
                    };
                    break;
            }
        }

        return totalScore;
    }
}
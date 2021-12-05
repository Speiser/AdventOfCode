namespace AdventOfCode2021;

public class Day04 : Solution
{
    public override void Solve()
    {
        var input = Input.Blocks(nameof(Day04));
        Console.WriteLine(this.Puzzle1(input));
        Console.WriteLine(this.Puzzle2(input));
    }

    private int Puzzle1(string[] input)
    {
        var drawNumbers = GetDrawNumbers(input);
        var boards = GetBoards(input);

        foreach (var drawNumber in drawNumbers)
        {
            foreach (var board in boards)
            {
                for (var i = 0; i < board.Length; i++)
                {
                    if (board[i] == drawNumber)
                    {
                        board[i] = -1;
                    }
                }

                if (IsFinished(board))
                {
                    return board.Where(x => x > 0).Sum() * drawNumber;
                }
            }
        }

        throw new InvalidProgramException();
    }

    private static int[] GetDrawNumbers(string[] input)
        => input[0].Split(',').Select(int.Parse).ToArray();

    private static List<int[]> GetBoards(string[] input)
    {
        var boards = new List<int[]>();
        for (var i = 1; i < input.Length; i++)
        {
            var ints = input[i].Replace("\r\n", " ").Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            boards.Add(ints);
        }
        return boards;
    }

    private static bool IsFinished(int[] board)
    {
        for (var i = 0; i < 25; i += 5)
        {
            if (board[i] + board[i + 1] + board[i + 2] + board[i + 3] + board[i + 4] == -5)
            {
                return true;
            }
        }

        for (var i = 0; i < 5; i++)
        {
            if (board[i] + board[i + 5] + board[i + 10] + board[i + 15] + board[i + 20] == -5)
            {
                return true;
            }
        }

        return false;
    }

    private int Puzzle2(string[] input)
    {
        var drawNumbers = GetDrawNumbers(input);
        var boards = GetBoards(input);
        var last = false;

        foreach (var drawNumber in drawNumbers)
        {
            foreach (var board in boards)
            {
                for (var i = 0; i < board.Length; i++)
                {
                    if (board[i] == drawNumber)
                    {
                        board[i] = -1;
                    }
                }
            }

            if (boards.Count > 1)
            {
                boards = boards.Where(x => !IsFinished(x)).ToList();
                continue;
            }

            if (IsFinished(boards[0]))
            {
                return boards[0].Where(x => x > 0).Sum() * drawNumber;
            }
        }

        throw new InvalidProgramException();
    }
}

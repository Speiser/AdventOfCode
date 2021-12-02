namespace AdventOfCode2021;

public class Day01 : Solution
{
    public override void Solve()
    {
        var input = Input.LinesAsInt(nameof(Day01)).ToArray();
        Console.WriteLine(this.Puzzle1(input));
        Console.WriteLine(this.Puzzle2(input));
    }

    private int Puzzle1(int[] input)
    {
        var count = 0;
        for (var i = 1; i < input.Length; i++)
        {
            if (input[i] > input[i - 1])
            {
                count++;
            }
        }
        return count;
    }

    private int Puzzle2(int[] input)
    {
        var count = 0;
        for (var i = 3; i < input.Length; i++)
        {
            if (input[i] > input[i - 3])
            {
                count++;
            }
        }
        return count;
    }

    private class Tests
    {
        private readonly int[] testInput = new[] { 199, 200, 208, 210, 200, 207, 240, 269, 260, 263 };

        [Test]
        public void Puzzle1()
        {
            var actual = new Day01().Puzzle1(this.testInput);
            Assert.AreEqual(7, actual);
        }

        [Test]
        public void Puzzle2()
        {
            var actual = new Day01().Puzzle2(this.testInput);
            Assert.AreEqual(5, actual);
        }
    }
}
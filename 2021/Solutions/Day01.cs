namespace AdventOfCode2021;

public class Day01 : Solution
{
    public override void Solve()
    {
        var input = Input.LinesAsInt(nameof(Day01)).ToArray();
        Console.WriteLine(this.Puzzle1(input));
        Console.WriteLine(this.Puzzle2(input));
    }

    private string Puzzle1(int[] input)
    {
        return "30";
    }

    private string Puzzle2(int[] input)
    {
        return "1234";
    }

    private class Tests
    {
        private readonly int[] testInput = new[] { 0 };

        [Test]
        public void Puzzle1()
        {
            const string expected = "514579";
            var actual = new Day01().Puzzle1(this.testInput);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Puzzle2()
        {
            const string expected = "241861950";
            var actual = new Day01().Puzzle2(this.testInput);
            Assert.AreEqual(expected, actual);
        }
    }
}


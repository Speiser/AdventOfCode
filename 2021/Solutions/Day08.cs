namespace AdventOfCode2021;

public class Day08 : Solution
{
    public override void Solve()
    {
        var input = Input.Lines(nameof(Day08));
        Console.WriteLine(this.Puzzle1(input));
        Console.WriteLine(this.Puzzle2(input));
    }

    private int Puzzle1(string[] input)
    {
        return -1;
    }

    private int Puzzle2(string[] input)
    {
        return -1;
    }

    private class Tests
    {
        [Test]
        public void Puzzle1()
        {
            var actual = new Day08().Puzzle1(TestInput);
            Assert.AreEqual(-1, actual);
        }

        [Test]
        public void Puzzle2()
        {
            var actual = new Day08().Puzzle2(TestInput);
            Assert.AreEqual(-1, actual);
        }

        private readonly string[] TestInput = @"".Split("\r\n");
    }
}

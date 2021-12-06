namespace AdventOfCode2021;

public class Day06 : Solution
{
    public override void Solve()
    {
        var input = Input.Text(nameof(Day06));
        Console.WriteLine(this.Puzzle1(input));
        Console.WriteLine(this.Puzzle2(input));
    }

    private long Puzzle1(string input) => Calculate(input, 80);
    private long Puzzle2(string input) => Calculate(input, 256);

    private long Calculate(string input, int days)
    {
        var times = Enumerable.Repeat<long>(0, 9).ToList();
        foreach (var i in input.Split(',').Select(int.Parse))
        {
            times[i]++;
        }

        var day = 0;
        while (day++ < days)
        {
            var zeros = times[0];
            times.RemoveAt(0);
            times.Add(zeros);
            times[6] += zeros;
        }

        return times.Sum();
    }

    private class Tests
    {
        [Test]
        public void Puzzle1()
        {
            var actual = new Day06().Puzzle1(TestInput);
            Assert.AreEqual(5934, actual);
        }

        [Test]
        public void Puzzle2()
        {
            var actual = new Day06().Puzzle2(TestInput);
            Assert.AreEqual(26984457539, actual);
        }

        private readonly string TestInput = "3,4,3,1,2";
    }
}

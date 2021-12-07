namespace AdventOfCode2021;

public class Day07 : Solution
{
    public override void Solve()
    {
        var input = Input.Text(nameof(Day07));
        Console.WriteLine(this.Puzzle1(input));
        Console.WriteLine(this.Puzzle2(input));
    }

    private int Puzzle1(string input)
    {
        var crabPositions = input.Split(',').Select(int.Parse).ToArray();
        var distinctPositions = crabPositions.Distinct().ToArray();

        var results = new List<int>();

        for (var distinctPosition = crabPositions.Min(); distinctPosition <= crabPositions.Max(); distinctPosition++)
        {
            results.Add(crabPositions.Sum(x => Math.Abs(distinctPosition - x)));
        }

        return results.Min();
    }

    private int Puzzle2(string input)
    {
        var crabPositions = input.Split(',').Select(int.Parse).ToArray();
        var results = new List<int>();

        for (var distinctPosition = crabPositions.Min(); distinctPosition <= crabPositions.Max(); distinctPosition++)
        {
            var sum = 0;
            foreach (var crabPosition in crabPositions)
            {
                for (var i = 1; i <= Math.Abs(distinctPosition - crabPosition); i++)
                {
                    sum += i;
                }
            }
            results.Add(sum);
        }

        return results.Min();
    }

    private class Tests
    {
        [Test]
        public void Puzzle1()
        {
            var actual = new Day07().Puzzle1(TestInput);
            Assert.AreEqual(37, actual);
        }

        [Test]
        public void Puzzle2()
        {
            var actual = new Day07().Puzzle2(TestInput);
            Assert.AreEqual(168, actual);
        }

        private readonly string TestInput = "16,1,2,0,4,2,7,1,2,14";
    }
}
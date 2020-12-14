using System;
using System.Linq;
using AdventOfCode2020.Solutions.Shared;

namespace AdventOfCode2020.Solutions
{
    public class Day13 : ISolution
    {
        public void Solve()
        {
            var input = Input.Lines(nameof(Day13));
            Console.WriteLine(this.Puzzle1(input));
            Console.WriteLine(this.Puzzle2(input));
        }

        private int Puzzle1(string[] input)
        {
            var id = int.Parse(input[0]);
            var items = input[1].Split(",").Where(x => x != "x").Select(int.Parse);
            var closestId = 0;
            var closestTime = int.MaxValue;

            foreach (var item in items)
            {
                var upper = id + item;
                for (var i = id; i < upper; i++)
                {
                    if (i % item == 0 && i < closestTime)
                    {
                        closestId = item;
                        closestTime = i;
                    }
                }
            }

            return closestId * (closestTime - id);
        }

        private long Puzzle2(string[] input)
        {
            var items = input[1].Split(",");
            long earliest = 0;
            long runningProduct = 1;

            for (var i = 0; i < items.Length; i++)
            {
                if (items[i] == "x")
                    continue;

                var id = int.Parse(items[i]);

                while ((earliest + i) % id != 0)
                    earliest += runningProduct;

                runningProduct *= id;
            }

            return earliest;
        }
    }
}

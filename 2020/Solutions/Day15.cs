using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.Solutions.Shared;
using NUnit.Framework;

namespace AdventOfCode2020.Solutions
{
    public class Day15 : ISolution
    {
        public void Solve()
        {
            const string input = "19,20,14,0,9,1";
            Console.WriteLine(this.Puzzle1(input));
            Console.WriteLine(this.Puzzle2(input));
        }

        private int Puzzle1(string input) => MemoryGame(input, 2020);
        private int Puzzle2(string input) => MemoryGame(input, 30000000);

        private static int MemoryGame(string input, int turns)
        {
            var nums = input.Split(",").Select(num => int.Parse(num)).ToList();
            var dict = new Dictionary<int, List<int>>();

            for (var i = 0; i < nums.Count; i++)
            {
                dict.Add(nums[i], new List<int> { i + 1 });
            }

            for (var turn = nums.Count + 1; turn <= turns; turn++)
            {
                var last = nums.Last();
                var usages = dict[last];

                if (usages.Count == 1)
                {
                    nums.Add(0);
                    dict[0].Add(turn);
                }
                else
                {
                    var lastTimeUsed = usages[usages.Count - 2];
                    var newNum = turn - 1 - lastTimeUsed;
                    nums.Add(newNum);
                    if (dict.TryGetValue(newNum, out var l))
                        l.Add(turn);
                    else
                        dict.Add(newNum, new List<int> { turn });
                }
            }

            return nums.Last();
        }

        private class Tests
        {
            [Test]
            public void MemoryGame()
            {
                var actual = Day15.MemoryGame("0,3,6", 2020);
                Assert.AreEqual(436, actual);
            }
        }
    }
}

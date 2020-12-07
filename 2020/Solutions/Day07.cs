using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.Solutions.Shared;
using NUnit.Framework;

namespace AdventOfCode2020.Solutions
{
    public class Day07 : ISolution
    {
        public void Solve()
        {
            var input = Input.Lines(nameof(Day07));
            Console.WriteLine(this.Puzzle1(input));
            Console.WriteLine(this.Puzzle2(input));
        }

        private const string ShinyGold = "shiny gold";

        private int Puzzle1(string[] lines)
        {
            var map = BuildMap(lines);
            return map.Count(item => ContainsShinyGoldBagRec(map, item.Value));
        }

        private int Puzzle2(string[] lines)
        {
            var map = BuildMap(lines);
            return CountRequiredBagsRec(map, map[ShinyGold]);
        }

        private static Dictionary<string, List<(string, int)>> BuildMap(string[] lines)
        {
            var map = new Dictionary<string, List<(string, int)>>();
            foreach (var line in lines)
            {
                var split = line.TrimEnd('.').Split("contain").Select(x => x.Trim()).ToArray();
                var contains = new List<(string, int)>();
                if (split[1] != "no other bags")
                {
                    var rightSplit = split[1].Split(",").Select(x => x.Trim());
                    foreach (var bag in rightSplit)
                    {
                        var bagSplit = bag.Split(" ");
                        contains.Add(($"{bagSplit[1]} {bagSplit[2]}", int.Parse(bagSplit[0])));
                    }
                }

                var leftSplit = split[0].Split(" ");
                map.Add($"{leftSplit[0]} {leftSplit[1]}", contains);
            }
            return map;
        }

        private static bool ContainsShinyGoldBagRec(Dictionary<string, List<(string, int)>> map, List<(string, int)> current)
        {
            if (current.Any(x => x.Item1 == ShinyGold))
            {
                return true;
            }

            foreach (var item in current)
            {
                if (ContainsShinyGoldBagRec(map, map[item.Item1])) return true;
            }

            return false;
        }

        private static int CountRequiredBagsRec(Dictionary<string, List<(string, int)>> map, List<(string, int)> current)
        {
            var result = 0;

            foreach (var item in current)
            {
                result += item.Item2;
                result += item.Item2 * CountRequiredBagsRec(map, map[item.Item1]);
            }

            return result;
        }

        private class Tests
        {
            [Test]
            public void Puzzle1()
            {
                var actual = new Day07().Puzzle1(TestInput.Split("\n"));
                Assert.AreEqual(4, actual);
            }

            [TestCase(TestInput, 32)]
            [TestCase(TestInput2, 126)]
            public void Puzzle2(string input, int expected)
            {
                var actual = new Day07().Puzzle2(input.Split("\n"));
                Assert.AreEqual(expected, actual);
            }

            private const string TestInput = @"light red bags contain 1 bright white bag, 2 muted yellow bags.
dark orange bags contain 3 bright white bags, 4 muted yellow bags.
bright white bags contain 1 shiny gold bag.
muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
dark olive bags contain 3 faded blue bags, 4 dotted black bags.
vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
faded blue bags contain no other bags.
dotted black bags contain no other bags.";

            private const string TestInput2 = @"shiny gold bags contain 2 dark red bags.
dark red bags contain 2 dark orange bags.
dark orange bags contain 2 dark yellow bags.
dark yellow bags contain 2 dark green bags.
dark green bags contain 2 dark blue bags.
dark blue bags contain 2 dark violet bags.
dark violet bags contain no other bags.";
        }
    }
}
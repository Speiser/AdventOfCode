using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.Solutions.Shared;

namespace AdventOfCode2020.Solutions
{
    // First "speed" attempt
    //             Time   Rank      
    // Part 1: 00:14:23   1026
    // Part 2: 01:03:22   2027
    public class Day16 : ISolution
    {
        public void Solve()
        {
            var input = Input.Lines(nameof(Day16));
            Console.WriteLine(this.Puzzle1(input) == 26053);
            Console.WriteLine(this.Puzzle2(input) == 1515506256421);
        }

        private int Puzzle1(string[] input)
        {
            var rules = ParseRules(input.Take(20));

            var invalidSum = 0;
            for (var i = 25; i < input.Length; i++)
            {
                var row = input[i].Split(",").Select(int.Parse);
                foreach (var v in row)
                {
                    var valid = false;
                    foreach (var rule in rules.Values)
                    {
                        if (InRange(rule, v))
                        {
                            valid = true;
                            break;
                        }
                    }
                    if (!valid)
                    {
                        invalidSum += v;
                    }
                }
            }

            return invalidSum;
        }

        private static bool InRange((int, int, int, int) rule, int value)
            => value >= rule.Item1 && value <= rule.Item2 || value >= rule.Item3 && value <= rule.Item4;

        private long Puzzle2(string[] input)
        {
            var rules = ParseRules(input.Take(20));

            var nearbyValid = new List<int[]>();
            for (var i = 25; i < input.Length; i++)
            {
                var row = input[i].Split(",").Select(int.Parse).ToArray();
                var valid = true;
                foreach (var v in row)
                {
                    if (!rules.Values.Any(rule => InRange(rule, v)))
                    {
                        valid = false;
                    }
                }
                if (valid)
                {
                    nearbyValid.Add(row);
                }
            }

            var usableFor = new Dictionary<string, List<int>>();

            foreach (var rule in rules)
            {
                var ruleUseableFor = new List<int>();

                for (var i = 0; i < nearbyValid.ElementAt(0).Length; i++)
                {
                    var column = nearbyValid.Select(x => x[i]);
                    var valid = column.All(v => v >= rule.Value.Item1 && v <= rule.Value.Item2 || v >= rule.Value.Item3 && v <= rule.Value.Item4);
                    if (valid) ruleUseableFor.Add(i);
                }

                usableFor.Add(rule.Key, ruleUseableFor);
            }

            while (true)
            {
                if (usableFor.All(x => x.Value.Count == 1))
                    break;

                var countIsOne = usableFor.Where(x => x.Value.Count == 1);
                foreach (var remove in countIsOne)
                {
                    foreach (var rule in usableFor.Where(x => x.Key != remove.Key))
                    {
                        rule.Value.Remove(remove.Value.Single());
                    }
                }
            }

            var depRules = usableFor.Where(x => x.Key.StartsWith("departure"));

            long prod = 1;
            var myTicket = input[22].Split(",").Select(int.Parse).ToArray();
            foreach (var (k, v) in depRules)
            {
                prod *= myTicket[v[0]];
            }

            return prod;
        }

        private static Dictionary<string, (int, int, int, int)> ParseRules(IEnumerable<string> raw)
        {
            var rules = new Dictionary<string, (int, int, int, int)>();
            foreach (var rule in raw)
            {
                var split = rule.Split(": ");
                var rightSplit = split[1].Split(" or ");
                var l = rightSplit[0].Split("-").Select(int.Parse).ToArray();
                var r = rightSplit[1].Split("-").Select(int.Parse).ToArray();
                rules.Add(split[0], (l[0], l[1], r[0], r[1]));
            }
            return rules;
        }
    }
}

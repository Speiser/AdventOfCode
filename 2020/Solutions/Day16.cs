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
        private const int RulesLength = 20;
        private const int YourTicketIndex = 22;
        private const int NearbyTicketsIndex = 25;

        public void Solve()
        {
            var input = Input.Lines(nameof(Day16));
            Console.WriteLine(this.Puzzle1(input));
            Console.WriteLine(this.Puzzle2(input));
        }

        private int Puzzle1(string[] input)
        {
            var rules = ParseRules(input.Take(RulesLength));

            var invalidSum = 0;
            for (var i = NearbyTicketsIndex; i < input.Length; i++)
            {
                var row = ParseTicket(input[i]);
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

        private long Puzzle2(string[] input)
        {
            var rules = ParseRules(input.Take(RulesLength));

            var nearbyValid = new List<int[]>();
            for (var i = NearbyTicketsIndex; i < input.Length; i++)
            {
                var row = ParseTicket(input[i]);
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

                for (var i = 0; i < nearbyValid[0].Length; i++)
                {
                    var allColumnValuesApplyToRule = nearbyValid.Select(x => x[i]).All(v => InRange(rule.Value, v));
                    if (allColumnValuesApplyToRule)
                    {
                        ruleUseableFor.Add(i);
                    }
                }

                usableFor.Add(rule.Key, ruleUseableFor);
            }

            while (!usableFor.All(x => x.Value.Count == 1))
            {
                var countIsOne = usableFor.Where(x => x.Value.Count == 1);
                foreach (var remove in countIsOne)
                {
                    foreach (var rule in usableFor.Where(x => x.Key != remove.Key))
                    {
                        rule.Value.Remove(remove.Value.Single());
                    }
                }
            }

            long prod = 1;
            var yourTicket = ParseTicket(input[YourTicketIndex]);
            foreach (var (k, v) in usableFor.Where(x => x.Key.StartsWith("departure")))
            {
                prod *= yourTicket[v[0]];
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

        private static int[] ParseTicket(string ticket) => ticket.Split(",").Select(int.Parse).ToArray();

        private static bool InRange((int, int, int, int) rule, int value)
            => value >= rule.Item1 && value <= rule.Item2 || value >= rule.Item3 && value <= rule.Item4;
    }
}

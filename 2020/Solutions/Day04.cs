using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode2020.Solutions.Shared;
using NUnit.Framework;

namespace AdventOfCode2020.Solutions
{
    public class Day04 : ISolution
    {
        public void Solve()
        {
            var input = Input.Text(nameof(Day04)).Blocks();
            Console.WriteLine(this.Puzzle1(input));
            Console.WriteLine(this.Puzzle2(input));
        }

        private int Puzzle1(string[] input)
        {
            return GetPassports(input).Count(passport =>
            {
                var fields = passport.Split(" ");
                return HasValidFields(fields);
            });
        }

        private int Puzzle2(string[] input)
        {
            return GetPassports(input).Count(passport =>
            {
                var fields = passport.Split(" ");
                return HasValidFields(fields) && AdheresToStrictRules(fields);
            });
        }

        private static IEnumerable<string> GetPassports(string[] input) => input.Select(i => i.Replace("\r\n", " "));
        private static bool HasValidFields(string[] fields)
            => fields.Length == 8 || fields.Length == 7 && !fields.Any(field => field.StartsWith("cid"));

        private static bool AdheresToStrictRules(string[] fields)
        {
            var eyeColors = new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            foreach (var field in fields)
            {
                var kv = field.Split(":");
                switch (kv[0])
                {
                    case "byr":
                        if (!IsLegitNumber(kv[1], 1920, 2002)) return false;
                        break;
                    case "iyr":
                        if (!IsLegitNumber(kv[1], 2010, 2020)) return false;
                        break;
                    case "eyr":
                        if (!IsLegitNumber(kv[1], 2020, 2030)) return false;
                        break;
                    case "hgt":
                        if (!kv[1].EndsWith("cm") && !kv[1].EndsWith("in")) return false;
                        if (kv[1].EndsWith("cm") && !IsLegitNumber(kv[1].Replace("cm", string.Empty), 150, 193)) return false;
                        if (kv[1].EndsWith("in") && !IsLegitNumber(kv[1].Replace("in", string.Empty), 59, 76)) return false;
                        break;
                    case "hcl":
                        if (!kv[1].StartsWith("#") || kv[1].Length != 7) return false;
                        for (var i = 1; i < 7; i++)
                        {
                            var c = kv[1][i];
                            if (!char.IsDigit(c) && !char.IsLower(c)) return false;
                        }
                        break;
                    case "ecl":
                        if (!eyeColors.Contains(kv[1])) return false;
                        break;
                    case "pid":
                        if (kv[1].Length != 9) return false;
                        if (!int.TryParse(kv[1], out var _)) return false;
                        break;
                }
            }

            return true;
        }

        private static bool IsLegitNumber(string value, int incLower, int incHigher)
        {
            if (!int.TryParse(value, out var byr)) return false;
            return byr >= incLower && byr <= incHigher;
        }

        private class Tests
        {

            [Test]
            public void Puzzle1()
            {
                const string testInput = @"ecl:gry pid:860033327 eyr:2020 hcl:#fffffd
byr:1937 iyr:2017 cid:147 hgt:183cm

iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884
hcl:#cfa07d byr:1929

hcl:#ae17e1 iyr:2013
eyr:2024
ecl:brn pid:760753108 byr:1931
hgt:179cm

hcl:#cfa07d eyr:2025 pid:166559648
iyr:2011 ecl:brn hgt:59in";
                const int expected = 2;
                var actual = new Day04().Puzzle1(testInput.Blocks());
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void Puzzle2_Invalid()
            {
                const string testInput = @"eyr:1972 cid:100
hcl:#18171d ecl:amb hgt:170 pid:186cm iyr:2018 byr:1926

iyr:2019
hcl:#602927 eyr:1967 hgt:170cm
ecl:grn pid:012533040 byr:1946

hcl:dab227 iyr:2012
ecl:brn hgt:182cm pid:021572410 eyr:2020 byr:1992 cid:277

hgt:59cm ecl:zzz
eyr:2038 hcl:74454a iyr:2023
pid:3556412378 byr:2007";
                const int expected = 0;
                var actual = new Day04().Puzzle2(testInput.Blocks());
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void Puzzle2_Valid()
            {
                const string testInput = @"pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980
hcl:#623a2f

eyr:2029 ecl:blu cid:129 byr:1989
iyr:2014 pid:896056539 hcl:#a97842 hgt:165cm

hcl:#888785
hgt:164cm byr:2001 iyr:2015 cid:88
pid:545766238 ecl:hzl
eyr:2022

iyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719";
                const int expected = 4;
                var actual = new Day04().Puzzle2(testInput.Blocks());
                Assert.AreEqual(expected, actual);
            }
        }
    }
}
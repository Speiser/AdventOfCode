using System;
using System.Security.Cryptography;
using System.Text;
using AdventOfCode2015.Solutions.Shared;

namespace AdventOfCode2015.Solutions
{
    public class Day04 : ISolution
    {
        public void Solve()
        {
            Console.WriteLine(this.Puzzle1(PuzzleInput));
            Console.WriteLine(this.Puzzle2(PuzzleInput));
        }

        private const string PuzzleInput = "iwrupvqb";

        private int Puzzle1(string input) => FindHashWithStarting(input, "00000", 1000000);

        private int Puzzle2(string input) => FindHashWithStarting(input, "000000", 10000000);

        private static int FindHashWithStarting(string input, string starting, int upper)
        {
            for (var i = 0; i < upper; i++)
            {
                var hash = CalcMd5Hash($"{input}{i:000000}");
                if (hash.StartsWith(starting))
                    return i;
            }
            throw new InvalidProgramException();
        }

        private static string CalcMd5Hash(string text)
        {
            var md5 = new MD5CryptoServiceProvider();
            var textToHash = Encoding.Default.GetBytes(text);
            var result = md5.ComputeHash(textToHash);

            return BitConverter.ToString(result).Replace("-", "");
        }
    }
}

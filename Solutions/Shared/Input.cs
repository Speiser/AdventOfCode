using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2020.Solutions.Shared
{
    public static class Input
    {
        public static string[] Lines(string day) => File.ReadAllLines(InputString(day));
        public static IEnumerable<int> LinesAsInt(string day) => Lines(day).Select(int.Parse);
        public static string Text(string day) => File.ReadAllText(InputString(day));

        private static string InputString(string day) => $"Input/{day}.txt";
    }
}

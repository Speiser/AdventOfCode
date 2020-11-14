using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2019.Solutions
{
    public class Day06
    {
        public static int Puzzle1()
        {
            var container = BuildReverseMap(GetPuzzleInput());
            return container.Keys.Sum(k => GetAllParents(container, k).Count);
        }

        public static int Puzzle2()
        {
            var container = BuildReverseMap(GetPuzzleInput());
            var allSanParents = GetAllParents(container, "SAN");
            var allYouParents = GetAllParents(container, "YOU");

            foreach (var parent in allYouParents)
            {
                if (allSanParents.Contains(parent))
                {
                    var you = allYouParents.IndexOf(parent);
                    var san = allSanParents.IndexOf(parent);
                    return you + san;
                }
            }

            return -1;
        }

        private static List<string> GetAllParents(Dictionary<string, string> reverseMap, string child)
        {
            var parent = reverseMap[child];
            var result = new List<string> { parent };
            while (parent != "COM")
            {
                parent = reverseMap[parent];
                result.Add(parent);
            }
            return result;
        }

        private static Dictionary<string, string> BuildReverseMap(string[] input)
        {
            var container = new Dictionary<string, string>();
            foreach (var line in input)
            {
                var split = line.Split(')');
                container.Add(split[1], split[0]);
            }
            return container;
        }

        private static string[] GetPuzzleInput() => File.ReadAllLines("Input/Day06.txt");
    }
}

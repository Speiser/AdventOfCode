using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2019.Solutions
{
    public class Day08
    {
        private const int Width = 25;
        private const int Height = 6;

        public static int Puzzle1()
        {
            var input = GetPuzzleInput();

            // Flattens int[,] to int[]
            var layers = GetLayers(input).Select(layer => layer.Cast<int>().ToArray());

            var min = new int[0];
            var currentMinCount = int.MaxValue;
            foreach (var layer in layers)
            {
                var currentCount = layer.Count(pixel => pixel == 0);
                if (currentCount < currentMinCount)
                {
                    min = layer;
                    currentMinCount = currentCount;
                }
            }

            return min.Count(pixel => pixel == 1) * min.Count(pixel => pixel == 2);
        }

        public static string Puzzle2()
        {
            var input = GetPuzzleInput();
            var layers = GetLayers(input);

            var resultingImage = layers.First();
            foreach (var layer in layers.Skip(1))
            {
                for (var h = 0; h < Height; h++)
                {
                    for (var w = 0; w < Width; w++)
                    {
                        if (resultingImage[h, w] == 2)
                            resultingImage[h, w] = layer[h, w];
                    }
                }
            }

            var picture = new StringBuilder();
            for (var h = 0; h < Height; h++)
            {
                for (var w = 0; w < Width; w++)
                {
                    picture.Append(resultingImage[h, w] == 1 ? "." : " ");
                }
                picture.AppendLine();
            }
            return picture.ToString();
        }

        private static IEnumerable<int[,]> GetLayers(int[] input)
        {
            var layers = new List<int[,]>();
            var pixelPerLayer = Width * Height;
            for (var i = 0; i < input.Length; i += pixelPerLayer)
            {
                var layer = new int[Height, Width];
                for (var h = 0; h < Height; h++)
                {
                    for (var w = 0; w < Width; w++)
                    {
                        layer[h, w] = input[i + (h * Width) + w];
                    }
                }
                layers.Add(layer);
            }

            return layers;
        }

        private static int[] GetPuzzleInput() => File.ReadAllText("Input/Day08.txt").Select(c => c - 48).ToArray();
    }
}

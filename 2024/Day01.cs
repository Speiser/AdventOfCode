var (left, right) = ParseDay1("input.txt");

Array.Sort(left);
Array.Sort(right);

var sum = 0; // Part 1
var counts = right.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());
var score = 0; // Part 2
for (var i = 0; i < left.Length; i++)
{
    sum += Math.Abs(left[i] - right[i]);
    score += left[i] * (counts.TryGetValue(left[i], out var timesInRight) ? timesInRight : 0);
}
Console.WriteLine(sum);
Console.WriteLine(score);

static (int[], int[]) ParseDay1(string file)
{
    var content = File.ReadLines(file).ToList();
    var left = new int[content.Count];
    var right = new int[content.Count];

    for (var i = 0; i < content.Count; i++)
    {
        var split = content[i].Split("   ").Select(int.Parse).ToArray();
        left[i] = split[0];
        right[i] = split[1];
    }

    return (left, right);
}

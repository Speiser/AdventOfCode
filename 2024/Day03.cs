using System.Text.RegularExpressions;

var input = File.ReadAllText("input.txt");

var regex = new Regex(@"mul\(\d\d?\d?,\d\d?\d?\)|do\(\)|don't\(\)");

var sum1 = 0;
var sum2 = 0;
var shouldAdd = true;

foreach (var match in regex.Matches(input))
{
    var s = match.ToString()!;
    if (s == "do()")
    {
        shouldAdd = true;
        continue;
    }
    if (s == "don't()")
    {
        shouldAdd = false;
        continue;
    }
    var values = s
        .Replace("mul(", "")
        .Replace(")", "")
        .Split(",")
        .Select(int.Parse)
        .ToList();

    var product = values[0] * values[1];
    sum1 += product;
    if (shouldAdd) sum2 += product;
}

Console.WriteLine(sum1);
Console.WriteLine(sum2);

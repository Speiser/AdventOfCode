var input = File.ReadLines("input.txt")
    .Select(x => x.Split(" ").Select(int.Parse).ToList())
    .ToList();

Console.WriteLine(input.Count(IsSafe)); // Part 1
Console.WriteLine(input.Count(row =>    // Part 2
{
    if (IsSafe(row)) return true;

    for (int i = 0; i < row.Count; i++)
    {
        var modified = row.Where((_, index) => index != i).ToList();
        if (IsSafe(modified)) return true;
    }

    return false;
}));

static bool IsSafe(List<int> row)
{
    var increasing = row[0] < row[1];
    for (var i = 0; i < row.Count - 1; i++)
    {
        var left = row[i];
        var right = row[i + 1];
        if ((increasing && left >= right)
         || (!increasing && left <= right)
         || (Math.Abs(left - right) > 3))
        {
            return false;
        }
    }

    return true;
}

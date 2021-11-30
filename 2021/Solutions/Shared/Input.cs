public static class Input
{
    public static string[] Lines(string day) => File.ReadAllLines(InputString(day));
    public static IEnumerable<int> LinesAsInt(string day) => Lines(day).Select(int.Parse);
    public static string Text(string day) => File.ReadAllText(InputString(day));

    public static string[] Blocks(this string s) => s.Split("\r\n\r\n");

    private static string InputString(string day) => $"Input/{day}.txt";
}

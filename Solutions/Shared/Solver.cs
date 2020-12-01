namespace AdventOfCode2020.Solutions.Shared
{
    public class Solver
    {
        public static void Solve<T>() where T : ISolution, new() => new T().Solve();
    }
}

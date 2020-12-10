namespace AdventOfCode2015.Solutions.Shared
{
    public class Solver
    {
        public static void Solve<T>() where T : ISolution, new() => new T().Solve();
    }
}

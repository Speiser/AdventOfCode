public class Solver
{
    public static void Today()
    {
        var assembly = typeof(Solver).Assembly;
        var day = $"{assembly.GetName().Name}.Day{DateTime.Now.Day:00}";
        var solutionType = assembly.GetType(day);
        var solution = Activator.CreateInstance(solutionType) as Solution;
        solution.Solve();
    }

    public static void Solve<T>() where T : Solution, new() => new T().Solve();
}
public abstract class Solution
{
    protected string Day => this.GetType().Name;
    public abstract void Solve();
}

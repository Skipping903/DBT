namespace DBTMod.Dynamicity
{
    public interface IHasParents<T>
    {
        T[] Parents { get; }
    }
}
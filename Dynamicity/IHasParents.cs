namespace DBTR.Dynamicity
{
    public interface IHasParents<T>
    {
        T[] Parents { get; }
    }
}
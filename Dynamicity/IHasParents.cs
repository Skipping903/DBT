namespace DBT.Dynamicity
{
    public interface IHasParents<T>
    {
        T[] Parents { get; }
    }
}
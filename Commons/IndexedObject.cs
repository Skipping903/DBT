namespace DBT.Commons
{
    public struct IndexedObject<T>
    {
        public readonly int index;
        public readonly T obj;

        public IndexedObject(int index, T obj)
        {
            this.index = index;
            this.obj = obj;
        }
    }
}
namespace DBT.Commons
{
    public struct Donator
    {
        public readonly long steamId64;
        public readonly string displayName;

        public Donator(long steamId64, string displayName)
        {
            this.steamId64 = steamId64;
            this.displayName = displayName;
        }
    }
}
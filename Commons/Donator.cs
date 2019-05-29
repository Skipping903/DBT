namespace DBT.Commons
{
    public struct Donator
    {
        public readonly long steamId64;
        public readonly string displayName;
        public readonly ulong discordId;

        public Donator(long steamId64, string displayName, ulong discordId)
        {
            this.steamId64 = steamId64;
            this.displayName = displayName;
            this.discordId = discordId;
        }
    }
}
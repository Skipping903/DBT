namespace DBT.Commons.Users
{
    public class Donator : User
    {
        public Donator(long steamId64, string displayName, ulong discordId) : base(steamId64, displayName, discordId)
        {
        }
    }
}
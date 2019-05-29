using DBT.Commons.Items;
using DBT.Helpers;

namespace DBT.Extensions
{
    public static class CommonsExtensions
    {

        public static bool IsPatreonDonator(this IIsPatreonLocked patreonLocked) => SteamHelper.Donator && SteamHelper.SteamId64 == patreonLocked.Donator.steamId64;
    }
}
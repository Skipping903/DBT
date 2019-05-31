using DBT.Commons.Items;
using DBT.Helpers;

namespace DBT.Extensions
{
    public static class CommonsExtensions
    {

        public static bool IsDonator(this IIsPatreonLocked patreonLocked) => SteamHelper.IsDonator && SteamHelper.CurrentDonator == patreonLocked.Donator;
    }
}
using DBT.Players;
using Terraria.DataStructures;

namespace DBT.Commons.Players
{
    public interface IHandleOnPlayerPreHurt
    {
        bool OnPlayerPreHurt(DBTPlayer dbtPlayer, bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource);
    }
}
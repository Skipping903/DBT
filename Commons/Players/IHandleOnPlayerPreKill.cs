using DBT.Players;
using Terraria.DataStructures;

namespace DBT.Commons.Players
{
    public interface IHandleOnPlayerPreKill
    {
        bool OnPlayerPreKill(DBTPlayer dbtPlayer, ref double damage, ref int hitDirection, ref bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource);
    }
}
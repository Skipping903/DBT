using DBT.Players;

namespace DBT.Commons
{
    public interface IUpdatesOnChargeTick
    {
        bool OnPlayerPostUpdateChargingTick(DBTPlayer dbtPlayer, ref float defenseMultiplier);
    }
}
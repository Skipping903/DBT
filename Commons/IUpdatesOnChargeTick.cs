using DBT.Players;

namespace DBT.Commons
{
    public interface IUpdatesOnChargeTick
    {
        void OnPlayerChargingTick(DBTPlayer dbtPlayer);
    }
}
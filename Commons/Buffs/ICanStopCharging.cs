using DBT.Players;

namespace DBT.Commons.Buffs
{
    public interface ICanStopCharging
    {
        bool DoesStopCharging(DBTPlayer dbtPlayer);
    }
}
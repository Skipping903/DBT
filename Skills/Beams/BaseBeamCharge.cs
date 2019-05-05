using DBT.Players;

namespace DBT.Skills.Beams
{
    public abstract class BaseBeamCharge
    {
        protected BaseBeamCharge(float baseChargeKiDrain, float chargeLimit, float minimumChargeLevel, float chargeRatePerSecond)
        {
            BaseChargeKiDrain = baseChargeKiDrain;

            BaseChargeLimit = chargeLimit;
            BaseMinimumChargeLevel = minimumChargeLevel;
        }

        public virtual float GetChargeKiDrain(DBTPlayer dbtPlayer) => BaseChargeKiDrain;

        public virtual float GetChargeLimit(DBTPlayer dbtPlayer) => BaseChargeLimit;
        public virtual float GetMinimumChargeLevel(DBTPlayer dbtPlayer) => BaseMinimumChargeLevel;

        public float BaseChargeKiDrain { get; }

        public float BaseChargeLimit { get; }
        public float BaseMinimumChargeLevel { get; }
    }
}
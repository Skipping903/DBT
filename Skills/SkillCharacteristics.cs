using DBT.Players;

namespace DBT.Skills
{
    public class SkillCharacteristics
    {
        /// <summary></summary>
        /// <param name="baseChargeTimer"></param>
        /// <param name="baseMaxChargeLevel"></param>
        /// <param name="baseCastKiDrain"></param>
        /// <param name="baseChargeKiDrain">The Ki drain for a charge level. Will be applied as a drain every tick (<see cref="GetChargeTimer")/>/<see cref="GetChargeKiDrainForChargeLevel"/>)</param>
        /// <param name="baseDamage"></param>
        public SkillCharacteristics(int baseChargeTimer, int baseMaxChargeLevel, int baseCastKiDrain, int baseChargeKiDrain, int baseDamage)
        {
            BaseChargeTimer = baseChargeTimer;
            BaseMaxChargeLevel = baseMaxChargeLevel;

            BaseCastKiDrain = baseCastKiDrain;
            BaseChargeKiDrain = baseChargeKiDrain;

            BaseDamage = baseDamage;
        }

        public virtual int GetChargeTimer(DBTPlayer dbtPlayer) => BaseChargeTimer;

        public virtual int GetMaxChargeLevel(DBTPlayer dbtPlayer) => BaseMaxChargeLevel;

        /// <summary>Returns the amount of Ki drained upon using the item. Returns <see cref="DBTPlayer.GetKiDrain"/> by default.</summary>
        /// <param name="dbtPlayer"></param>
        /// <returns></returns>
        public virtual float GetCastKiDrain(DBTPlayer dbtPlayer) => dbtPlayer.GetKiDrain(BaseCastKiDrain);

        public virtual float GetChargeKiDrain(DBTPlayer dbtPlayer) => GetChargeTimer(dbtPlayer) / GetChargeKiDrainForChargeLevel(dbtPlayer);

        public virtual float GetChargeKiDrainForChargeLevel(DBTPlayer dbtPlayer) => BaseChargeKiDrain;

        public int BaseChargeTimer { get; }
        public int BaseMaxChargeLevel { get; }

        public float BaseCastKiDrain { get; }
        public float BaseChargeKiDrain { get; }

        public float BaseDamage { get; }
    }
}
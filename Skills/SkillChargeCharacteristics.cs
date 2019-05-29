using DBT.Players;

namespace DBT.Skills
{
    public class SkillChargeCharacteristics
    {
        /// <summary></summary>
        /// <param name="baseChargeTimer">How long you must channel a skill to get one charge on it.</param>
        /// <param name="baseMaxChargeLevel">The maximum charge level for the skill.</param>
        /// <param name="baseCastKiDrain">The drain per tick while charging.</param>
        /// <param name="baseChargeTickKiDrain">The Ki drain for a charge level. Will be applied as a drain every tick (<see cref="GetChargeTimer"/>/<see cref="GetChargeKiDrainForChargeLevel"/>)</param>
        public SkillChargeCharacteristics(int baseChargeTimer, int baseMaxChargeLevel, int baseCastKiDrain, int baseChargeTickKiDrain)
        {
            BaseChargeTimer = baseChargeTimer;
            BaseMaxChargeLevel = baseMaxChargeLevel;

            BaseCastKiDrain = baseCastKiDrain;
            BaseChargeTickKiDrain = baseChargeTickKiDrain;
        }


        public virtual int GetChargeTimer(DBTPlayer dbtPlayer) => BaseChargeTimer;

        public virtual int GetMaxChargeLevel(DBTPlayer dbtPlayer) => BaseMaxChargeLevel;

        /// <summary>Returns the amount of Ki drained upon using the item. Returns <see cref="DBTPlayer.GetKiDrain"/> by default.</summary>
        /// <param name="dbtPlayer"></param>
        /// <returns></returns>
        public virtual float GetCastKiDrain(DBTPlayer dbtPlayer) => dbtPlayer.GetKiDrain(BaseCastKiDrain);

        public virtual float GetChargeTickKiDrain(DBTPlayer dbtPlayer) => GetChargeTimer(dbtPlayer) / GetChargeKiDrainForChargeLevel(dbtPlayer);

        public virtual float GetChargeKiDrainForChargeLevel(DBTPlayer dbtPlayer) => BaseChargeTickKiDrain;


        public int BaseChargeTimer { get; }
        public int BaseMaxChargeLevel { get; }

        public float BaseCastKiDrain { get; }
        public float BaseChargeTickKiDrain { get; }
    }
}
using System;
using DBT.Players;

namespace DBT.Skills
{
    public class SkillCharacteristics
    {
        /// <summary></summary>
        /// /// <param name="skillChargeCharacteristics"></param>
        /// <param name="baseDamageMultiplierPerCharge">The number by which to multiply the damage.</param>
        /// <param name="baseDamage">The base damage done by the skill per damage tick.</param>
        public SkillCharacteristics(SkillChargeCharacteristics skillChargeCharacteristics, float baseDamageMultiplierPerCharge, int baseDamage, float baseShootSpeed)
        {
            ChargeCharacteristics = skillChargeCharacteristics;

            BaseBaseDamageMultiplierPerCharge = baseDamageMultiplierPerCharge;
            BaseDamage = baseDamage;

            BaseShootSpeed = baseShootSpeed;
        }


        public virtual float GetDamageMultiplierPerCharge(DBTPlayer dbtPlayer) => BaseBaseDamageMultiplierPerCharge;

        public virtual float GetDamage(DBTPlayer dbtPlayer, int chargeLevel) => BaseDamage * GetDamageMultiplierPerCharge(dbtPlayer) * chargeLevel;

        public virtual float GetShootSpeed(DBTPlayer dbtPlayer) => BaseShootSpeed;

        public SkillChargeCharacteristics ChargeCharacteristics { get; }

        public float BaseBaseDamageMultiplierPerCharge { get; }
        public float BaseDamage { get; }

        public float BaseShootSpeed { get; }
    }
}
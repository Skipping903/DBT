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
        /// <param name="baseShootSpeed"></param>
        public SkillCharacteristics(SkillChargeCharacteristics skillChargeCharacteristics, float baseDamageMultiplierPerCharge, int baseDamage, float baseShootSpeed, float baseKnockbackMultiplierPerCharge, float baseKnockback)
        {
            ChargeCharacteristics = skillChargeCharacteristics;

            BaseBaseDamageMultiplierPerCharge = baseDamageMultiplierPerCharge;
            BaseDamage = baseDamage;

            BaseShootSpeed = baseShootSpeed;

            BaseKnockbackMultiplierPerCharge = baseKnockbackMultiplierPerCharge;
            BaseKnockback = baseKnockback;
        }


        public virtual float GetDamageMultiplierPerCharge(DBTPlayer dbtPlayer) => BaseBaseDamageMultiplierPerCharge;

        public virtual float GetDamage(DBTPlayer dbtPlayer, int chargeLevel)
        {
            int damage = (int)BaseDamage;
            GetDamage(dbtPlayer, ref damage, chargeLevel);

            return damage;
        }

        public virtual void GetDamage(DBTPlayer dbtPlayer, ref int damage, int chargeLevel)
        {
            if (chargeLevel > 0)
                damage = (int) (damage * GetDamageMultiplierPerCharge(dbtPlayer) * chargeLevel);
        }


        public virtual float GetShootSpeed(DBTPlayer dbtPlayer, int chargeLevel) => BaseShootSpeed;


        public virtual float GetKnockbackMultiplierPerCharge(DBTPlayer dbtPlayer) => BaseKnockbackMultiplierPerCharge;

        public virtual float GetKnockback(DBTPlayer dbtPlayer, int chargeLevel)
        {
            float knockback = BaseKnockback;
            GetKnockback(dbtPlayer, ref knockback, chargeLevel);

            return knockback;
        }

        public virtual void GetKnockback(DBTPlayer dbtPlayer, ref float knockback, int chargeLevel)
        {
            if (chargeLevel > 0)
                knockback *= GetKnockbackMultiplierPerCharge(dbtPlayer) * chargeLevel;
        }


        public SkillChargeCharacteristics ChargeCharacteristics { get; }

        public float BaseBaseDamageMultiplierPerCharge { get; }
        public int BaseDamage { get; }

        public float BaseShootSpeed { get; }

        public float BaseKnockbackMultiplierPerCharge { get; }
        public float BaseKnockback { get; }
    }
}
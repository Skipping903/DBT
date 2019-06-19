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
        public SkillCharacteristics(SkillChargeCharacteristics skillChargeCharacteristics, int baseDamage, float baseDamageMultiplierPerCharge, float baseShootSpeed, float baseKnockback, float baseKnockbackMultiplierPerCharge)
        {
            ChargeCharacteristics = skillChargeCharacteristics;

            BaseDamage = baseDamage;
            BaseBaseDamageMultiplierPerCharge = baseDamageMultiplierPerCharge;

            BaseShootSpeed = baseShootSpeed;

            BaseKnockback = baseKnockback;
            BaseKnockbackMultiplierPerCharge = baseKnockbackMultiplierPerCharge;
        }


        public virtual float GetDamage(DBTPlayer dbtPlayer, int chargeLevel)
        {
            int damage = (int)BaseDamage;
            GetDamage(dbtPlayer, ref damage, chargeLevel);

            return damage;
        }

        public virtual void GetDamage(DBTPlayer dbtPlayer, ref int damage, int chargeLevel)
        {
            damage = (int)(damage * GetDamageMultiplierPerCharge(dbtPlayer) * (chargeLevel + 1));
        }

        public virtual float GetDamageMultiplierPerCharge(DBTPlayer dbtPlayer) => BaseBaseDamageMultiplierPerCharge;


        public virtual float GetShootSpeed(DBTPlayer dbtPlayer, int chargeLevel) => BaseShootSpeed;


        public virtual float GetKnockback(DBTPlayer dbtPlayer, int chargeLevel)
        {
            float knockback = BaseKnockback;
            GetKnockback(dbtPlayer, ref knockback, chargeLevel);

            return knockback;
        }

        public virtual float GetKnockbackMultiplierPerCharge(DBTPlayer dbtPlayer) => BaseKnockbackMultiplierPerCharge;


        public virtual void GetKnockback(DBTPlayer dbtPlayer, ref float knockback, int chargeLevel)
        {
            knockback *= GetKnockbackMultiplierPerCharge(dbtPlayer) * (chargeLevel + 1);
        }


        public SkillChargeCharacteristics ChargeCharacteristics { get; }

        public int BaseDamage { get; }
        public float BaseBaseDamageMultiplierPerCharge { get; }

        public float BaseShootSpeed { get; }

        public float BaseKnockback { get; }
        public float BaseKnockbackMultiplierPerCharge { get; }
    }
}
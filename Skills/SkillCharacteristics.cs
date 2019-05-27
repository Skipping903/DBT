namespace DBT.Skills
{
    public struct SkillCharacteristics
    {
        public SkillCharacteristics(int chargeTimer, int maxChargeLevel, int damage)
        {
            ChargeTimer = chargeTimer;
            MaxChargeLevel = maxChargeLevel;

            Damage = damage;
        }

        public int ChargeTimer { get; }
        public int MaxChargeLevel { get; }

        public int Damage { get; }
    }
}
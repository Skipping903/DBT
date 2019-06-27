namespace DBT.Skills.Kamehameha
{
    public sealed class KamehamehaDefinition : SkillDefinition
    {
        public KamehamehaDefinition() : base("Kamehameha", "Kamehameha", "Charges up to 6 times\n" + DEFAULT_BEAM_INSTRUCTIONS, new DefinitionCharacteristics())
        {
        }

        public sealed class DefinitionCharacteristics : SkillCharacteristics
        {
            public DefinitionCharacteristics() : base(new DefinitionChargeCharacteristics(), 88, 1f, 0f, 2f, 1f,
                0.15f, 1, 2, 1)
            {
                Channel = true;
                BaseSkillCooldown = 5 * Constants.TICKS_PER_SECOND;
            }

            public sealed class DefinitionChargeCharacteristics : SkillChargeCharacteristics
            {
                public DefinitionChargeCharacteristics() : base(60, 6, 80, 80 / Constants.TICKS_PER_SECOND)
                {
                }
            }
        }
    }
}
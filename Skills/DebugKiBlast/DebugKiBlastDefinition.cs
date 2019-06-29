namespace DBT.Skills.DebugKiBlast
{
    public sealed class DebugKiBlastDefinition : SkillDefinition
    {
        public DebugKiBlastDefinition() : base("DebugKiBlast", "Debug Ki Blast", "A small debugging Ki blast that damages enemies when it really shouldn't.", new DebugKiBlastCharacteristics())
        {
        }

        public sealed class DebugKiBlastCharacteristics : SkillCharacteristics
        {
            public DebugKiBlastCharacteristics() : base(new DebugKiBlastChargeCharacteristics(), 100, 1, 15f, 5f, 1, 
                0.5f, 1, 2, 1)
            {
            }

            public sealed class DebugKiBlastChargeCharacteristics : SkillChargeCharacteristics
            {
                public DebugKiBlastChargeCharacteristics() : base(60, 0, 90, 0)
                {
                }
            }
        }
    }
}
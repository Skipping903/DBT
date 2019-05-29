namespace DBT.Skills.KiBlast
{
    public sealed class DebugKiBlastDefinition : SkillDefinition
    {
        public DebugKiBlastDefinition() : base("KiBlast", "Ki Blast", "A small Ki blast that damages enemies.", new DebugKiBlastCharacteristics())
        {
        }
    }

    public sealed class DebugKiBlastCharacteristics : SkillCharacteristics
    {
        public DebugKiBlastCharacteristics() : base(new DebugKiBlastChargeCharacteristics(), 0, 100, 15f)
        {
        }
    }

    public sealed class DebugKiBlastChargeCharacteristics : SkillChargeCharacteristics
    {
        public DebugKiBlastChargeCharacteristics() : base(60, 0, 90, 0)
        {
        }
    }
}
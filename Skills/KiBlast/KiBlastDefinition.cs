namespace DBT.Skills.KiBlast
{
    public sealed class KiBlastDefinition : SkillDefinition
    {
        public KiBlastDefinition() : base("KiBlast", "KiBlast", "A small Ki blast that damages enemies.", new KiBlastCharacteristics())
        {
        }
    }

    public sealed class KiBlastCharacteristics : SkillCharacteristics
    {
        public KiBlastCharacteristics() : base(new KiBlastChargeCharacteristics(), 19, 1f, 15f, 5f, 1f)
        {
        }
    }

    public sealed class KiBlastChargeCharacteristics : SkillChargeCharacteristics
    {
        public KiBlastChargeCharacteristics() : base(0, 0, 15, 0)
        {
        }
    }
}
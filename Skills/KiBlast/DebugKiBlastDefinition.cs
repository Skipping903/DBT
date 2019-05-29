namespace DBT.Skills.KiBlast
{
    public sealed class DebugKiBlastDefinition : SkillDefinition
    {
        public DebugKiBlastDefinition() : base("KiBlast", "Ki Blast", new DebugKiBlastCharacteristics())
        {
        }
    }

    public sealed class DebugKiBlastCharacteristics : SkillCharacteristics
    {
        public DebugKiBlastCharacteristics() : base(60, 1, 90, 0, 100)
        {
        }
    }
}
namespace DBT.Skills.DestructoDisk
{
    public sealed class DestructoDiskDefinition : SkillDefinition
    {
        public DestructoDiskDefinition() : base("DestructoDisk", "Destructo Disk", "'Its a frizbee, I swear.'", new DestructoDiskCharacteristics())
        {
        }
    }

    public sealed class DestructoDiskCharacteristics : SkillCharacteristics
    {
        public DestructoDiskCharacteristics() : base(new DestructoDiskChargeCharacteristics(), 42, 1f, 20f, 3f, 1f, 0.05f, 1, 2, 1)
        {
        }
    }

    public sealed class DestructoDiskChargeCharacteristics : SkillChargeCharacteristics
    {
        public DestructoDiskChargeCharacteristics() : base(0, 0, 40, 0)
        {
        }
    }
}
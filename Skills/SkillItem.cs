using DBT.Items;

namespace DBT.Skills
{
    public abstract class SkillItem : DBTItem
    {
        protected SkillDefinition(SkillDefinition definition)
        {
            Definition = definition;
        }

        public SkillDefinition Definition { get; }
    }
}
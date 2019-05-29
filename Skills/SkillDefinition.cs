using DBT.Commons;
using DBT.Dynamicity;

namespace DBT.Skills
{
    public class SkillDefinition : IHasUnlocalizedName, IHasParents<SkillDefinition>
    {
        protected SkillDefinition(string unlocalizedName, string displayName, SkillCharacteristics characteristics, params SkillDefinition[] parents)
        {
            UnlocalizedName = unlocalizedName;
            DisplayName = displayName;

            Characteristics = characteristics;

            Parents = parents;
        }

        public string UnlocalizedName { get; }
        public string DisplayName { get; }

        public SkillCharacteristics Characteristics { get; }

        public SkillDefinition[] Parents { get; }
    }
}
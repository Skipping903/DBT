using DBT.Commons;
using DBT.Dynamicity;

namespace DBT.Skills
{
    public class SkillDefinition : IHasUnlocalizedName, IHasParents<SkillDefinition>
    {
        protected SkillDefinition(string unlocalizedName, SkillCharacteristics characteristics, params SkillDefinition[] parents)
        {
            UnlocalizedName = unlocalizedName;

            Characteristics = characteristics;

            Parents = parents;
        }

        public string UnlocalizedName { get; }

        public SkillCharacteristics Characteristics { get; }

        public SkillDefinition[] Parents { get; }
    }
}
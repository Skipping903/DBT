using System.Collections.Generic;
using DBT.Items;
using Terraria.ModLoader;

namespace DBT.Skills
{
    public abstract class SkillItem : DBTItem
    {
        protected SkillItem(SkillDefinition definition, int width, int height) : base(definition.DisplayName, "", width, height)
        {
            Definition = definition;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(tooltips);
        }

        public override void PostModifyTooltips(List<TooltipLine> tooltips)
        {
            base.ModifyTooltips(tooltips);
        }

        public SkillDefinition Definition { get; }
    }
}
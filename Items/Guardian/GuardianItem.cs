using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Guardian
{
    public abstract class GuardianItem : DBTItem
    {
        protected GuardianItem(string displayName, string tooltip, int width, int height, int value = 0, int defense = 0, int rarity = ItemRarityID.White) : base(displayName, tooltip, width, height)
        {
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine indicate = new TooltipLine(mod, "", "- Guardian -") { overrideColor = new Color(69, 255, 56) };
            tooltips.Add(indicate);

            base.ModifyTooltips(tooltips);
        }

        public override bool CloneNewInstances => true;
    }
}

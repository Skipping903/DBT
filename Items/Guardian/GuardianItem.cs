using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace DBT.Items.Guardian
{
    public abstract class GuardianItem : DBTItem
    {
        protected GuardianItem(string displayName, string tooltip) : base(displayName, tooltip)
        {
        }

        public override bool CloneNewInstances => true;

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine indicate = new TooltipLine(mod, "", "");
            string[] text2 = indicate.text.Split(' ');
            indicate.text = "-- Guardian --";
            indicate.overrideColor = new Color(69, 255, 56);
            tooltips.Add(indicate);

            base.ModifyTooltips(tooltips);
        }
    }
}

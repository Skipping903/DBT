using System.Collections.Generic;
using Terraria.ModLoader;

namespace DBT.Commons.Items
{
    public interface IHasBottomTooltipLines
    {
        void AddBottomTooltipLines(List<TooltipLine> tooltipLines);
    }
}
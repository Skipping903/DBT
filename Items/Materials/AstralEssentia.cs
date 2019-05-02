using Terraria;
using Terraria.ID;

namespace DBT.Items.Materials
{
    public sealed class AstralEssentia : DBTMaterial
    {
        public AstralEssentia() : base("Astral Essentia", "'The sky's astral energy emanates from within.'",
            36, 22, Item.buyPrice(copper: 3), ItemRarityID.Orange)
        {
        }
    }
}
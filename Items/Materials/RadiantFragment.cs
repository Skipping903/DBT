using Terraria;
using Terraria.ID;

namespace DBT.Items.Materials
{
    public sealed class RadiantFragment : DBTMaterial
    {
        public RadiantFragment() : base("Radiant Fragment", "'The endurance of the cosmos crackles around this fragment.'",
            20, 20, Item.buyPrice(silver: 20), ItemRarityID.Cyan)
        {
        }
    }
}
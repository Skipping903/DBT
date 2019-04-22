using Terraria;
using Terraria.ID;

namespace DBT.Items.Materials
{
    public sealed class DemonicSoul : DBTMaterial
    {
        public DemonicSoul() : base("Demonic Soul", "'A fragment of the devil's rage.'",
            26, 26, Item.buyPrice(silver: 1, copper: 60), ItemRarityID.Lime)
        {
        }
    }
}
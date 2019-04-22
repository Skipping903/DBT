using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories.Necklaces.GemNecklaces
{
    public sealed class EmeraldGemNecklace : GemNecklace
    {
        public EmeraldGemNecklace() : base("Emerald Necklace", "9% increased ranged damage and crit chance.", Item.buyPrice(silver: 85), 0, ItemID.Emerald)
        {
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.rangedDamage += 0.05f;
            player.rangedCrit += 5;
        }
    }
}

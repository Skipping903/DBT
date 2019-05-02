using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories.Necklaces.GemNecklaces
{
    public sealed class AmethystGemNecklace : GemNecklace
    {
        public AmethystGemNecklace() : base("Amethyst Necklace", "5% increased damage reduction and +4 defense.", Item.buyPrice(silver: 31), 4, ItemID.Amethyst)
        {
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.endurance += 0.05f;
        }
    }
}
using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories.Necklaces.GemNecklaces
{
    public sealed class TopazGemNecklace : GemNecklace
    {
        public TopazGemNecklace() : base("Topaz Necklace", "5% increased minion damage and +1 max minions.", Item.buyPrice(silver: 50), 0, ItemID.Topaz)
        {
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.minionDamage += 0.05f;
            player.maxMinions += 1;
        }
    }
}
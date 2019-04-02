using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories.Necklaces.GemNecklaces
{
    public sealed class TopazGemNecklace : GemNecklace
    {
        public TopazGemNecklace() : base("Topaz Necklace", "9% increased minion damage and +1 max minions.", 50 * Constants.SILVER_VALUE_MULTIPLIER, 0, ItemID.Topaz)
        {
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.minionDamage += 0.09f;
            player.maxMinions += 1;
        }
    }
}
using Terraria;
using Terraria.ID;

namespace DBTMod.Items.Accessories.Necklaces.GemNecklaces
{
    public sealed class EmeraldGemNecklace : GemNecklace
    {
        public EmeraldGemNecklace() : base("Emerald Necklace", "9% increased ranged damage and crit chance.", 85 * Constants.SILVER_VALUE_MULTIPLIER, 0, ItemID.Emerald)
        {
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.rangedDamage += 0.09f;
            player.rangedCrit += 9;
        }
    }
}

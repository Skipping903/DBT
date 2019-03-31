using Terraria;
using Terraria.ID;

namespace DBTMod.Items.Accessories.Necklaces.GemNecklaces
{
    public sealed class AmethystGemNecklace : GemNecklace
    {
        public AmethystGemNecklace() : base("Amethyst Necklace", "9% increased endurance and +4 defense.", 31 * Constants.SILVER_VALUE_MULTIPLIER, 4, ItemID.Amethyst)
        {
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.endurance += 0.09f;
        }
    }
}
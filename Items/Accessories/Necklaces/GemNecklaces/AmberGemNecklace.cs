using Terraria;
using Terraria.ID;

namespace DBTMod.Items.Accessories.Necklaces.GemNecklaces
{
    public sealed class AmberGemNecklace : GemNecklace
    {
        public AmberGemNecklace() : base("Amber Necklace", "+1 life regeneration.", 160 * Constants.SILVER_VALUE_MULTIPLIER, 1, ItemID.Amber)
        {
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lifeRegen += 1;
        }
    }
}

using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories.Necklaces.GemNecklaces
{
    public sealed class DiamondGemNecklace : GemNecklace
    {
        public DiamondGemNecklace() : base("Diamond Necklace", "5% increased melee damage and speed.", 160 * Constants.SILVER_VALUE_MULTIPLIER, 0, ItemID.Diamond)
        {
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.meleeDamage += 0.05f;
            player.meleeSpeed += 0.05f;
        }
    }
}

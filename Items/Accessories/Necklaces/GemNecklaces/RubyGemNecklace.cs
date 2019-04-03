using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories.Necklaces.GemNecklaces
{
    public sealed class RubyGemNecklace : GemNecklace
    {
        public RubyGemNecklace() : base("Ruby Necklace", "9% increased magic damage and crit chance.", 120 * Constants.SILVER_VALUE_MULTIPLIER, 0, ItemID.Ruby)
        {
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.magicDamage += 0.09f;
            player.magicCrit += 9;
        }
    }
}
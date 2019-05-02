using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories.Necklaces.GemNecklaces
{
    public sealed class RubyGemNecklace : GemNecklace
    {
        public RubyGemNecklace() : base("Ruby Necklace", "5% increased magic damage and crit chance.", Item.buyPrice(gold: 1, silver: 20), 0, ItemID.Ruby)
        {
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.magicDamage += 0.05f;
            player.magicCrit += 5;
        }
    }
}
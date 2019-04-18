using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories.Necklaces.GemNecklaces
{
    public sealed class AmberGemNecklace : GemNecklace
    {
        public AmberGemNecklace() : base("Amber Necklace", "+1 life regeneration.", Item.buyPrice(gold:1, silver: 60), 1, ItemID.Amber)
        {
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lifeRegen += 1;
        }
    }
}

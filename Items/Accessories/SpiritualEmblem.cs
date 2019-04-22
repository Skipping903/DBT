using DBT.Players;
using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories
{
    public sealed class SpiritualEmblem : DBTAccessory
    {
        public SpiritualEmblem() : base("Spiritual Emblem",
            "'The emblem seems to have weird writing inscribed on it.'" +
            "\n15% Increased Ki Damage.",
            24, 28, value: Item.buyPrice(gold: 1, silver: 60), rarity: ItemRarityID.Orange)
        {
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            player.GetModPlayer<DBTPlayer>().KiDamageMultiplier += 1.15f;
        }
    }
}
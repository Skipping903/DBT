using DBT.Players;
using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories.Radiant
{
    public sealed class RadiantTotem : DBTAccessory
    {
        public RadiantTotem() : base("Radiant Totem",
            "'It explodes with radiant energy.'" +
            "\n12% Increased Ki damage" +
            "\n+500 Max Ki" +
            "\nDrastically increased ki regen",
            18, 30, value: Item.buyPrice(gold: 2, silver: 40), rarity: ItemRarityID.Cyan)
        {
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 0.12f;
            dbtPlayer.MaxKiModifier += 500;
            dbtPlayer.ExtraKiRegeneration = 2;
        }
    }
}
using DBT.Players;
using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories
{
    public sealed class KaioCrystal : DBTAccessory
    {
        public KaioCrystal() : base("Kaio Crystal",
            "'A spiritual fragment of one of the legendary Kais'" +
            "\nAll Kaioken forms drain half as much health" +
            "\n30% less max ki",
            18, 30, value: Item.buyPrice(gold: 1, silver: 80), rarity: ItemRarityID.LightPurple)
        {
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.MaxKiMultiplier -= 0.3f;
            dbtPlayer.HealthDrainMultiplier -= 0.5f;
        }
    }
}
using DBT.Players;
using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories
{
    public sealed class LuminousSectum : DBTAccessory
    {
        public LuminousSectum() : base("Luminous Sectum",
            "'It radiates with unstable energy.'" +
            "\n9% increased ki damage" +
            "\n+250 max ki" +
            "\nHitting enemies has a small chance to fire off homing ki sparks",
            18, 30, value: Item.buyPrice(gold: 6, silver: 40), rarity: ItemRarityID.Pink)
        {
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 0.09f;
            dbtPlayer.MaxKiModifier += 250;
        }
    }
}
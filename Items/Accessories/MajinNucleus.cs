using DBT.Players;
using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories
{
    public sealed class MajinNucleus : DBTAccessory
    {
        public MajinNucleus() : base("Majin Nucleus",
            "'The pulsing nucleus of a invicible being.'" +
            "\nMassivly increased health regen" +
            "\nMassivly increased ki regen" +
            "\n-1500 max ki",
            18, 30, value: Item.buyPrice(gold: 4, silver: 80), rarity: ItemRarityID.LightPurple)
        {
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            player.lifeRegen += 12;
            dbtPlayer.NaturalKiRegeneration += 6;
            dbtPlayer.MaxKiModifier -= 1500;
        }
    }
}
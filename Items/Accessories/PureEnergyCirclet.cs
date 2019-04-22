using DBT.Players;
using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories
{
    public sealed class PureEnergyCirclet : DBTAccessory
    {
        public PureEnergyCirclet() : base("Pure Energy Circlet",
            "'It radiates a unbelievably pure presence.'" +
            "\n12% Increased Ki damage" +
            "\nIncreased Ki regen" +
            "\n+300 Max Ki" +
            "\nCharging grants a aura of inferno and frostburn around you.",
            18, 30, value: Item.buyPrice(gold: 2, silver: 40), rarity: ItemRarityID.LightPurple)
        {
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 0.12f;
            dbtPlayer.ExtraKiRegeneration += 2;
            dbtPlayer.MaxKiModifier += 300;

            // TODO Add aura.
        }
    }
}
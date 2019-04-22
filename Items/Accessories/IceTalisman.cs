using DBT.Players;
using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories
{
    public sealed class IceTalisman : DBTAccessory
    {
        // TODO Add item functionality.
        public IceTalisman() : base("Ice Energy Talisman",
            "'A frozen talisman that seems to make even your soul cold.'" +
            "\n7% Increased Ki damage" +
            "\nIncreased Ki regen" +
            "\nCharging grants a aura of frostburn around you",
            18, 30, value: Item.buyPrice(silver: 90), rarity: ItemRarityID.LightRed)
        {
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 0.07f;
            dbtPlayer.ExtraKiRegeneration += 2;
            
            // TODO Add frost aura.
        }
    }
}
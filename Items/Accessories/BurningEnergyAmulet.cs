using DBT.Players;
using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories
{
    public sealed class BurningEnergyAmulet : DBTItem
    {
        public BurningEnergyAmulet() : base("Burning Energy Amulet",
            "'A glowing amulet that radiates with extreme heat'" +
            "\n5% Increased Ki damage" +
            "\n+200 Max ki" +
            "\nCharging grants a aura of fire around you",
            18, 30, value: Item.buyPrice(silver: 80), rarity: ItemRarityID.LightRed)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 0.05f;
            dbtPlayer.MaxKiModifier += 200;
        }
    }
}
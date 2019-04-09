using DBT.Players;
using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories
{
    public sealed class ArmCannon : DBTItem
    {
        public ArmCannon() : base("Arm Cannon", "An old arm blaster used by many soldiers.\n10% Reduced Ki usage\nIncreased charge speed")
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.width = 18;
            item.height = 30;
            item.value = 3 * Constants.SILVER_VALUE_MULTIPLIER;
            item.rare = ItemRarityID.LightRed;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);

            player.GetModPlayer<DBTPlayer>().KiChargeRate += 1;
        }
    }
}
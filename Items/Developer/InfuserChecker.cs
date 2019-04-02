using DBT.Extensions;
using DBT.HairStyles;
using DBT.Items.Accessories.Infusers;
using DBT.Players;
using Terraria;

namespace DBT.Items.Developer
{
    public sealed class InfuserChecker : DeveloperItem
    {
        public InfuserChecker() : base(18, 34)
        {
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Infuser Checker");
            Tooltip.SetDefault("Checks if the Infusers are part of the player's accessory.");
        }

        public override bool UseItem(Player player)
        {
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            Main.NewText(dbtPlayer.GetItemsInArmor<Infuser>().Count);

            return true;
        }
    }
}
using DBTMod.Players;
using Terraria;

namespace DBTMod.Items.Developer
{
    public class TransformationClearer : DeveloperItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Transformation Clearer");
            Tooltip.SetDefault("Clears the player's transformation to the code.");
        }

        public override bool UseItem(Player player)
        {
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();
            
            dbtPlayer.ClearTransformations();
            dbtPlayer.AcquiredTransformations.Clear();

            return true;
        }
    }
}

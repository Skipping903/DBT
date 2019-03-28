using DBTR.Players;
using DBTR.Transformations;
using Terraria;

namespace DBTR.Items.Developer
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
            DBTRPlayer dbtrPlayer = player.GetModPlayer<DBTRPlayer>();
            
            dbtrPlayer.ClearTransformations();
            dbtrPlayer.AcquiredTransformations.Clear();

            return true;
        }
    }
}

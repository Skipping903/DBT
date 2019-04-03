using DBT.Players;
using Terraria;

namespace DBT.Items.Developer
{
    public class TransformationClearer : DeveloperItem
    {
        public TransformationClearer() : base(40, 40)
        {
        }

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

using DBTR.Players;
using DBTR.Transformations;
using Terraria;

namespace DBTR.Items.Developer
{
    public sealed class TransformationSetter : DeveloperItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Transformation Setter");
            Tooltip.SetDefault("Sets the player's transformation to the code.");
        }

        public override bool UseItem(Player player)
        {
            DBTRPlayer dbtrPlayer = player.GetModPlayer<DBTRPlayer>();

            if (dbtrPlayer.FirstTransformation == null)
            {
                dbtrPlayer.AcquireAndTransform(TransformationDefinitionManager.Instance.SSJ1);
                return true;
            }

            TransformationDefinition currentTransformation = dbtrPlayer.FirstTransformation.Definition;

            dbtrPlayer.ClearTransformations();
            int nextIndex = TransformationDefinitionManager.Instance.GetIndex(currentTransformation) + 1;

            if (nextIndex >= TransformationDefinitionManager.Instance.Count)
                nextIndex = 0;

            dbtrPlayer.AcquireAndTransform(TransformationDefinitionManager.Instance[nextIndex]);

            return true;
        }
    }
}

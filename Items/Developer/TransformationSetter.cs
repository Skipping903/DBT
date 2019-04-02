using DBT.Players;
using DBT.Transformations;
using Terraria;

namespace DBT.Items.Developer
{
    public sealed class TransformationSetter : DeveloperItem
    {
        public TransformationSetter() : base(40, 40)
        {
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Transformation Setter");
            Tooltip.SetDefault("Sets the player's transformation to the code.");
        }

        public override bool UseItem(Player player)
        {
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            if (dbtPlayer.FirstTransformation == null)
            {
                dbtPlayer.AcquireAndTransform(TransformationDefinitionManager.Instance.SSJ1);
                return true;
            }

            TransformationDefinition currentTransformation = dbtPlayer.FirstTransformation.Definition;

            dbtPlayer.ClearTransformations();
            int nextIndex = TransformationDefinitionManager.Instance.GetIndex(currentTransformation) + 1;

            if (nextIndex >= TransformationDefinitionManager.Instance.Count)
                nextIndex = 0;

            dbtPlayer.AcquireAndTransform(TransformationDefinitionManager.Instance[nextIndex]);

            return true;
        }
    }
}

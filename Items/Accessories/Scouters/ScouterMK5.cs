using Terraria.ID;

namespace DBT.Items.Accessories.Scouters
{
    public class ScouterMK5 : Scouter
    {
        public ScouterMK5() : base("Yellow Scouter", "A Piece of equipment used for scanning power levels.\nGives Increased Ki Damage and Hunter effect", 480 * Constants.SILVER_VALUE_MULTIPLIER, ItemRarityID.Lime, 0.20f)
        {
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            // TODO Rework recipe.
            /*ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(PureKiCrystal), 20);
            recipe.AddIngredient(mod, nameof(ScouterMK4));
            recipe.AddTile(mod, nameof(ZTableTile));

            recipe.SetResult(this);
            recipe.AddRecipe();*/
        }
    }
}
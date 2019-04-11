using Terraria.ID;

namespace DBT.Items.Accessories.Scouters
{
    public sealed class ScouterMK3 : Scouter
    {
        public ScouterMK3() : base("Red Scouter", "A Piece of equipment used for scanning power levels\nGives Increased Ki Damage and Hunter effect", 240 * Constants.SILVER_VALUE_MULTIPLIER, ItemRarityID.Pink, 0.12f)
        {
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            // TODO Rework recipe.
            /*ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(AngerfulKiCrystal), 20);
            recipe.AddIngredient(mod, nameof(ScouterMK2));
            recipe.AddTile(mod, nameof(ZTableTile));

            recipe.SetResult(this);
            recipe.AddRecipe();*/
        }
    }
}
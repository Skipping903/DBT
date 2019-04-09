using DBT.Items.Materials.Metals;
using DBT.Tiles;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Accessories.Scouters
{
    public sealed class ScouterMK1 : Scouter
    {
        public ScouterMK1() : base("Green Scouter", "A Piece of equipment used for scanning power levels.\nIncreased Ki Damage and Hunter effect.\n-Tier 1-", 80 * Constants.SILVER_VALUE_MULTIPLIER, ItemRarityID.Orange, 0.05f)
        {
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            /*ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(CalmKiCrystal), 20);
            recipe.AddIngredient(mod, nameof(ScrapMetal), 15);
            recipe.AddTile(mod, nameof(ZTableTile));

            recipe.SetResult(this);
            recipe.AddRecipe();*/
        }
    }
}
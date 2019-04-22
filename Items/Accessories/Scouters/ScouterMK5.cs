using Terraria.ID;
using Terraria;

namespace DBT.Items.Accessories.Scouters
{
    public class ScouterMK5 : Scouter
    {
        public ScouterMK5() : base("Yellow Scouter", "A Piece of equipment used for scanning power levels.\n20% Increased Ki Damage and Hunter effect", Item.buyPrice(gold: 4, silver: 80), ItemRarityID.Lime, 0.20f)
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
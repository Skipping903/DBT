using Terraria.ID;
using Terraria;

namespace DBT.Items.Accessories.Scouters
{
    public class ScouterMK4 : Scouter
    {
        public ScouterMK4() : base("Purple Scouter", "A Piece of equipment used for scanning power levels\n15% Increased Ki Damage and Hunter effect", Item.buyPrice(gold: 3, silver: 60), ItemRarityID.LightPurple, 0.15f)
        {
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            // TODO Rework recipe.
            /*ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(PureKiCrystal), 20);
            recipe.AddIngredient(mod, nameof(ScouterMK3));
            recipe.AddTile(mod, nameof(ZTableTile));

            recipe.SetResult(this);
            recipe.AddRecipe();*/
        }
    }
}
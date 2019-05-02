using Terraria.ID;
using Terraria;

namespace DBT.Items.Accessories.Scouters
{
    public sealed class ScouterMK3 : Scouter
    {
        public ScouterMK3() : base("Red Scouter", "A Piece of equipment used for scanning power levels\n12% Increased Ki Damage and Hunter effect", Item.buyPrice(gold: 2, silver: 40), ItemRarityID.Pink, 0.12f)
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
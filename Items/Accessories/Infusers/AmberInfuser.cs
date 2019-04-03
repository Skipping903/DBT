using DBT.Items.Materials.Metals;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Accessories.Infusers
{
    public sealed class AmberInfuser : Infuser
    {
        public AmberInfuser() : base("Amber Ki Infuser", "Hitting enemies with ki attacks inflicts ichor.", 260 * Constants.SILVER_VALUE_MULTIPLIER, ItemID.Ichor)
        {
        }

        // TODO Rework recipe.
        public override void AddRecipes()
        {
            base.AddRecipes();

            /*ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(AngerKiCrystal), 25);
            recipe.AddIngredient(mod, nameof(ScrapMetal), 12);
            recipe.AddIngredient(ItemID.CursedFlame, 5);

            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();*/
        }
    }
}
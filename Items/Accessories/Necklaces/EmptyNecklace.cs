using Terraria.ID;

namespace DBT.Items.Accessories.Necklaces
{
    public sealed class EmptyNecklace : Necklace
    {
        public EmptyNecklace() : base("Empty Necklace", "It seems a gem can be attached to it.", 22, 24, 2000, 0, ItemRarityID.White)
        {
        }

        // TODO Rework recipe.
        /*public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(ScrapMetal), 3);
            recipe.AddRecipeGroup("IronBar", 5);
            recipe.AddTile(mod, nameof(ZTable));
            recipe.SetResult(this);

            recipe.AddRecipe();
        }*/
    }
}

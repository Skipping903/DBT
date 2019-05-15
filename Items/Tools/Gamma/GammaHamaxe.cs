using DBT.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Tools.Gamma
{
    public sealed class GammaHamaxe : GammaTool
    {
        public GammaHamaxe() : base("Gamma Hamaxe", 48, 46, Item.buyPrice(gold: 1), 27, 9, 0, 30, 100, 60, 7, 4)
        {
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.LunarBar, 12);
            recipe.AddIngredient(mod, nameof(GammaFragment), 14);
            recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
using DBT.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Tools.Radiant
{
    public sealed class RadiantPickaxe : RaditantTool
    {
        public RadiantPickaxe() : base("Radiant Pickaxe", 42, 40, Item.buyPrice(gold: 1), 11, 11, 225, 0, 0, 4, 80, 5.5f)
        {
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.LunarBar, 10);
            recipe.AddIngredient(mod, nameof(RadiantFragment), 12);
            recipe.AddTile(TileID.LunarCraftingStation);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
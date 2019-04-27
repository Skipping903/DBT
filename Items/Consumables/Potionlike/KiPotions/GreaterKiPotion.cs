using DBT.Items.KiStones;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Consumables.Potionlike.KiPotions
{
    public sealed class GreaterKiPotion : KiPotion
    {
        public GreaterKiPotion() : base("Greater Ki Potion", 1080, 0)
        {
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(KiStoneT3));
            recipe.AddIngredient(ItemID.UnicornHorn, 10);
            recipe.AddIngredient(ItemID.Waterleaf, 6);
            recipe.AddIngredient(ItemID.BottledWater, 6);
            recipe.AddTile(TileID.Bottles);

            recipe.SetResult(this, 6);
            recipe.AddRecipe();
        }
    }
}

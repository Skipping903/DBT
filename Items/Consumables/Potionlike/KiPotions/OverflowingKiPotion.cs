using DBT.Items.KiStones;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Consumables.Potionlike.KiPotions
{
    public sealed class OverflowingKiPotion : KiPotion
    {
        public OverflowingKiPotion() : base("Overflowing Ki Potion", 5100, 0)
        {
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(KiStoneT5));
            recipe.AddIngredient(mod, nameof(SuperKiPotion), 4);
            recipe.AddTile(TileID.Bottles);

            recipe.SetResult(this, 4);
            recipe.AddRecipe();
        }
    }
}

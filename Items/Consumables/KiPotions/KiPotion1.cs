using DBT.Items.KiStones;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Consumables.KiPotions
{
    public sealed class KiPotion1 : KiPotion
    {
        public KiPotion1() : base("Lesser Ki Potion", "Restores 330 Ki", 330)
        {
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(KiStoneT1), 1);
            recipe.AddIngredient(ItemID.Blinkroot, 4);
            recipe.AddIngredient(ItemID.Waterleaf, 6);
            recipe.AddIngredient(ItemID.BottledWater, 4);
            recipe.AddTile(TileID.Bottles);

            recipe.SetResult(this, 4);
            recipe.AddRecipe();
        }
    }
}
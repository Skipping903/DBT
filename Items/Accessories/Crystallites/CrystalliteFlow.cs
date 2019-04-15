using DBT.Items.KiStones;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Accessories.Crystallites
{
    public sealed class CrystalliteFlow : Crystallite
    {
        public CrystalliteFlow() : base("Influunt Crystallite", "'The essence of a calm flowing spirit lives within the crystal.'\nGreatly Increased speed while charging\n+1000 Max ki",
            22, 34, 640 * Constants.SILVER_VALUE_MULTIPLIER, ItemRarityID.Pink, 1000)
        {
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(KiStoneT4), 3);
            recipe.AddIngredient(mod, nameof(KiStoneT5), 3);
            recipe.AddIngredient(ItemID.CrystalShard, 10);
            recipe.AddIngredient(mod, nameof(SoulOfEntity), 10);
            recipe.AddIngredient(mod, nameof(CrystalliteControl));

            recipe.AddTile(mod, nameof(KaiTableTile));
            recipe.SetResult(this);

            recipe.AddRecipe();
        }
    }
}
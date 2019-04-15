using DBT.Items.KiStones;
using DBT.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Accessories.Crystallites
{
    public sealed class CrystalliteControl : Crystallite
    {
        public CrystalliteControl() : base("Imperium Crystallite", "'The essence of pure ki control lives within the crystal.'\nIncreased speed while charging\n+500 Max ki", 
            22, 34, 240 * Constants.SILVER_VALUE_MULTIPLIER, ItemRarityID.LightRed, 500)
        {  
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(KiStoneT2), 3);
            recipe.AddIngredient(mod, nameof(KiStoneT3), 3);
            recipe.AddIngredient(mod, nameof(AstralEssentia), 10);
            recipe.AddIngredient(mod, nameof(SkeletalEssence), 10);

            recipe.AddTile(mod, nameof(ZTableTile));
            recipe.SetResult(this);

            recipe.AddRecipe();
        }
    }
}
using DBT.Items.KiStones;
using DBT.Tiles;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace DBT.Items.Accessories.Crystallites
{
    public sealed class CrystalliteFlow : CrystalliteItem
    {
        public CrystalliteFlow() : base("Influunt Crystallite", "'The essence of a calm flowing spirit lives within the crystal.'\nGreatly Increased speed while charging\n+1000 Max ki",
            22, 34, Item.buyPrice(gold:6, silver: 40), ItemRarityID.Pink, 1000)
        {
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(KiStoneT4), 3);
            recipe.AddIngredient(mod, nameof(KiStoneT5), 3);
            recipe.AddIngredient(ItemID.CrystalShard, 10);
            //recipe.AddIngredient(mod, nameof(SoulOfEntity), 10);
            recipe.AddIngredient(mod, nameof(CrystalliteControl));

            recipe.AddTile(mod, nameof(KaiTableTile));
            recipe.SetResult(this);

            recipe.AddRecipe();
        }
    }
}
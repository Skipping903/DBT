using DBT.Items.KiStones;
using DBT.Tiles;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace DBT.Items.Accessories.Scouters
{
    public sealed class ScouterMK3 : Scouter
    {
        public ScouterMK3() : base("Red Scouter", "A Piece of equipment used for scanning power levels\n12% Increased Ki Damage and Hunter effect", Item.buyPrice(gold: 2, silver: 40), ItemRarityID.Pink, 0.12f)
        {
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(KiStoneT4), 3);
            recipe.AddIngredient(mod, nameof(ScouterMK2));
            recipe.AddTile(mod, nameof(ZTableTile));

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
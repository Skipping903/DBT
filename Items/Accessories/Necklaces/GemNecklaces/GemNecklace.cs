using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Accessories.Necklaces.GemNecklaces
{
    public abstract class GemNecklace : Necklace
    {
        private const int GEM_COUNT = 5;

        // TODO Add auto value calculation based on GEN_COUNT and Terraria.Main.item[gemID].value.
        protected GemNecklace(string displayName, string tooltip, int value, int defense, int gemID) : base(displayName, tooltip, 22, 34, value, ItemRarityID.Orange, defense)
        {
            GemID = gemID;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(EmptyNecklace));
            recipe.AddIngredient(GemID, GEM_COUNT);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);

            recipe.AddRecipe();
        }

        public int GemID { get; }
    }
}

using Terraria.ID;
using Terraria.ModLoader;

namespace DBTMod.Items.Accessories.Necklaces.GemNecklaces
{
    public abstract class GemNecklace : Necklace
    {
        protected GemNecklace(string displayName, string tooltip, int value, int defense, int gemID) : base(displayName, tooltip, 22, 34, value, ItemRarityID.Orange, defense)
        {
            Defense = defense;
            Value = value;
            GemID = gemID;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(EmptyNecklace));
            recipe.AddIngredient(GemID, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);

            recipe.AddRecipe();
        }

        public int Defense { get; }

        public int Value { get; }

        public int GemID { get; }
    }
}

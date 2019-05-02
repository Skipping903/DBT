using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Materials.Metals
{
    public sealed class RefinedMetal : DBTItem
    {
        public RefinedMetal() : base("Refined Metal", 
            "Level 3 Craft Item" +
            "\nThis high quality metal can be used to make some very fancy items.", 
            36, 34, value: Item.buyPrice(gold: 2, silver: 70), rarity: ItemRarityID.Pink)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.maxStack = 9999;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(ReclaimedMetal), 2);
            recipe.AddTile(TileID.Anvils);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
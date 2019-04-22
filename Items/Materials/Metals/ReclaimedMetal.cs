using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Materials.Metals
{
    public sealed class ReclaimedMetal : DBTItem
    {
        public ReclaimedMetal() : base("Reclaimed Metal", 
            "Level 2 Craft Item" +
            "\nThis mid-quality metal can be used to in some durable items." +
            "\nIt can be upgraded even further.",
            26, 22, value: Item.buyPrice(silver: 90), rarity: ItemRarityID.Orange)
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

            recipe.AddIngredient(mod, nameof(ScrapMetal), 3);
            recipe.AddTile(TileID.Anvils);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}

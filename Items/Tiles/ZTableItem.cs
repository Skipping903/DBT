using DBT.Tiles;
using Terraria.ID;

namespace DBT.Items.Tiles
{
    public sealed class ZTableItem : DBTItem
    {
        public ZTableItem() : base("Z-Table", "A device capable of bending the essence of Ki itself.")
        {
        }

        public override void SetDefaults()
        {
            item.width = 24;
            item.height = 26;
            item.maxStack = 99;

            item.useTurn = true;
            item.autoReuse = true;

            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.consumable = true;

            item.rare = ItemRarityID.Green;
            item.value = 3000;
            item.createTile = mod.TileType(nameof(ZTableTile));
        }

        // TODO Change recipe.
        /*public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(ScrapMetal), 15);
            recipe.AddIngredient(mod, nameof(StableKiCrystal), 20);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);

            recipe.AddRecipe();
        }*/
    }
}

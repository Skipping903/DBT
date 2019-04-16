using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Tiles
{
    public sealed class KaiTableItem : DBTItem
    {
        public KaiTableItem() : base("Kai Table", "It pulses with divine pressure, it seems to entrance you.")
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

            item.rare = ItemRarityID.Lime;
            item.value = 120000;
            item.createTile = mod.TileType(nameof(KaiTableTile));
        }
        /*public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ZTableItem", 1);
            recipe.AddIngredient(null, "PureKiCrystal", 15);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 12);
            recipe.AddIngredient(ItemID.Pearlwood, 20);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }*/
    }
}
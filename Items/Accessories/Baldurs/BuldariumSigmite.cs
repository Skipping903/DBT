using DBT.Items.KiStones;
using DBT.Players;
using DBT.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Accessories.Baldurs
{
    public sealed class BuldariumSigmite : BaldurItem
    {
        public BuldariumSigmite() : base("Buldarium Sigmite", "'A fragment of the God of Defense's soul.'\nCharging grants a protective barrier that grants massively increased defense\nCharging also grants drastically increased life regen\nIncreased Ki charge rate",
            180 * Constants.SILVER_VALUE_MULTIPLIER, 10, ItemRarityID.Yellow, 0.5f)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.GetModPlayer<DBTPlayer>().IsCharging)
                player.shinyStone = true;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(BaldurEssentia));
            recipe.AddIngredient(mod, nameof(KiStoneT3));
            recipe.AddIngredient(ItemID.ShinyStone);

            recipe.AddTile(mod, nameof(ZTableTile));
            recipe.SetResult(this);

            recipe.AddRecipe();
        }
    }
}
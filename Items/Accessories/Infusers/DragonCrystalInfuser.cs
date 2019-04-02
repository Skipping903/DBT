using DBT.Items.Materials.Metals;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Accessories.Infusers
{
    public class DragonCrystalInfuser : DBTItem
    {
        public DragonCrystalInfuser() : base("Dragon Crystal Ki Infuser", "Hitting enemies with ki attacks inflicts a multitude of debuffs.")
        {
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 34;
            item.value = 1371 * Constants.SILVER_VALUE_MULTIPLIER;
            item.rare = ItemRarityID.Yellow;
            item.accessory = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            // TODO Rework recipe.
            //recipe.AddIngredient(mod, nameof(PureKiCrystal), 25);
            recipe.AddIngredient(mod, nameof(ScrapMetal), 12);
            recipe.AddIngredient(mod, nameof(AmberInfuser));
            recipe.AddIngredient(mod, nameof(AmethystInfuser));
            recipe.AddIngredient(mod, nameof(DiamondInfuser));
            recipe.AddIngredient(mod, nameof(EmeraldInfuser));
            recipe.AddIngredient(mod, nameof(RubyInfuser));
            recipe.AddIngredient(mod, nameof(SapphireInfuser));
            recipe.AddIngredient(mod, nameof(TopazInfuser));

            recipe.AddIngredient(ItemID.Ectoplasm, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);

            recipe.AddRecipe();
        }
    }
}
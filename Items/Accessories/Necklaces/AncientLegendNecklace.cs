using DBTMod.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBTMod.Items.Accessories.Necklaces
{
    public sealed class AncientLegendNecklace : Necklace
    {
        public AncientLegendNecklace() : base("Ancient Legend Necklace", "A ancient necklace that seems to seal energy.\n12% reduced ki usage\n9% increased ki damage\n-500 max ki", 24, 28, 64000, ItemRarityID.Pink, 0)
        {
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamage *= 1.09f;
            dbtPlayer.MaxKiModifier -= 500;
        }

        // TODO Rework recipe.
        /*public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(PureKiCrystal), 20);
            recipe.AddIngredient(mod, nameof(DemonicSoul), 5);
            recipe.AddIngredient(ItemID.GoldBar, 8);
            
            recipe.AddTile(mod, nameof(KaiTableTile));
            recipe.SetResult(this);

            recipe.AddRecipe();
        }*/
    }
}
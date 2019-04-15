using DBT.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Accessories
{
    [AutoloadEquip(EquipType.Waist)]
    public sealed class AncientLegendWaistCape : DBTItem
    {
        public AncientLegendWaistCape() : base("Ancient Legend Waistcape", "A ancient garment made of a Ki enhancing material\n14% reduced Ki usage\n6% increased Ki damage\n-250 max Ki",
            24, 28, value: 300 * Constants.SILVER_VALUE_MULTIPLIER, rarity: ItemRarityID.Pink)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);

            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier *= 1.06f;
            dbtPlayer.MaxKiModifier -= 250;
        }

        // TODO Rework recipe.
        public override void AddRecipes()
        {
            /*base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(PureKiCrystal), 25);
            recipe.AddIngredient(mod, nameof(SatanicCloth), 8);
            recipe.AddIngredient(mod, nameof(KaiTable));
            
            recipe.SetResult(this);
            recipe.AddRecipe();*/
        }
    }
}
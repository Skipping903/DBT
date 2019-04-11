using DBT.Items.Materials.Metals;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Accessories.Infusers
{
    public class DragonCrystalInfuser : DBTItem
    {
        public DragonCrystalInfuser() : base("Dragon Crystal Ki Infuser", "Hitting enemies with Ki attacks inflicts a multitude of debuffs")
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

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            base.OnHitNPC(player, target, damage, knockBack, crit);

            target.AddBuff(BuffID.ShadowFlame, 300);
            target.AddBuff(BuffID.CursedInferno, 300);
            target.AddBuff(BuffID.Confused, 180);
            target.AddBuff(BuffID.Frostburn, 180);
            target.AddBuff(BuffID.Ichor, 300);
            target.AddBuff(BuffID.OnFire, 180);
            target.AddBuff(BuffID.Frostburn, 180);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            // TODO Rework recipe.
            //recipe.AddIngredient(mod, nameof(PureKiCrystal), 25);
            recipe.AddIngredient(mod, nameof(ScrapMetal), 12);
            recipe.AddIngredient(mod, nameof(IchorInfuser));
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
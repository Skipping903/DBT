using DBTMod.Players;
using DBTMod.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBTMod.Items.Accessories.Necklaces.GemNecklaces
{
    public sealed class DragonGemNecklace : DBTItem
    {
        public DragonGemNecklace() : base("Dragon Gem Necklace", "Infused with the essence of the dragon.\nAll effects of the previous necklaces, some enhanced.")
        {
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 34;
            item.value = 640 * Constants.SILVER_VALUE_MULTIPLIER;

            item.rare = ItemRarityID.LightRed;
            item.accessory = true;
            item.defense = 2;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamage += 0.07f;
            dbtPlayer.MaxKiModifier += 250;

            player.endurance += 0.07f;

            player.meleeDamage += 0.07f;
            player.meleeSpeed += 0.07f;

            player.magicDamage += 0.07f;
            player.magicCrit += 7;

            player.rangedDamage += 0.07f;
            player.rangedCrit += 7;

            player.lifeRegen += 2;

            player.minionDamage += 0.07f;
            player.maxMinions += 2;

            player.statDefense += 6;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(AmberGemNecklace));
            recipe.AddIngredient(mod, nameof(AmethystGemNecklace));
            recipe.AddIngredient(mod, nameof(DiamondGemNecklace));
            recipe.AddIngredient(mod, nameof(EmeraldGemNecklace));
            recipe.AddIngredient(mod, nameof(RubyGemNecklace));
            recipe.AddIngredient(mod, nameof(SapphireGemNecklace));
            recipe.AddIngredient(mod, nameof(TopazGemNecklace));

            recipe.AddTile(mod, nameof(ZTableTile));
            recipe.SetResult(this);

            recipe.AddRecipe();
        }
    }
}
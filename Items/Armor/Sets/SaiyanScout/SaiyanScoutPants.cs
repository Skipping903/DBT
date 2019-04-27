using DBT.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Armor.Sets.SaiyanScout
{
    [AutoloadEquip(EquipType.Legs)]
    public sealed class SaiyanScoutPants : DBTArmorPiece
    {
        public SaiyanScoutPants() : base("Saiyan Scout Pants",
            "2% Increased Ki damage" +
            "\n2% Increased Ki knockback" +
            "\n6% Increased movement speed",
            28, 18, value: Item.buyPrice(silver: 12), defense: 2, rarity: ItemRarityID.Green)
        {
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 0.02f;
            dbtPlayer.KiKnockbackAddition += 0.02f;
            player.moveSpeed += 0.06f;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.Silk, 14);
            recipe.AddIngredient(ItemID.CopperBar, 6);
            recipe.AddTile(TileID.Anvils);

            recipe.SetResult(this);
            recipe.AddRecipe();


            recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.Silk, 14);
            recipe.AddIngredient(ItemID.TinBar, 6);
            recipe.AddTile(TileID.Anvils);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
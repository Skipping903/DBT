using DBT.Items.Materials;
using DBT.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Armor.Sets.Demon
{
    [AutoloadEquip(EquipType.Legs)]
    public sealed class DemonLeggings : DBTArmorPiece
    {
        public DemonLeggings() : base("Demon Leggings",
            "13% Increased Ki Damage" +
            "\n9% Increased Ki Crit Chance" +
            "\n+300 Max Ki" +
            "\nIncreased Ki Regen" +
            "\n12% Increased movement speed",
            28, 18, Item.buyPrice(silver: 36), 12, ItemRarityID.Cyan)
        {
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 0.13f;
            dbtPlayer.KiCritAddition += 9;
            dbtPlayer.MaxKiModifier += 300;
            dbtPlayer.ExternalKiRegenerationModifier = 1f;

            player.moveSpeed += 0.12f;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(SatanicCloth));
            recipe.AddTile(TileID.Anvils);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
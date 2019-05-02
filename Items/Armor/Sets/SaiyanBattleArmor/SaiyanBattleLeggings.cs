using DBT.Items.Materials;
using DBT.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Armor.Sets.SaiyanBattleArmor
{
    [AutoloadEquip(EquipType.Legs)]
    public sealed class SaiyanBattleLeggings : DBTArmorPiece
    {
        public SaiyanBattleLeggings() : base("Saiyan Battle Leggings",
            "7% Increased Ki Damage" +
            "\n5% Increased Ki Crit Chance" +
            "\n16% Increased movement speed",
            28, 18, value: Item.buyPrice(silver: 32), defense: 9, rarity: ItemRarityID.LightRed)
        {
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 0.07f;
            dbtPlayer.KiCritAddition += 5;
            player.moveSpeed += 0.16f;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();
            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.HellstoneBar, 15);
            recipe.AddIngredient(mod, nameof(SkeletalEssence), 10);
            recipe.AddTile(TileID.Anvils);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
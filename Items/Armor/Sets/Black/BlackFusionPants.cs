using DBT.Items.Materials;
using DBT.Players;
using DBT.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Armor.Sets.Black
{
    [AutoloadEquip(EquipType.Legs)]
    public sealed class BlackFusionPants : DBTArmorPiece
    {
        public BlackFusionPants() : base("Black Fusion Pants",
            "20% Increased Ki Damage" +
            "\n16% Increased Ki Crit Chance" +
            "\n+500 Max Ki" +
            "\nIncreased Ki Regen" +
            "\n18% Increased movement speed",
            28, 18, Item.buyPrice(silver: 64), 16, ItemRarityID.Cyan)
        {
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 0.20f;
            dbtPlayer.KiCritAddition += 16;
            dbtPlayer.MaxKiModifier += 500;
            dbtPlayer.ExtraKiRegeneration += 2;

            player.moveSpeed += 0.18f;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(mod, nameof(DivineThread), 12);
            recipe.AddTile(mod, nameof(KaiTableTile));

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
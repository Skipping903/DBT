using DBT.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Armor.Individual
{
    [AutoloadEquip(EquipType.Head)]
    public sealed class TitaniumCap : DBTArmorPiece
    {
        public TitaniumCap() : base("Titanium Cap",
            "14% Increased Ki Damage" +
            "\n9% Increased Ki Crit Chance" +
            "\nMaximum Ki increased by 250",
            28, 18, Item.buyPrice(silver: 22), 8, ItemRarityID.LightRed)
        {
        }

        public override bool IsArmorSet(Item head, Item body, Item legs) => base.IsArmorSet(head, body, legs) && body.type == ItemID.TitaniumBreastplate && legs.type == ItemID.TitaniumLeggings;

        public override void UpdateArmorSet(Player player)
        {
            base.UpdateArmorSet(player);

            player.setBonus = "Briefly become invulnerable after striking an enemy.";
            player.onHitDodge = true;
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 0.15f;
            dbtPlayer.KiCritAddition += 9;
            dbtPlayer.MaxKiModifier += 250;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.TitaniumBar, 13);
            recipe.AddTile(TileID.MythrilAnvil);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
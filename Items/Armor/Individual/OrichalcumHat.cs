using DBT.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Armor.Individual
{
    [AutoloadEquip(EquipType.Head)]
    public sealed class OrichalcumHat : DBTArmorPiece
    {
        public OrichalcumHat() : base("Orichalcum Hat",
            "10% Increased Ki Damage" +
            "\n7% Increased Ki Crit Chance" +
            "\nMaximum Ki increased by 100",
            20, 16, Item.buyPrice(silver: 18), 6, ItemRarityID.LightRed)
        {
        }

        public override bool IsArmorSet(Item head, Item body, Item legs) => base.IsArmorSet(head, body, legs) && body.type == ItemID.OrichalcumBreastplate && legs.type == ItemID.OrichalcumLeggings;

        public override void UpdateArmorSet(Player player)
        {
            base.UpdateArmorSet(player);

            player.setBonus = "Flower petals will fall on your target for extra damage.";
            player.onHitPetal = true;
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);

            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 0.10f;
            dbtPlayer.KiCritAddition += 7;
            dbtPlayer.MaxKiModifier += 100;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.OrichalcumBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
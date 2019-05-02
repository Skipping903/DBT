using DBT.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Armor.Individual
{
    [AutoloadEquip(EquipType.Head)]
    public sealed class HallowedFedora : DBTArmorPiece
    {
        public HallowedFedora() : base("Hallowed Fedora",
            "13% Increased Ki Damage"
            + "\n11% Increased Ki Crit Chance" +
            "\nMaximum Ki increased by 300",
            20, 16, Item.buyPrice(silver: 24), 10, ItemRarityID.LightRed)
        {
        }

        public override bool IsArmorSet(Item head, Item body, Item legs) => base.IsArmorSet(head, body, legs) && body.type == ItemID.HallowedPlateMail && legs.type == ItemID.HallowedGreaves;

        public override void UpdateArmorSet(Player player)
        {
            base.UpdateArmorSet(player);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 0.09f;
            dbtPlayer.MaxKiModifier += 200;
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 0.13f;
            dbtPlayer.KiCritAddition += 11;
            dbtPlayer.MaxKiModifier += 300;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.HallowedBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
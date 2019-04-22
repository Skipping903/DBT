using DBT.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Armor.Individual
{
    [AutoloadEquip(EquipType.Head)]
    public sealed class MythrilGlasses : DBTArmorPiece
    {
        public MythrilGlasses() : base("Mythril Glasses",
            "11% Increased Ki Damage" +
            "\n6% Increased Ki Crit Chance" +
            "\nMaximum Ki increased by 100",
            20, 16, Item.buyPrice(silver: 18), 5, ItemRarityID.LightRed)
        {
        }

        public override bool IsArmorSet(Item head, Item body, Item legs) => base.IsArmorSet(head, body, legs) && body.type == ItemID.MythrilChainmail && legs.type == ItemID.MythrilGreaves;

        public override void UpdateArmorSet(Player player)
        {
            base.UpdateArmorSet(player);

            player.setBonus = "Ki orbs restore twice as much Ki.";
            player.GetModPlayer<DBTPlayer>().KiOrbRestoreAmount *= 2;
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 0.11f;
            dbtPlayer.KiCritAddition += 6;
            dbtPlayer.MaxKiModifier += 100;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.MythrilBar, 10);
            recipe.AddTile(TileID.MythrilAnvil);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
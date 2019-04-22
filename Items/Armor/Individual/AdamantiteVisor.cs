using DBT.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Armor.Individual
{
    [AutoloadEquip(EquipType.Head)]
    public sealed class AdamantiteVisor : DBTArmorPiece
    {
        public AdamantiteVisor() : base("Adamantite Visor",
            "12% Increased Ki Damage" +
            "\n10% Increased Ki Crit Chance" +
            "\nMaximum Ki increased by 250",
            20, 16, Item.buyPrice(silver: 22), 9, ItemRarityID.LightRed)
        {
        }

        public override bool IsArmorSet(Item head, Item body, Item legs) => base.IsArmorSet(head, body, legs) && body.type == ItemID.AdamantiteBreastplate && legs.type == ItemID.AdamantiteLeggings;

        public override void UpdateArmorSet(Player player)
        {
            base.UpdateArmorSet(player);

            player.setBonus = "7% Increased Ki damage";
            player.GetModPlayer<DBTPlayer>().KiDamageMultiplier += 0.07f;
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 0.12f;
            dbtPlayer.KiCritAddition += 10;
            dbtPlayer.MaxKiModifier += 250;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.AdamantiteBar, 12);
            recipe.AddTile(TileID.MythrilAnvil);
            
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
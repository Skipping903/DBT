using DBT.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Armor.Individual
{
    [AutoloadEquip(EquipType.Head)]
    public sealed class CobaltCrown : DBTArmorPiece
    {
        public CobaltCrown() : base("Cobalt Crown",
            "9% Increased Ki Damage\n" +
            "6% Increased Ki Crit Chance",
            16, 20, Item.buyPrice(silver: 16), 5, ItemRarityID.LightRed)
        {
        }

        public override bool IsArmorSet(Item head, Item body, Item legs) => base.IsArmorSet(head, body, legs) && body.type == ItemID.CobaltBreastplate && legs.type == ItemID.CobaltLeggings;

        public override void UpdateArmorSet(Player player)
        {
            base.UpdateArmorSet(player);

            player.setBonus = "15% Increased Ki knockback";
            player.GetModPlayer<DBTPlayer>().KiKnockbackAddition += 15;
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 0.09f;
            dbtPlayer.KiCritAddition += 6;
        }

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            base.DrawHair(ref drawHair, ref drawAltHair);

            drawHair = true;
            drawAltHair = true;
        }

        public override void AddRecipes()
        {
            base.AddRecipes();

            ModRecipe recipe = new ModRecipe(mod);

            recipe.AddIngredient(ItemID.CobaltBar, 10);
            recipe.AddTile(TileID.Anvils);

            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
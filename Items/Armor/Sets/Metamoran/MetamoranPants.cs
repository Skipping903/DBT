using DBT.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Armor.Sets.Metamoran
{
    [AutoloadEquip(EquipType.Legs)]
    public sealed class MetamoranPants : DBTArmorPiece
    {
        public MetamoranPants() : base("Metamoran Pants",
            "10% Increased Ki damage" +
            "\n20% Increased Ki knockback" +
            "\n+25% Increased movement speed",
            28, 18, value: Item.buyPrice(silver: 22), defense: 6, rarity: ItemRarityID.Red)
        {
        }

        public override void UpdateEquip(Player player)
        {
            base.UpdateEquip(player);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 0.1f;
            dbtPlayer.KiKnockbackAddition += 0.2f;

            player.moveSpeed += 0.25f;
        }
    }
}
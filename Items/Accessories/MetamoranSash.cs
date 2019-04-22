using DBT.Commons.Items;
using DBT.Commons.Players;
using DBT.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Accessories
{
    [AutoloadEquip(EquipType.Waist)]
    public sealed class MetamoranSash : DBTAccessory, IIsPatreonItem, IHandleOnPlayerHitNPC
    {
        public MetamoranSash() : base("Metamoran Sash",
            "'Your own bad energy will be your undoing!'" +
            "\n10% Increased Ki damage" +
            "\n30% Reduced Ki usage" +
            "\n15% chance to do double damage",
            18, 30, value: Item.buyPrice(silver: 80), defense: 3, rarity: ItemRarityID.Expert)
        {
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 0.1f;
            dbtPlayer.KiDrainMultiplier -= 0.3f;

            
        }

        public void OnPlayerHitNPC(Item withItem, NPC target, ref int damage, ref float knockback, ref bool crit)
        {
            if (Main.rand.NextBool(15))
                damage *= 2;
        }

        public string PatreonDonor => "Chese780";
    }
}
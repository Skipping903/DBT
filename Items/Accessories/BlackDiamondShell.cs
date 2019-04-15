using DBT.Commons;
using DBT.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace DBT.Items.Accessories
{
    public sealed class BlackDiamondShell : DBTItem, IUpdateOnPlayerPreHurt
    {
        public BlackDiamondShell() : base("Black Diamond Shell", "A jeweled turtle shell that gets the attention of many creatures, for some reason it's unbelievably tough\n12% increased ki damage, 14% increased ki knockback\n+200 Max Ki\nGetting hit restores a small amount of Ki", 
            20, 30, value: 240 * Constants.SILVER_VALUE_MULTIPLIER, defense: 14, rarity: ItemRarityID.Lime)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            DBTPlayer dbtPlayer = player.GetModPlayer<DBTPlayer>();

            dbtPlayer.KiDamageMultiplier += 0.12f;
            dbtPlayer.KiKnockbackAddition += 0.14f;
            dbtPlayer.MaxKiModifier += 200;
        }

        public bool OnPlayerPreHurt(DBTPlayer dbtPlayer, bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            int i = Main.rand.Next(10, 100);
            dbtPlayer.ModifyKi(i);

            CombatText.NewText(new Rectangle((int)dbtPlayer.player.position.X, (int)dbtPlayer.player.position.Y, dbtPlayer.player.width, dbtPlayer.player.height), new Color(51, 204, 255), i, false, false);
            return true;
        }
    }
}
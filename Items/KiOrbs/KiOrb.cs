using DBT.Helpers;
using DBT.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace DBT.Items.KiOrbs
{
    public sealed class KiOrb : DBTItem
    {
        public KiOrb() : base("Ki Orb", "", 20, 66)
        {
        }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(6, 3));
            Lighting.AddLight(item.Center, 0.1f, 0.3f, 0.9f);
        }

        public override void GrabRange(Player player, ref int grabRange)
        {
            base.GrabRange(player, ref grabRange);
            grabRange *= (int) player.GetModPlayer<DBTPlayer>().KiOrbGrabRange;
        }

        public override bool OnPickup(Player player)
        {
            SoundHelper.PlayVanillaSound(SoundID.NPCDeath7, player);
            CombatText.NewText(new Rectangle((int) player.position.X, (int) player.position.Y, player.width, player.height), new Color(51, 204, 255), 50, false, false);

            return false;
        }
    }
}
using DBTMod.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBTMod.HairStyles
{
    public sealed class HairPlayerLayer : PlayerLayer
    {
        public static readonly HairPlayerLayer hairLayer = new HairPlayerLayer();

        public HairPlayerLayer() : base(DBTMod.Instance.Name, "HairLayer", null, DrawLayer)
        {
        }

        private static void DrawLayer(PlayerDrawInfo drawInfo)
        {
            if (Main.netMode == NetmodeID.Server) return;

            Player player = drawInfo.drawPlayer;
            DBTRPlayer dbtrPlayer = player.GetModPlayer<DBTRPlayer>();

            if (dbtrPlayer.CurrentHair == null) return;

            Color alpha = drawInfo.drawPlayer.GetImmuneAlpha(Lighting.GetColor((int)(drawInfo.position.X + drawInfo.drawPlayer.width * 0.5) / 16, (int)((drawInfo.position.Y + drawInfo.drawPlayer.height * 0.25) / 16.0), dbtrPlayer.player.hairColor), drawInfo.shadow);

            Vector2 bobbingOffset = Vector2.Zero;
            int bodyFrame = player.bodyFrame.Y / player.bodyFrame.Height;

            if (dbtrPlayer.ChosenHairStyle.AutoBobbing)
                bobbingOffset = Main.OffsetsPlayerHeadgear[bodyFrame];

            int height = dbtrPlayer.CurrentHair.Height / 14;

            DrawData data = new DrawData(
                dbtrPlayer.CurrentHair,

                /*new Vector2(
                    (int)(drawInfo.position.X - Main.screenPosition.X - player.bodyFrame.Width / 2 + player.width / 2), 
                    (int)(drawInfo.position.Y - Main.screenPosition.Y + player.height - player.bodyFrame.Height + 4f)) + player.headPosition + drawInfo.headOrigin*/

                new Vector2(
                    (int)(drawInfo.position.X - Main.screenPosition.X - player.bodyFrame.Width / 2 + player.width / 2),
                    (int)(drawInfo.position.Y - Main.screenPosition.Y + player.height - player.bodyFrame.Height)) 
                    + player.headPosition + drawInfo.headOrigin + dbtrPlayer.ChosenHairStyle.Offset + bobbingOffset,

                // TODO Add hair animation.
                new Rectangle(0, 0, dbtrPlayer.CurrentHair.Width, height),
                alpha,
                player.headRotation,
                drawInfo.headOrigin,
                1f,
                drawInfo.spriteEffects,
                0
                );

            data.shader = drawInfo.hairShader;
            Main.playerDrawData.Add(data);
        }
    }
}

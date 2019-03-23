using DBTR.Extensions;
using DBTR.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBTR.Auras
{
    public sealed class AuraPlayerLayer : PlayerLayer
    {
        public AuraPlayerLayer(int index) : base(DBTRMod.Instance.Name, "AuraLayer" + index, null, DrawLayer)
        {
        }

        private static void DrawLayer(PlayerDrawInfo drawInfo)
        {
            if (Main.netMode == NetmodeID.Server) return;

            DBTRPlayer dbtrPlayer = drawInfo.drawPlayer.GetModPlayer<DBTRPlayer>();

            AuraAppearance aura = dbtrPlayer.GetAura();
            if (aura == null) return;

            int auraHeight = aura.Information.GetHeight(dbtrPlayer);

            Texture2D auraTexture = aura.Information.GetTexture(dbtrPlayer);
            Rectangle auraRectangle = new Rectangle(0, auraHeight * dbtrPlayer.AuraFrameIndex, auraTexture.Width, auraHeight);

            float scale = aura.Information.GetAuraScale(dbtrPlayer);
            Tuple<float, Vector2> rotationAndPosition = aura.Information.GetRotationAndPosition(dbtrPlayer);

            SamplerState samplerState = dbtrPlayer.GetPlayerSamplerState();
            aura.Information.BlendState.SetSpriteBatchForPlayerLayerCustomDraw(samplerState);

            Main.spriteBatch.Draw(auraTexture, rotationAndPosition.Item2 - Main.screenPosition, auraRectangle, Color.White, rotationAndPosition.Item1,
                new Vector2(aura.Information.GetWidth(dbtrPlayer), aura.Information.GetHeight(dbtrPlayer)) * 0.5f, scale, SpriteEffects.None, 0f);

            samplerState.ResetSpriteBatchForPlayerDrawLayers();
        }
    }
}

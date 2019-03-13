using System;
using System.Collections.Generic;
using DBTR.Auras;
using DBTR.Extensions;
using DBTR.Transformations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace DBTR.Players
{
    public sealed partial class DBTRPlayer
    {
        internal void PostUpdateHandleAura()
        {
            PlayerTransformation transformation = GetFirstTransformation();
            AuraAppearance aura = GetAura();

            if (Aura != aura)
                Aura = aura;

            if (Aura == null) return;

            AuraFrameTimer += Aura.GetTicksPerFrameTimerTick(this);

            if (AuraFrameTimer >= Aura.Information.FramesTimer)
            {
                AuraFrameTimer = 0;
                AuraFrameIndex++;
            }

            if (AuraFrameIndex >= Aura.Information.FramesCount)
                AuraFrameIndex = 0;

            float[] rgbLightingRadiuses = aura.Lighting.GetRGBLightingRadiuses(this);
            Lighting.AddLight(player.Center + player.velocity * 8f, rgbLightingRadiuses[0], rgbLightingRadiuses[1], rgbLightingRadiuses[2]);
        }


        public AuraAppearance GetAura()
        {
            if (player.dead) return null;

            if (ActiveTransformations.Count == 0 && IsCharging)
                // TODO Change this to racial/trait aura.
                return AuraAnimationInformation.chargeAura;

            // TODO Support multiple auras.
            PlayerTransformation transformation = GetFirstTransformation();
            if (transformation != null)
                return transformation.Definition.Appearance.Aura;

            return null;
        }

        public void DrawAura(AuraAppearance aura)
        {
            int auraHeight = aura.Information.GetHeight(this);

            Texture2D auraTexture = aura.Information.GetTexture(this);
            Rectangle auraRectangle = new Rectangle(0, auraHeight * AuraFrameIndex, auraTexture.Width, auraHeight);

            float scale = aura.Information.GetAuraScale(this);
            Tuple<float, Vector2> rotationAndPosition = aura.Information.GetRotationAndPosition(this);

            aura.Information.BlendState.SetSpriteBatchForPlayerLayerCustomDraw(GetPlayerSamplerState());

            Main.spriteBatch.Draw(auraTexture, rotationAndPosition.Item2 - Main.screenPosition, auraRectangle, Color.White, rotationAndPosition.Item1,
                new Vector2(aura.Information.GetWidth(this), aura.Information.GetHeight(this)) * 0.5f, scale, SpriteEffects.None, 0f);

            GetPlayerSamplerState().ResetSpriteBatchForPlayerDrawLayers();
        }

        public void HandleAuraDrawLayers(List<PlayerLayer> layers)
        {
            AuraAnimationInformation.auraLayer.visible = true;
            layers.Insert(layers.FindIndex(l => l.Name == "MiscEffectsBack"), AuraAnimationInformation.auraLayer);
        }


        public AuraAppearance Aura { get; private set; }


        public int AuraFrameTimer { get; private set; }

        public int AuraFrameIndex { get; private set; }
    }
}

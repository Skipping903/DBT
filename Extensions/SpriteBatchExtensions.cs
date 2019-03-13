using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace DBTR.Extensions
{
    public static class SpriteBatchExtensions
    {
        // TODO Give credit to who wrote this (guessing X3n0ph0b3).
        public static void SetSpriteBatchForPlayerLayerCustomDraw(this BlendState blendState, SamplerState samplerState)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, blendState, samplerState, DepthStencilState.None, Main.instance.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
        }

        public static void ResetSpriteBatchForPlayerDrawLayers(this SamplerState samplerState)
        {
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, samplerState, DepthStencilState.None, Main.instance.Rasterizer, null, Main.GameViewMatrix.TransformationMatrix);
        }
    }
}

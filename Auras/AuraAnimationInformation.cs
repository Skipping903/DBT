using System;
using DBTMod.Extensions;
using DBTMod.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace DBTMod.Auras
{
    public class AuraAnimationInformation
    {
        public static readonly AuraAppearance chargeAura = new ChargeAura();

        // TODO Add multi-transformation support.
        public static readonly PlayerLayer auraLayer = new AuraPlayerLayer(0);

        public AuraAnimationInformation(string texture, int framesCount, int framesTimer, BlendState blendState, float baseScale, bool isFormAura, int priority = 0, int ticksPerFrameTimerTick = 1)
        {
            TexturePath = texture;

            FramesCount = framesCount;
            FramesTimer = framesTimer;

            BlendState = blendState;

            BaseScale = baseScale;
            IsFormAura = isFormAura;

            Priority = priority;

            TicksPerFrameTimerTick = ticksPerFrameTimerTick;
        }

        public AuraAnimationInformation(Type type, int framesCount, int framesTimer, BlendState blendState, float baseScale, bool isFormAura, int priority = 0, int ticksPerFrameTimerTick = 1) : 
            this(GetAuraTextureFromType(type), framesCount, framesTimer, blendState, baseScale, isFormAura, priority, ticksPerFrameTimerTick)
        {
        }


        #region Public Methods

        public virtual float GetAuraScale(DBTPlayer dbtPlayer) => BaseScale;

        public virtual int GetAuraOffsetY(DBTPlayer dbtPlayer)
        {
            int frameHeight = GetHeight(dbtPlayer);
            float scale = GetAuraScale(dbtPlayer);

            return (int)-(frameHeight / 2f * scale - dbtPlayer.player.height * 0.775f);
        }


        public int GetHeight(DBTPlayer dbtPlayer) => GetTexture(dbtPlayer).Height / FramesCount;
        public int GetWidth(DBTPlayer dbtPlayer) => GetTexture(dbtPlayer).Width;


        public Tuple<float, Vector2> GetRotationAndPosition(DBTPlayer dbtPlayer)
        {
            bool playerMostlyStationary = Math.Abs(dbtPlayer.player.velocity.X) <= 6f && Math.Abs(dbtPlayer.player.velocity.Y) <= 6f;

            float rotation = 0f;
            Vector2 position = Vector2.Zero;

            float scale = GetAuraScale(dbtPlayer);
            int auraOffsetY = GetAuraOffsetY(dbtPlayer);

            // TODO Add code for flight aura animation.

            //if (playerMostlyStationary)
            //{
                position = dbtPlayer.player.Center + new Vector2(-0.75f, auraOffsetY);
                rotation = 0f;
            //}

            return new Tuple<float, Vector2>(rotation, position);
        }

        // TODO Try to add caching.
        public Vector2 GetCenter(DBTPlayer dbtPlayer) => GetRotationAndPosition(dbtPlayer).Item2 + new Vector2(GetWidth(dbtPlayer), GetHeight(dbtPlayer)) * 0.5f;


        public virtual Texture2D GetTexture(DBTPlayer dbtPlayer) => dbtPlayer.mod.GetTexture(TexturePath);

        protected static string GetAuraTextureFromType(Type type) => type.GetTexturePathFromType() + "Aura";

        #endregion


        public string TexturePath { get; }


        public int FramesCount { get; }

        public int FramesTimer { get; }


        public BlendState BlendState { get; }


        public float BaseScale { get; }

        public bool IsFormAura { get; }


        public int Priority { get; }

        
        public int TicksPerFrameTimerTick { get; }
    }

    public sealed class ChargeAura : AuraAppearance
    {
        public ChargeAura() : base(new AuraAnimationInformation("Auras/BaseAura", 4, 3, BlendState.Additive, 1f, false), new LightingAppearance(new float[] { 1f, 1f, 1f }))
        {
        }

        public override int GetTicksPerFrameTimerTick(DBTPlayer dbtPlayer) => Information.TicksPerFrameTimerTick;
    }
}

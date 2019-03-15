using System;
using DBTR.Players;
using DBTR.Transformations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace DBTR.Auras
{
    public class AuraAnimationInformation
    {
        public static readonly AuraAppearance chargeAura = new ChargeAura();

        // TODO Add multi-transformation support.
        public static readonly PlayerLayer auraLayer = new AuraDrawPlayer(0);

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
            this(GetTexturePathFromTransformation(type), framesCount, framesTimer, blendState, baseScale, isFormAura, priority, ticksPerFrameTimerTick)
        {
        }


        #region Public Methods

        public virtual float GetAuraScale(DBTRPlayer dbtrPlayer) => BaseScale;

        public virtual int GetAuraOffsetY(DBTRPlayer dbtrPlayer)
        {
            int frameHeight = GetHeight(dbtrPlayer);
            float scale = GetAuraScale(dbtrPlayer);

            return (int)-(frameHeight / 2f * scale - dbtrPlayer.player.height * 0.775f);
        }


        public int GetHeight(DBTRPlayer dbtrPlayer) => GetTexture(dbtrPlayer).Height / FramesCount;
        public int GetWidth(DBTRPlayer dbtrPlayer) => GetTexture(dbtrPlayer).Width;


        public Tuple<float, Vector2> GetRotationAndPosition(DBTRPlayer dbtrPlayer)
        {
            bool playerMostlyStationary = Math.Abs(dbtrPlayer.player.velocity.X) <= 6f && Math.Abs(dbtrPlayer.player.velocity.Y) <= 6f;

            float rotation = 0f;
            Vector2 position = Vector2.Zero;

            float scale = GetAuraScale(dbtrPlayer);
            int auraOffsetY = GetAuraOffsetY(dbtrPlayer);

            // TODO Add code for flight aura animation.

            //if (playerMostlyStationary)
            //{
                position = dbtrPlayer.player.Center + new Vector2(-0.75f, auraOffsetY);
                rotation = 0f;
            //}

            return new Tuple<float, Vector2>(rotation, position);
        }

        // TODO Try to add caching.
        public Vector2 GetCenter(DBTRPlayer dbtrPlayer) => GetRotationAndPosition(dbtrPlayer).Item2 + new Vector2(GetWidth(dbtrPlayer), GetHeight(dbtrPlayer)) * 0.5f;


        public virtual Texture2D GetTexture(DBTRPlayer dbtrPlayer) => dbtrPlayer.mod.GetTexture(TexturePath);


        public static string GetTexturePathFromTransformation(TransformationDefinition transformation) => GetTexturePathFromTransformation(transformation.GetType());

        public static string GetTexturePathFromTransformation(Type type)
        {
            string[] segments = type.Namespace.Split('.');
            return string.Join("/", segments, 1, segments.Length - 1) + '/' + type.Name + "Aura";
        }

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

        public override int GetTicksPerFrameTimerTick(DBTRPlayer dbtrPlayer) => Information.TicksPerFrameTimerTick;
    }
}

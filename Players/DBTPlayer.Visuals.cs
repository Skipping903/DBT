using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace DBTMod.Players
{
    public sealed partial class DBTPlayer
    {
        public SamplerState GetPlayerSamplerState() => player.mount.Active ? Main.MountedSamplerState : Main.DefaultSamplerState;
    }
}

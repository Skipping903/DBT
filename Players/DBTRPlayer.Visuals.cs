using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace DBTR.Players
{
    public sealed partial class DBTRPlayer
    {
        public SamplerState GetPlayerSamplerState() => player.mount.Active ? Main.MountedSamplerState : Main.DefaultSamplerState;
    }
}

using Terraria;
using Terraria.ModLoader;

namespace DBT.Extensions
{
    public static class BuffCheckExtensions
    {
        public static bool HasBuff<T>(this Player player) where T : ModBuff =>
            player.HasBuff(typeof(T).GetModFromType().BuffType<T>());
    }
}
using Terraria.ModLoader.IO;

namespace DBTR.Players
{
    public sealed partial class DBTRPlayer
    {
        internal void SaveKi(TagCompound tag)
        {
            tag.Add(nameof(Ki), Ki);
            tag.Add(nameof(BaseMaxKi), BaseMaxKi);
        }

        internal void LoadKi(TagCompound tag)
        {
            Ki = tag.GetFloat(nameof(Ki));
            BaseMaxKi = tag.GetFloat(nameof(BaseMaxKi));
        }
    }
}

using Terraria.ModLoader.IO;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
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

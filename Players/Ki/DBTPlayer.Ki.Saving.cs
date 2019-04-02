using Terraria.ModLoader.IO;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        internal void SaveKi(TagCompound tag)
        {
            tag.Add(nameof(Players.DBTPlayer.Ki), Ki);
            tag.Add(nameof(Players.DBTPlayer.BaseMaxKi), BaseMaxKi);
        }

        internal void LoadKi(TagCompound tag)
        {
            Ki = tag.GetFloat(nameof(Players.DBTPlayer.Ki));
            BaseMaxKi = tag.GetFloat(nameof(Players.DBTPlayer.BaseMaxKi));
        }
    }
}

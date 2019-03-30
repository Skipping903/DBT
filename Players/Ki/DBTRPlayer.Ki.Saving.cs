using Terraria.ModLoader.IO;

namespace DBTMod.Players.Ki
{
    public sealed partial class DBTRPlayer
    {
        internal void SaveKi(TagCompound tag)
        {
            tag.Add(nameof(Players.DBTRPlayer.Ki), Ki);
            tag.Add(nameof(Players.DBTRPlayer.BaseMaxKi), BaseMaxKi);
        }

        internal void LoadKi(TagCompound tag)
        {
            Ki = tag.GetFloat(nameof(Players.DBTRPlayer.Ki));
            BaseMaxKi = tag.GetFloat(nameof(Players.DBTRPlayer.BaseMaxKi));
        }
    }
}

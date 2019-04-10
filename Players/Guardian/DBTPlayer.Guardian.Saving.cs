using Terraria.ModLoader.IO;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        internal void SaveGuardian(TagCompound tag)
        {
            tag.Add(nameof(Players.DBTPlayer.BaseHealingBonus), BaseHealingBonus);
        }

        internal void LoadGuardian(TagCompound tag)
        {
            BaseHealingBonus = tag.GetInt(nameof(Players.DBTPlayer.BaseHealingBonus));
        }
    }
}

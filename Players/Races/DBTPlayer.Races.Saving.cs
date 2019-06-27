using DBT.Races;
using Terraria.ModLoader.IO;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        internal void SaveRace(TagCompound tag)
        {
            if (Race == null)
                Race = RaceDefinitionManager.Instance.Terrarian;

            tag.Add(nameof(Race), Race.UnlocalizedName);
        }

        internal void LoadRace(TagCompound tag)
        {
            if (!tag.ContainsKey(nameof(Race)))
                Race = RaceDefinitionManager.Instance.Terrarian;
            else
                Race = RaceDefinitionManager.Instance[tag.GetString(nameof(Race))];
        }
    }
}

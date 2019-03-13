using System.Collections.Generic;
using Terraria.ModLoader.IO;

namespace DBTR.Players
{
    public sealed partial class DBTRPlayer
    {
        public override TagCompound Save()
        {
            TagCompound tag = new TagCompound();

            tag.Add(nameof(PlayerInitialized), PlayerInitialized);

            SaveMastery(tag);

            return tag;
        }

        public override void Load(TagCompound tag)
        {
            PlayerInitialized = tag.GetBool(nameof(PlayerInitialized));

            foreach (KeyValuePair<string, object> kvp in tag)
            {
                LoadMasteryEntry(kvp);
            }
        }
    }
}

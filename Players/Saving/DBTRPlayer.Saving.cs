using System.Collections.Generic;
using DBTR.Transformations;
using Terraria.ModLoader.IO;

namespace DBTR.Players
{
    public sealed partial class DBTRPlayer
    {
        public override TagCompound Save()
        {
            TagCompound tag = new TagCompound();

            SaveMastery(tag);

            return tag;
        }

        public override void Load(TagCompound tag)
        {
            foreach (KeyValuePair<string, object> kvp in tag)
            {
                LoadMasteryEntry(kvp);
            }
        }
    }
}

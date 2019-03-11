using System.Collections.Generic;
using DBTRMod.Transformations;
using Terraria.ModLoader.IO;

namespace DBTRMod.Players
{
    public sealed partial class DBTModPlayer
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

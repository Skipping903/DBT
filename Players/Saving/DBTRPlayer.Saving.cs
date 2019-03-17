using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ModLoader.IO;

namespace DBTR.Players
{
    public sealed partial class DBTRPlayer
    {
        public override TagCompound Save()
        {
            ClearTransformations();

            TagCompound tag = new TagCompound();

            tag.Add(nameof(PlayerInitialized), PlayerInitialized);

            SaveMastery(tag);
            SaveKi(tag);

            return tag;
        }

        public override void Load(TagCompound tag)
        {
            PlayerInitialized = tag.GetBool(nameof(PlayerInitialized));

            LoadMastery(tag);
            LoadKi(tag);
        }
    }
}

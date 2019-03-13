using System.Collections.Generic;
using DBTR.Transformations;
using Terraria.ModLoader.IO;

namespace DBTR.Players
{
    public sealed partial class DBTRPlayer
    {
        public void SaveMastery(TagCompound tag)
        {
            for (int i = 0; i < AcquiredTransformations.Count; i++)
                tag.Add(AcquiredTransformations[i].ToSavableFormat());
        }

        public void LoadMasteryEntry(KeyValuePair<string, object> kvp)
        {
            if (kvp.Key.StartsWith(PlayerTransformation.MASTERY_PREFIX))
                AcquiredTransformations.Add(new PlayerTransformation(TransformationDefinitionManager.Instance[kvp.Key.Substring(PlayerTransformation.MASTERY_PREFIX.Length)], float.Parse(kvp.Value.ToString())));
        }
    }
}

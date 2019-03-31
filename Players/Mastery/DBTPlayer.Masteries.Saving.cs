using System.Collections.Generic;
using DBTMod.Transformations;
using Terraria.ModLoader.IO;

namespace DBTMod.Players
{
    public sealed partial class DBTPlayer
    {
        public void SaveMastery(TagCompound tag)
        {
            foreach (PlayerTransformation playerTransformation in AcquiredTransformations.Values)
                tag.Add(playerTransformation.ToSavableFormat());
        }

        public void LoadMastery(TagCompound tag)
        {
            foreach (KeyValuePair<string, object> kvp in tag)
                LoadMasteryEntry(kvp);
        }

        public void LoadMasteryEntry(KeyValuePair<string, object> kvp)
        {
            if (kvp.Key.StartsWith(PlayerTransformation.MASTERY_PREFIX))
            {
                TransformationDefinition transformation = TransformationDefinitionManager.Instance[kvp.Key.Substring(PlayerTransformation.MASTERY_PREFIX.Length)];

                if (AcquiredTransformations.ContainsKey(transformation)) return;
                AcquiredTransformations.Add(transformation, new PlayerTransformation(transformation, float.Parse(kvp.Value.ToString())));
            }
        }
    }
}

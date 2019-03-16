﻿using System.Collections.Generic;
using DBTR.Transformations;
using Terraria.ModLoader.IO;

namespace DBTR.Players
{
    public sealed partial class DBTRPlayer
    {
        public void SaveMastery(TagCompound tag)
        {
            foreach (PlayerTransformation playerTransformation in AcquiredTransformations.Values)
                tag.Add(playerTransformation.ToSavableFormat());
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
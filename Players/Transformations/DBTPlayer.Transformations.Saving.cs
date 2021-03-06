﻿using System.Collections.Generic;
using DBT.Transformations;
using Terraria.ModLoader.IO;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        private void InitializeTransformations()
        {
            AcquiredTransformations = new Dictionary<TransformationDefinition, PlayerTransformation>();
            ActiveTransformations = new List<TransformationDefinition>();
            SelectedTransformations = new List<TransformationDefinition>();
        }

        private void SaveTransformations(TagCompound tag)
        {
            string[] transformationNames = new string[SelectedTransformations.Count];

            for (int i = 0; i < SelectedTransformations.Count; i++)
                transformationNames[i] = SelectedTransformations[i].UnlocalizedName;

            tag.Add(nameof(SelectedTransformations), string.Join(",", transformationNames));
        }

        private void LoadTransformations(TagCompound tag)
        {
            string[] transformationNames = tag.GetString(nameof(SelectedTransformations)).Split(',');

            for (int i = 0; i < transformationNames.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(transformationNames[i])) continue;
                SelectedTransformations.Add(TransformationDefinitionManager.Instance[transformationNames[i]]);
            }
        }
    }
}
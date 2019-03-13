using System;
using System.Collections.Generic;
using DBTR.Transformations;

namespace DBTR.Players
{
    public sealed partial class DBTRPlayer
    {
        public void ListenForTransformations()
        {
            // TODO Add transformation management code.
        }


        public void ForAllActiveTransformations(Action<PlayerTransformation> action)
        {
            for (int i = 0; i < ActiveTransformations.Count; i++)
                action(ActiveTransformations[i]);
        }


        public void Transform(TransformationDefinition definition)
        {
            for (int i = 0; i < ActiveTransformations.Count; i++)
                if (ActiveTransformations[i].Definition == definition)
                    return;

            ActiveTransformations.Add(new PlayerTransformation(definition));
            player.AddBuff(mod.GetBuff(definition.BuffType.Name).Type, definition.Duration);
        }


        public void Untransform(PlayerTransformation transformation)
        {
            if (!ActiveTransformations.Contains(transformation)) return;

            ActiveTransformations.Remove(transformation);
        }

        public void Untransform(TransformationDefinition definition)
        {
            for (int i = ActiveTransformations.Count - 1; i >= 0; i--)
            {
                PlayerTransformation transformation = ActiveTransformations[i];

                if (transformation.Definition == definition)
                {
                    ActiveTransformations.Remove(transformation);
                    player.ClearBuff(mod.GetBuff(definition.BuffType.Name).Type);
                }
            }
        }


        public void ClearTransformations()
        {
            for (int i = 0; i < ActiveTransformations.Count; i++)
                ActiveTransformations.Clear();
        }


        public bool IsTransformed() => ActiveTransformations.Count > 0;

        public bool IsTransformed(TransformationDefinition definition)
        {
            for (int i = 0; i < ActiveTransformations.Count; i++)
                if (ActiveTransformations[i].Definition == definition)
                    return true;

            return false;
        }

        public bool HasAcquiredTransformation(TransformationDefinition definition)
        {
            for (int i = 0; i < AcquiredTransformations.Count; i++)
                if (AcquiredTransformations[i].Definition == definition)
                    return true;

            return false;
        }


        public PlayerTransformation GetFirstTransformation()
        {
            if (ActiveTransformations.Count == 0) return null;

            return ActiveTransformations[0];
        }


        public List<PlayerTransformation> AcquiredTransformations { get; } = new List<PlayerTransformation>();

        public List<PlayerTransformation> ActiveTransformations { get; } = new List<PlayerTransformation>();
    }
}

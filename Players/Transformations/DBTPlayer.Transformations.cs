using System;
using System.Collections.Generic;
using DBT.Network;
using DBT.Transformations;
using Terraria;
using Terraria.ID;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        public void ListenForTransformations()
        {
            // TODO Add transformation management code.
        }


        public void ForAllActiveTransformations(Action<TransformationDefinition> action)
        {
            for (int i = 0; i < ActiveTransformations.Count; i++)
                action(ActiveTransformations[i]);
        }

        public void ForAllAcquiredTransformations(Action<PlayerTransformation> action)
        {
            foreach (PlayerTransformation playerTransformation in AcquiredTransformations.Values)
                action(playerTransformation);
        }


        public void AcquireAndTransform(TransformationDefinition definition)
        {
            Acquire(definition);
            Transform(definition);
        }

        public void Acquire(TransformationDefinition definition)
        {
            if (AcquiredTransformations.ContainsKey(definition)) return;

            AcquiredTransformations.Add(definition, new PlayerTransformation(definition));
            definition.OnPlayerAcquiredTransformation(this);
        }

        public void Transform(TransformationDefinition definition)
        {
            for (int i = 0; i < ActiveTransformations.Count; i++)
                if (ActiveTransformations[i] == definition)
                    return;

            ActiveTransformations.Add(definition);
            player.AddBuff(mod.GetBuff(definition.BuffType.Name).Type, definition.Duration);

            if (Main.netMode == NetmodeID.MultiplayerClient && player.whoAmI == Main.myPlayer)
                NetworkPacketManager.Instance.PlayerTransformedPacket.SendPacketToServer(player.whoAmI, (byte)player.whoAmI, definition.UnlocalizedName);
        }


        public void Untransform(TransformationBuff transformation) => Untransform(transformation.Definition);

        public void Untransform(PlayerTransformation transformation)
        {
            if (!ActiveTransformations.Contains(transformation.Definition)) return;

            Untransform(transformation.Definition);
        }

        public void Untransform(TransformationDefinition definition)
        {
            for (int i = ActiveTransformations.Count - 1; i >= 0; i--)
            {
                TransformationDefinition transformation = ActiveTransformations[i];

                if (transformation == definition)
                {
                    ActiveTransformations.Remove(transformation);
                    player.ClearBuff(mod.GetBuff(definition.BuffType.Name).Type);
                }
            }
        }


        public void ClearTransformations()
        {
            for (int i = ActiveTransformations.Count - 1; i >= 0 ; i--)
                Untransform(ActiveTransformations[i]);
        }


        public bool IsTransformed() => ActiveTransformations.Count > 0;

        public bool IsTransformed(TransformationDefinition definition)
        {
            for (int i = 0; i < ActiveTransformations.Count; i++)
                if (ActiveTransformations.Contains(definition))
                    return true;

            return false;
        }

        public bool IsTransformed(TransformationBuff buff)
        {
            if (ActiveTransformations.Count == 0) return false;

            for (int i = 0; i < ActiveTransformations.Count; i++)
            {
                Type buffType = buff.GetType();
                bool isBuff = ActiveTransformations[i].BuffType.IsAssignableFrom(buffType);

                if (isBuff)
                    return true;
            }

            return false;
        }


        public bool HasAcquiredTransformation(TransformationDefinition definition)
        {
            for (int i = 0; i < AcquiredTransformations.Count; i++)
                if (AcquiredTransformations.ContainsKey(definition))
                    return true;

            return false;
        }


        public PlayerTransformation GetFirstTransformation()
        {
            if (ActiveTransformations.Count == 0) return null;

            return AcquiredTransformations[ActiveTransformations[0]];
        }


        public Dictionary<TransformationDefinition, PlayerTransformation> AcquiredTransformations { get; } = new Dictionary<TransformationDefinition, PlayerTransformation>();

        public List<TransformationDefinition> ActiveTransformations { get; } = new List<TransformationDefinition>();

        public PlayerTransformation FirstTransformation { get; private set; }

        public TransformationDefinition SelectedTransformation { get; set; }
    }
}

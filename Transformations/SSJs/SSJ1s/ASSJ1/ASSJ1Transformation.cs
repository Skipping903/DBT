﻿using DBTR.Players;
using DBTR.Transformations.SSJs.SSJ1s.SSJ1;

namespace DBTR.Transformations.SSJs.SSJ1s.ASSJ1
{
    public sealed class ASSJ1Transformation : TransformationDefinition
    {
        public ASSJ1Transformation(params TransformationDefinition[] parents) : base(
            "ASSJ1", "Ascended Super Saiyan", typeof(ASSJ1TransformationBuff),
            1.75f, 1.75f, 3, 70f, 35f,
            new SSJ1Appearance(), parents: parents)
        {
        }

        public override void OnPlayerMasteryGain(DBTRPlayer dbtrPlayer, float gain, float currentMastery)
        {
            dbtrPlayer.GainMastery(TransformationDefinitionManager.Instance.SSJ1, gain);
        }
    }

    public sealed class ASSJ1TransformationBuff : TransformationBuff
    {
        public ASSJ1TransformationBuff() : base(TransformationDefinitionManager.Instance.ASSJ1)
        {
        }
    }
}

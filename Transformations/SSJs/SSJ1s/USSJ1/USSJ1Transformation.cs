﻿using DBTR.Players;
using DBTR.Transformations.SSJs.SSJ1s.SSJ1;

namespace DBTR.Transformations.SSJs.SSJ1s.USSJ1
{
    public sealed class USSJ1Transformation : TransformationDefinition
    {
        public USSJ1Transformation(params TransformationDefinition[] parents) : base(
            "USSJ1", "Ultra Super Saiyan", typeof(USSJ1TransformationBuff),
            1.90f, 1.45f, 5, 90f, 45f,
            new SSJ1Appearance(), parents: parents)
        {
        }

        public override void OnPlayerMasteryGain(DBTRPlayer dbtrPlayer, float gain, float currentMastery)
        {
            dbtrPlayer.GainMastery(TransformationDefinitionManager.Instance.SSJ1, gain);
        }
    }

    public sealed class USSJ1TransformationBuff : TransformationBuff
    {
        public USSJ1TransformationBuff() : base(TransformationDefinitionManager.Instance.USSJ1)
        {
        }
    }
}
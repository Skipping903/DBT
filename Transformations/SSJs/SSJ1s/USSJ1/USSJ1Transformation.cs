﻿using DBT.Players;
using DBT.Transformations.SSJs.SSJ1s.SSJ1;

namespace DBT.Transformations.SSJs.SSJ1s.USSJ1
{
    public sealed class USSJ1Transformation : TransformationDefinition
    {
        public USSJ1Transformation(params TransformationDefinition[] parents) : base(
            "USSJ1", "Ultra Super Saiyan", typeof(USSJ1TransformationBuff),
            1.90f, 1.45f, 5, 
            new TransformationDrain(1.5f, 0.75f), 
            new SSJ1Appearance(),
            displaysInMenu: false, parents: parents)
        {
        }

        public override void OnPlayerMasteryGain(DBTPlayer dbtPlayer, float gain, float currentMastery)
        {
            dbtPlayer.GainMastery(TransformationDefinitionManager.Instance.SSJ1, gain);
        }
    }

    public sealed class USSJ1TransformationBuff : TransformationBuff
    {
        public USSJ1TransformationBuff() : base(TransformationDefinitionManager.Instance.USSJ1)
        {
        }
    }
}

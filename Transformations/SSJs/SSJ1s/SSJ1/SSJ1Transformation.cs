using DBTR.Auras;
using DBTR.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DBTR.Transformations.SSJs.SSJ1s.SSJ1
{
    public sealed class SSJ1Transformation : TransformationDefinition
    {
        public SSJ1Transformation() : base(
            "SSJ1", "Super Saiyan", typeof(SSJ1TransformationBuff),
            1.5f, 1.5f, 2, 60f, 30f,
            new SSJ1Appearance())
        {
        }

        public override float GetMaxMastery(DBTRPlayer dbtrPlayer)
        {
            if (!dbtrPlayer.AcquiredTransformations.ContainsKey(TransformationDefinitionManager.Instance.SSJG) ||
                dbtrPlayer.AcquiredTransformations[TransformationDefinitionManager.Instance.SSJG].CurrentMastery < 1f)
                return BaseMaxMastery;

            return 2f;
        }

        public override void OnPlayerMasteryGain(DBTRPlayer dbtrPlayer, float gain, float currentMastery)
        {
            if (currentMastery >= 0.5f && !dbtrPlayer.HasAcquiredTransformation(TransformationDefinitionManager.Instance.ASSJ1))
                dbtrPlayer.Acquire(TransformationDefinitionManager.Instance.ASSJ1);

            if (currentMastery >= 0.75f && !dbtrPlayer.HasAcquiredTransformation(TransformationDefinitionManager.Instance.USSJ1))
                dbtrPlayer.Acquire(TransformationDefinitionManager.Instance.USSJ1);
        }
    }

    public sealed class SSJ1TransformationBuff : TransformationBuff
    {
        public SSJ1TransformationBuff() : base(TransformationDefinitionManager.Instance.SSJ1)
        {
        }
    }

    public sealed class SSJ1Appearance : TransformationAppearance
    {
        public SSJ1Appearance() : base(
            new AuraAppearance(new AuraAnimationInformation(typeof(SSJ1Transformation), 4, 3, BlendState.Additive, 1f, true),
                new LightingAppearance(new float[] { 1.25f, 1.25f, 0f })), 
            new HairAppearance(Color.White))
        {
        }
    }
}

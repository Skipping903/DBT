using DBTMod.Auras;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DBTMod.Transformations.SSJGs.SSJBs.SSJB
{
    public sealed class SSJBTransformation : TransformationDefinition
    {
        public SSJBTransformation(params TransformationDefinition[] parents) : base(
            "SSJB", "Super Saiyan Blue", typeof(SSJBTransformationBuff),
            4.5f, 4.5f, 32, 300f, 150f,
            new SSJBAppearance(), parents: parents)
        {
        }
    }

    public sealed class SSJBTransformationBuff : TransformationBuff
    {
        public SSJBTransformationBuff() : base(TransformationDefinitionManager.Instance.SSJB)
        {
        }
    }

    public sealed class SSJBAppearance : TransformationAppearance
    {
        public SSJBAppearance() : base(
            new AuraAppearance(new AuraAnimationInformation(typeof(SSJBTransformation), 8, 3, BlendState.Additive, 1f, true),
                new LightingAppearance(new float[] { 0.38f, 0.24f, 1.25f })),
            new HairAppearance(Color.White))
        {
        }
    }
}

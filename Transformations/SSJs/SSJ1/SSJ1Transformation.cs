namespace DBTRMod.Transformations.SSJs.SSJ1
{
    public sealed class SSJ1Transformation : TransformationDefinition
    {
        public SSJ1Transformation() : base(
            "SSJ1", "Super Saiyan", typeof(SSJ1Buff),
            1.5f, 1.5f, 2, 60f, 30f)
        {
        }
    }

    public sealed class SSJ1Buff : TransformationBuff
    {
        public SSJ1Buff() : base(TransformationDefinitionManager.Instance.SSJ1Definition)
        {
        }
    }
}

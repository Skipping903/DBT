namespace DBT.Transformations
{
    public struct TransformationDrain
    {
        public readonly float
            baseUnmasteredKiDrain,
            baseMasteredKiDrain,
            baseUnmasteredHealthDrain,
            baseMasteredHealthDrain,

            maxTransformationDrainMultiplier,
            multiplierPerStep;

        public readonly int transformationStepDelay;

        public TransformationDrain(float baseUnmasteredKiDrain, float baseMasteredKiDrain, float baseUnmasteredHealthDrain = 0f, float baseMasteredHealthDrain = 0f, 
            int transformationStepDelay = 300, float maxTransformationDrainMultiplier = 3f, float multiplierPerStep = 0.5f)
        {
            this.baseUnmasteredKiDrain = baseUnmasteredKiDrain;
            this.baseMasteredKiDrain = baseMasteredKiDrain;
            this.baseUnmasteredHealthDrain = baseUnmasteredHealthDrain;
            this.baseMasteredHealthDrain = baseMasteredHealthDrain;

            this.transformationStepDelay = transformationStepDelay;
            this.maxTransformationDrainMultiplier = maxTransformationDrainMultiplier;
            this.multiplierPerStep = multiplierPerStep;
        }
    }
}
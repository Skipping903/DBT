using DBTR.Transformations;

namespace DBTR.Players
{
    public sealed partial class DBTRPlayer
    {
        public void GainMastery(float gain)
        {
            for (int i = 0; i < ActiveTransformations.Count; i++)
                GainMastery(ActiveTransformations[i], gain);
        }

        public void GainMastery(TransformationDefinition definition, float gain)
        {
            PlayerTransformation playerTransformation = AcquiredTransformations[definition];
            playerTransformation.CurrentMastery += gain;

            definition.OnPlayerMasteryGain(this, gain, playerTransformation.CurrentMastery);
        }
    }
}

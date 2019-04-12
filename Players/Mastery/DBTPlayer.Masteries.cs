using DBT.Transformations;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        public void GainMastery(float gain)
        {
            for (int i = 0; i < ActiveTransformations.Count; i++)
                GainMastery(ActiveTransformations[i], gain);
        }

        public void GainMastery(TransformationDefinition definition, float gain)
        {
            PlayerTransformation playerTransformation = AcquiredTransformations[definition];
            float maxMastery = playerTransformation.Definition.GetMaxMastery(this);

            if (playerTransformation.CurrentMastery >= maxMastery)
                return;

            if (playerTransformation.CurrentMastery + gain > maxMastery)
                gain = maxMastery - playerTransformation.CurrentMastery;

            if (Trait != null)
                Trait.OnPlayerMasteryGained(this, ref gain, playerTransformation.CurrentMastery);

            playerTransformation.CurrentMastery += gain;
            definition.OnPlayerMasteryGain(this, gain, playerTransformation.CurrentMastery);
        }
    }
}

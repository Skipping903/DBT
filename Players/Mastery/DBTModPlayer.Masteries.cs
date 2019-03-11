using DBTRMod.Transformations;

namespace DBTRMod.Players
{
    public sealed partial class DBTModPlayer
    {
        public void GainMastery(float gain)
        {
            for (int i = 0; i < ActiveTransformations.Count; i++)
                GainMastery(ActiveTransformations[i], gain);
        }

        public void GainMastery(PlayerTransformation definition, float gain) => definition.OnPlayerMasteryGain(this, gain);
    }
}

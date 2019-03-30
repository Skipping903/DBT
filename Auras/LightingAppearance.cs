using DBTMod.Players;

namespace DBTMod.Auras
{
    public class LightingAppearance
    {
        public LightingAppearance(float[] baseRGBLightingRadiuses)
        {
            BaseRGBLightingRadiuses = baseRGBLightingRadiuses;
        }

        public virtual float[] GetRGBLightingRadiuses(DBTRPlayer dbtrPlayer)
        {
            if (!dbtrPlayer.IsCharging)
                return BaseRGBLightingRadiuses;

            return new float[]
            {
                BaseRGBLightingRadiuses[0] * 1.3f,
                BaseRGBLightingRadiuses[1] * 1.3f,
                BaseRGBLightingRadiuses[2] * 1.3f
            };
        }

        public float[] BaseRGBLightingRadiuses { get; }
    }
}

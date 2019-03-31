using System;

namespace DBTMod.Players
{
    public sealed partial class DBTPlayer
    {
        internal void ResetEffectsKi()
        {
            KiDamage = 1;
            MaxKiMultiplier = 1;
            MaxKiModifier = 1;
        }


        public float KiDamage { get; set; } = 1;

        public float Ki { get; private set; }


        public float BaseMaxKi { get; private set; }

        public float MaxKiMultiplier { get; set; } = 1;

        public float MaxKiModifier { get; set; } = 1;

        public int MaxKi => (int)(Math.Round(BaseMaxKi * MaxKiMultiplier) + MaxKiModifier);
    }
}

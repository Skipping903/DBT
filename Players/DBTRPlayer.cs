using System;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace DBTR.Players
{
    public sealed partial class DBTRPlayer : ModPlayer
    {
        public void ModifyKi(float kiAmount)
        {
            // TODO Add mastery for being in a form, if need be.

        }
        

        #region Ki

        public float KiMultiplier { get; set; }

        #region Current Ki

        public int Ki { get; private set; }

        #endregion

        #region Max Ki

        public int BaseMaxKi { get; private set; }

        public float MaxKiMultiplier { get; private set; } = 1;

        public int MaxKi => (int)Math.Round(BaseMaxKi * MaxKiMultiplier);

        #endregion

        #endregion

        public bool PlayerInitialized { get; private set; }
    }
}

using System;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace DBTRMod.Players
{
    public sealed partial class DBTModPlayer : ModPlayer
    {
        internal const string DBTMOD_PREFIX = "DBTMOD_";

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

        public float MaxKiMultiplier { get; private set; }

        public int MaxKi => (int)Math.Round(BaseMaxKi * MaxKiMultiplier);

        #endregion

        #endregion

    }
}

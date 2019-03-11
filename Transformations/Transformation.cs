using DBTMod;
using Microsoft.Xna.Framework;

namespace DBTMod.Transformations
{
    public abstract class Transformation : IHasUnlocalizedName
    {
        protected Transformation(string unlocalizedName)
        {
            UnlocalizedName = unlocalizedName;
        }

        #region Properties

        public string UnlocalizedName { get; }


        #region Statistics

        #region Multipliers

        public float BaseDamageMultiplier { get; }

        public float BaseSpeedMultiplier { get; }

        public float BaseDefenseMultiplier { get; }

        #endregion



        #endregion

        #endregion
    }
}

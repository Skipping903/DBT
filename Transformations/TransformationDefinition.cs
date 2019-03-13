using System;
using DBTR.Players;

namespace DBTR.Transformations
{
    public abstract class TransformationDefinition : IHasUnlocalizedName
    {
        internal const int TRANSFORMATION_LONG_DURATION = 6666666;

        protected TransformationDefinition(string unlocalizedName, string displayName, Type buffType,
            float baseDamageMultiplier, float baseSpeedMultiplier, int baseDefenseAdditive, float unmasteredKiDrain, float masteredKiDrain,
            TransformationAppearance appearance,
            bool masterable = true, float maxMastery = 1f, 
            int duration = TRANSFORMATION_LONG_DURATION,
            params TransformationDefinition[] parents)
        {
            UnlocalizedName = unlocalizedName;
            DisplayName = displayName;

            BuffType = buffType;

            BaseDamageMultiplier = baseDamageMultiplier;
            BaseSpeedMultiplier = baseSpeedMultiplier;
            BaseDefenseAdditive = baseDefenseAdditive;

            UnmasteredKiDrain = unmasteredKiDrain;
            MasteredKiDrain = masteredKiDrain;

            Appearance = appearance;

            Mastereable = masterable;
            MaxMastery = maxMastery;

            Duration = duration;
        }

        #region Methods

        #region Player Hooks

        public virtual void OnPlayerTransformed(PlayerTransformation transformation) { }

        public virtual void OnPlayerMasteryGain(DBTRPlayer dbtrPlayer, float gain, float currentMastery) { }

        public virtual void OnPlayerDied(DBTRPlayer dbtrPlayer, double damage, bool pvp) { }

        #endregion

        #region Multipliers

        public virtual float GetDamageMultiplier(DBTRPlayer dbtrPlayer) => BaseDamageMultiplier;

        public virtual float GetSpeedMultiplier(DBTRPlayer dbtrPlayer) => BaseSpeedMultiplier;

        #endregion

        #region Additive

        public virtual int GetDefenseAdditive(DBTRPlayer dbtrPlayer) => BaseDefenseAdditive;

        #endregion

        #region Ki Drain

        public float GetUnmasteredKiDrain(DBTRPlayer dbtrPlayer) => UnmasteredKiDrain;

        public float GetMasteredKiDrain(DBTRPlayer dbtrPlayer) => MasteredKiDrain;

        #endregion

        #endregion

        #region Properties

        public string UnlocalizedName { get; }

        public string DisplayName { get; }

        public Type BuffType { get; }

        #region Statistics

        #region Multipliers

        public virtual float BaseDamageMultiplier { get; }

        public virtual float BaseSpeedMultiplier { get; }

        #endregion

        #region Additives

        public virtual int BaseDefenseAdditive { get; }

        #endregion

        #region Ki Drain

        public virtual float UnmasteredKiDrain { get; }

        public virtual float MasteredKiDrain { get; }

        #endregion

        #region Mastery

        public virtual bool Mastereable { get; }

        public virtual float MaxMastery { get; }

        #endregion

        #endregion

        public virtual TransformationAppearance Appearance { get; }

        public int Duration { get; }

        #endregion
    }
}

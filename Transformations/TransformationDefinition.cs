using System;
using DBTRMod.Players;

namespace DBTRMod.Transformations
{
    public abstract class TransformationDefinition : IHasUnlocalizedName
    {
        internal const int TRANSFORMATION_LONG_DURATION = 6666666;

        protected TransformationDefinition(string unlocalizedName, string displayName, Type buffType,
            float baseDamageMultiplier, float baseSpeedMultiplier, int baseDefenseAdditive, float unmasteredKiDrain, float masteredKiDrain, bool masterable = true, float maxMastery = 1f,
            int duration = TRANSFORMATION_LONG_DURATION)
        {
            UnlocalizedName = unlocalizedName;
            DisplayName = displayName;

            BuffType = buffType;

            BaseDamageMultiplier = baseDamageMultiplier;
            BaseSpeedMultiplier = baseSpeedMultiplier;
            BaseDefenseAdditive = baseDefenseAdditive;

            UnmasteredKiDrain = unmasteredKiDrain;
            MasteredKiDrain = masteredKiDrain;

            Mastereable = masterable;
            MaxMastery = maxMastery;

            Duration = duration;
        }

        #region Methods

        #region Player Hooks

        public virtual void OnPlayerTransformed(PlayerTransformation transformation) { }

        public virtual void OnPlayerMasteryGain(DBTModPlayer player, float gain, float currentMastery) { }

        public virtual void OnPlayerDied(DBTModPlayer player, double damage, bool pvp) { }

        #endregion

        #region Multipliers

        public virtual float GetDamageMultiplier(DBTModPlayer player) => BaseDamageMultiplier;

        public virtual float GetSpeedMultiplier(DBTModPlayer player) => BaseSpeedMultiplier;

        #endregion

        #region Additive

        public virtual int GetDefenseAdditive(DBTModPlayer player) => BaseDefenseAdditive;

        #endregion

        #region Ki Drain

        public float GetUnmasteredKiDrain(DBTModPlayer player) => UnmasteredKiDrain;

        public float GetMasteredKiDrain(DBTModPlayer player) => MasteredKiDrain;

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

        public int Duration { get; }

        #endregion
    }
}

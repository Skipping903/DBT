using System;
using DBT.Dynamicity;
using DBT.Players;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader.IO;

namespace DBT.Transformations
{
    public abstract class TransformationDefinition : IHasUnlocalizedName, IHasParents<TransformationDefinition>
    {
        internal const int TRANSFORMATION_LONG_DURATION = 6666666;

        protected TransformationDefinition(string unlocalizedName, string displayName, Type buffType,
            float baseDamageMultiplier, float baseSpeedMultiplier, int baseDefenseAdditive, float unmasteredKiDrain, float masteredKiDrain,
            TransformationAppearance appearance,
            bool masterable = true, float maxMastery = 1f,
            int duration = TRANSFORMATION_LONG_DURATION, bool displaysInMenu = true,
            bool anyParents = false, params TransformationDefinition[] parents)
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
            BaseMaxMastery = maxMastery;

            Duration = duration;

            DisplayInMenu = displaysInMenu;

            AnyParents = anyParents;
            Parents = parents;
        }


        #region Methods

        #region Player Hooks Active

        public virtual void OnPlayerTransformed(PlayerTransformation transformation) { }

        public virtual void OnPlayerMasteryGain(DBTPlayer dbtPlayer, float gain, float currentMastery) { }

        public virtual void OnActivePlayerDied(DBTPlayer dbtPlayer, double damage, bool pvp, PlayerDeathReason damageSource) { }

        public virtual void OnActivePlayerKilledNPC(DBTPlayer dbtPlayer, NPC npc) { }

        #endregion

        #region Player Hooks Acquired

        public virtual void OnPlayerLoading(DBTPlayer dbtPlayer, TagCompound tag) { }

        public virtual void OnPlayerSaving(DBTPlayer dbtPlayer, TagCompound tag) { }

        public virtual void OnPlayerAcquiredTransformation(DBTPlayer dbtPlayer) { }

        #endregion

        #region Player Hooks PreAcquired

        public virtual void OnPreAcquirePlayerKilledNPC(DBTPlayer dbtPlayer, NPC npc) { }

        public virtual void OnPreAcquirePlayerDied(DBTPlayer dbtPlayer, double damage, bool pvp, PlayerDeathReason damageSource) { }

        #endregion


        #region Access

        /// <summary>Called in special cases when the mod needs to know wether or not, regardless of the player, this transformation should work.</summary>
        /// <returns></returns>
        public virtual bool CheckPrePlayerConditions() => true;

        public bool HasParents(DBTPlayer dbtPlayer)
        {
            for (int i = 0; i < Parents.Length; i++)
            {
                if (AnyParents && dbtPlayer.AcquiredTransformations.ContainsKey(Parents[i]))
                    return true;

                if (!AnyParents && !dbtPlayer.AcquiredTransformations.ContainsKey(Parents[i]))
                    return false;
            }

            return true;
        }

        public bool CanUnlock(DBTPlayer dbtPlayer) => HasParents(dbtPlayer);

        /// <summary>Checks wether or not the transformation is part of the character menu. If not overriden, uses the same value as <see cref="CheckPrePlayerConditions"/>.</summary>
        /// <param name="dbtPlayer"></param>
        /// <returns></returns>
        public bool DoesDisplayInCharacterMenu(DBTPlayer dbtPlayer) => DisplayInMenu && CheckPrePlayerConditions();

        #endregion

        #region Multipliers

        public virtual float GetDamageMultiplier(DBTPlayer dbtPlayer) => BaseDamageMultiplier;

        public virtual float GetSpeedMultiplier(DBTPlayer dbtPlayer) => BaseSpeedMultiplier;

        #endregion

        #region Additive

        public virtual int GetDefenseAdditive(DBTPlayer dbtPlayer) => BaseDefenseAdditive;

        #endregion

        #region Ki Drain

        public float GetUnmasteredKiDrain(DBTPlayer dbtPlayer) => UnmasteredKiDrain;

        public float GetMasteredKiDrain(DBTPlayer dbtPlayer) => MasteredKiDrain;

        #endregion

        #region Mastery

        public float GetCurrentMastery(DBTPlayer dbtPlayer)
        {
            if (dbtPlayer.HasAcquiredTransformation(this))
                return dbtPlayer.AcquiredTransformations[this].CurrentMastery;

            return 0f;
        }

        public virtual float GetMaxMastery(DBTPlayer dbtPlayer) => BaseMaxMastery;

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

        public float BaseMaxMastery { get; }

        #endregion

        #endregion

        public virtual TransformationAppearance Appearance { get; }


        public int Duration { get; }

        public bool DisplayInMenu { get; }


        public bool AnyParents { get; }

        public TransformationDefinition[] Parents { get; }

        #endregion
    }
}

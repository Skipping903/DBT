using System;
using System.Collections.Generic;
using DBT.Auras;
using DBT.Players;
using DBT.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader.IO;

namespace DBT.Transformations.Developers.Webmilio
{
    public sealed class SoulStealerTransformation : TransformationDefinition
    {
        private const string
            SOULSTEALER_PREFIX = "SoulStealer_",
            SOULPOWER_TAG = SOULSTEALER_PREFIX + "SoulPowerCount",
            DIMINISHINGRETURNS_MOBCOUNT_PREFIX = SOULSTEALER_PREFIX + "DiminishingReturns_MobCount";

        public SoulStealerTransformation() : base(
            "SoulStealer", "Soul Stealer", typeof(SoulStealerTransformationBuff),
            1f, 1f, 0, 0f, 0f,
            new SoulStealerAppearance())
        {
        }

        #region Loading/Saving

        public override void OnPlayerLoading(DBTPlayer dbtPlayer, TagCompound tag)
        {
            if (!CheckPrePlayerConditions())
            {
                if (dbtPlayer.HasAcquiredTransformation(this))
                    dbtPlayer.AcquiredTransformations.Remove(this);

                return;
            }

            if (!dbtPlayer.HasAcquiredTransformation(this))
                dbtPlayer.Acquire(this);

            PlayerTransformation playerTransformation = dbtPlayer.AcquiredTransformations[this];

            if (playerTransformation != null)
            {
                DefaultSetup(playerTransformation);
                Dictionary<string, int> mobCount = GetDiminishingReturnsDictionary(dbtPlayer);

                foreach (KeyValuePair<string, object> kvp in tag)
                {
                    if (!kvp.Key.StartsWith(DIMINISHINGRETURNS_MOBCOUNT_PREFIX)) continue;
                    
                    mobCount.Add(kvp.Key.Substring(DIMINISHINGRETURNS_MOBCOUNT_PREFIX.Length), int.Parse(kvp.Value.ToString()));
                }

                SetSoulPower(dbtPlayer, tag.GetFloat(SOULPOWER_TAG));
            }
        }

        public override void OnPlayerSaving(DBTPlayer dbtPlayer, TagCompound tag)
        {
            tag.Add(SOULPOWER_TAG, GetSoulPower(dbtPlayer));
            PlayerTransformation playerTransformation = dbtPlayer.AcquiredTransformations[this];

            Dictionary<string, int> mobCount = (Dictionary<string, int>)playerTransformation.ExtraInformation[DIMINISHINGRETURNS_MOBCOUNT_PREFIX];

            foreach (KeyValuePair<string, int> kvp in mobCount)
                tag.Add(DIMINISHINGRETURNS_MOBCOUNT_PREFIX + kvp.Key, kvp.Value);
        }

        #endregion

        public override void OnPlayerAcquiredTransformation(DBTPlayer dbtPlayer) => DefaultSetup(dbtPlayer.AcquiredTransformations[this]);

        private void DefaultSetup(PlayerTransformation playerTransformation)
        {
            if (!playerTransformation.ExtraInformation.ContainsKey(SOULPOWER_TAG))
                playerTransformation.ExtraInformation.Add(SOULPOWER_TAG, 0f);

            if (!playerTransformation.ExtraInformation.ContainsKey(DIMINISHINGRETURNS_MOBCOUNT_PREFIX))
                playerTransformation.ExtraInformation.Add(DIMINISHINGRETURNS_MOBCOUNT_PREFIX, new Dictionary<string, int>());
        }


        public override void OnActivePlayerKilledNPC(DBTPlayer dbtPlayer, NPC npc)
        {
            Dictionary<string, int> dimishingReturnsDictionary = GetDiminishingReturnsDictionary(dbtPlayer);
            string npcType = npc.TypeName.Replace(" ", "");

            if (!dimishingReturnsDictionary.ContainsKey(npcType))
                dimishingReturnsDictionary.Add(npcType, 1);

            dimishingReturnsDictionary[npcType]++;
            AddSoulPower(dbtPlayer, npc);
        }

        public override float GetDamageMultiplier(DBTPlayer dbtPlayer)
        {
            return BaseDamageMultiplier + GetSoulPower(dbtPlayer) / 1100;
        }

        public override float GetSpeedMultiplier(DBTPlayer dbtPlayer)
        {
            return BaseSpeedMultiplier + GetSoulPower(dbtPlayer) / 1100;
        }

        public override int GetDefenseAdditive(DBTPlayer dbtPlayer)
        {
            return (int)Math.Round(GetSoulPower(dbtPlayer) / 250f);
        }


        public override bool CheckPrePlayerConditions() => SteamHelper.SteamID64 == "76561198046878487";


        public float GetSoulPower(DBTPlayer dbtPlayer) => (float)dbtPlayer.AcquiredTransformations[this].ExtraInformation[SOULPOWER_TAG];

        public void SetSoulPower(DBTPlayer dbtPlayer, float multiplier) => dbtPlayer.AcquiredTransformations[this].ExtraInformation[SOULPOWER_TAG] = multiplier;

        public void AddSoulPower(DBTPlayer dbtPlayer, NPC npc)
        {
            float gain = (npc.lifeMax / (float)dbtPlayer.player.statLifeMax2) / GetMobKilledCount(dbtPlayer, npc.TypeName.Replace(" ", ""));

            if (npc.boss && gain < 110)
                gain = 110;
            else if (gain < 1)
                gain = 1;
            else if (npc.boss)
                gain *= 2;

            SetSoulPower(dbtPlayer, GetSoulPower(dbtPlayer) + gain);
        }

        private int GetMobKilledCount(DBTPlayer dbtPlayer, string npcTypeName) => GetDiminishingReturnsDictionary(dbtPlayer)[npcTypeName];

        public Dictionary<string, int> GetDiminishingReturnsDictionary(DBTPlayer dbtPlayer) => (Dictionary<string, int>)dbtPlayer.AcquiredTransformations[this].ExtraInformation[DIMINISHINGRETURNS_MOBCOUNT_PREFIX];
    }

    public sealed class SoulStealerTransformationBuff : TransformationBuff
    {
        public SoulStealerTransformationBuff() : base(TransformationDefinitionManager.Instance.SoulStealer)
        {
        }

        public override string BuildDefaultTooltip(DBTPlayer player)
        {
            return base.BuildDefaultTooltip(player) + "\nSoul Power: " + ((SoulStealerTransformation)Definition).GetSoulPower(player);
        }
    }

    public sealed class SoulStealerAppearance : TransformationAppearance
    {
        public SoulStealerAppearance() : base(
            new AuraAppearance(new AuraAnimationInformation(typeof(SoulStealerTransformation), 8, 3, BlendState.Additive, 1f, true), 
                new LightingAppearance(new float[] { 0.85f, 0f, 1.30f })),
            new HairAppearance(Color.Fuchsia))
        {
        }
    }
}

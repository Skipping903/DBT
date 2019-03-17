using DBTR.Auras;
using DBTR.Players;
using DBTR.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader.IO;

namespace DBTR.Transformations.Developers.Webmilio
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

        public override void OnPlayerLoading(DBTRPlayer dbtrPlayer, TagCompound tag)
        {
            if (!CheckPrePlayerConditions())
            {
                if (dbtrPlayer.HasAcquiredTransformation(this))
                    dbtrPlayer.AcquiredTransformations.Remove(this);

                return;
            }

            if (!dbtrPlayer.HasAcquiredTransformation(this))
                dbtrPlayer.Acquire(this);

            PlayerTransformation playerTransformation = dbtrPlayer.AcquiredTransformations[this];

            if (playerTransformation != null)
            {
                DefaultSetup(playerTransformation);

                foreach (KeyValuePair<string, object> kvp in tag)
                {
                    if (!kvp.Key.StartsWith(DIMINISHINGRETURNS_MOBCOUNT_PREFIX)) continue;

                    ((Dictionary<string, int>)playerTransformation.ExtraInformation[DIMINISHINGRETURNS_MOBCOUNT_PREFIX]).Add(kvp.Key.Substring(DIMINISHINGRETURNS_MOBCOUNT_PREFIX.Length), int.Parse(kvp.Value.ToString()));
                }

                SetSoulPower(dbtrPlayer, tag.GetFloat(SOULPOWER_TAG));
            }
        }

        public override void OnPlayerSaving(DBTRPlayer dbtrPlayer, TagCompound tag)
        {
            tag.Add(SOULPOWER_TAG, GetSoulPower(dbtrPlayer));
            PlayerTransformation playerTransformation = dbtrPlayer.AcquiredTransformations[this];

            Dictionary<string, int> mobCount = (Dictionary<string, int>)playerTransformation.ExtraInformation[DIMINISHINGRETURNS_MOBCOUNT_PREFIX];

            foreach (KeyValuePair<string, int> kvp in mobCount)
                tag.Add(DIMINISHINGRETURNS_MOBCOUNT_PREFIX + kvp.Key, kvp.Value);
        }

        #endregion

        public override void OnPlayerAcquiredTransformation(DBTRPlayer dbtrPlayer) => DefaultSetup(dbtrPlayer.AcquiredTransformations[this]);

        private void DefaultSetup(PlayerTransformation playerTransformation)
        {
            if (!playerTransformation.ExtraInformation.ContainsKey(SOULPOWER_TAG))
                playerTransformation.ExtraInformation.Add(SOULPOWER_TAG, 0f);

            if (!playerTransformation.ExtraInformation.ContainsKey(DIMINISHINGRETURNS_MOBCOUNT_PREFIX))
                playerTransformation.ExtraInformation.Add(DIMINISHINGRETURNS_MOBCOUNT_PREFIX, new Dictionary<string, int>());
        }


        public override void OnPlayerKilledNPC(DBTRPlayer dbtrPlayer, NPC npc)
        {
            Dictionary<string, int> dimishingReturnsDictionary = GetDiminishingReturnsDictionary(dbtrPlayer);
            string npcType = npc.TypeName.Replace(" ", "");

            if (!dimishingReturnsDictionary.ContainsKey(npcType))
                dimishingReturnsDictionary.Add(npcType, 1);

            dimishingReturnsDictionary[npcType]++;
            AddSoulPower(dbtrPlayer, npc);
        }

        public override float GetDamageMultiplier(DBTRPlayer dbtrPlayer)
        {
            return BaseDamageMultiplier + GetSoulPower(dbtrPlayer) / 1100;
        }

        public override float GetSpeedMultiplier(DBTRPlayer dbtrPlayer)
        {
            return BaseSpeedMultiplier + GetSoulPower(dbtrPlayer) / 1100;
        }

        public override int GetDefenseAdditive(DBTRPlayer dbtrPlayer)
        {
            return (int)Math.Round(GetSoulPower(dbtrPlayer) / 250f);
        }


        public override bool CheckPrePlayerConditions() => SteamHelper.SteamID64 == "76561198046878487";


        public float GetSoulPower(DBTRPlayer player) => (float)player.AcquiredTransformations[this].ExtraInformation[SOULPOWER_TAG];

        public void SetSoulPower(DBTRPlayer player, float multiplier) => player.AcquiredTransformations[this].ExtraInformation[SOULPOWER_TAG] = multiplier;

        public void AddSoulPower(DBTRPlayer player, NPC npc)
        {
            float gain = (npc.lifeMax / (float)player.player.statLifeMax2) / GetMobKilledCount(player, npc.TypeName.Replace(" ", ""));

            if (npc.boss && gain < 110)
                gain = 110;
            else if (gain < 1)
                gain = 1;
            else if (npc.boss)
                gain *= 2;

            SetSoulPower(player, GetSoulPower(player) + gain);
        }

        private int GetMobKilledCount(DBTRPlayer dbtrPlayer, string npcTypeName) => GetDiminishingReturnsDictionary(dbtrPlayer)[npcTypeName];

        public Dictionary<string, int> GetDiminishingReturnsDictionary(DBTRPlayer dbtrPlayer) => (Dictionary<string, int>)dbtrPlayer.AcquiredTransformations[this].ExtraInformation[DIMINISHINGRETURNS_MOBCOUNT_PREFIX];
    }

    public sealed class SoulStealerTransformationBuff : TransformationBuff
    {
        public SoulStealerTransformationBuff() : base(TransformationDefinitionManager.Instance.SoulStealer)
        {
        }

        public override string BuildDefaultTooltip(DBTRPlayer player)
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

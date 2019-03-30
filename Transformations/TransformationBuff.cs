using System;
using System.Text;
using DBTMod.Buffs;
using DBTMod.Players;
using Terraria;

namespace DBTMod.Transformations
{
    public abstract class TransformationBuff : DBTRBuff, IHasUnlocalizedName
    {
        protected TransformationBuff(TransformationDefinition definition)
        {
            Definition = definition;
        }


        public override void SetDefaults()
        {
            if (Definition == null) return;

            DisplayName.SetDefault(Definition.DisplayName);
            Description.SetDefault(BuildDefaultTooltip());

            Main.buffNoTimeDisplay[Type] = Definition.Duration == TransformationDefinition.TRANSFORMATION_LONG_DURATION;
            Main.buffNoSave[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            DBTRPlayer dbtrPlayer = player.GetModPlayer<DBTRPlayer>();
            if (dbtrPlayer == null) return;

            if (!dbtrPlayer.IsTransformed(this))
            {
                player.ClearBuff(Type);
                return;
            }

            float 
                damageMultiplier = Definition.GetDamageMultiplier(dbtrPlayer),
                halvedDamageMultiplier = damageMultiplier / 2;

            player.meleeDamage *= damageMultiplier;
            dbtrPlayer.KiMultiplier = damageMultiplier;

            player.rangedDamage *= halvedDamageMultiplier;
            player.thrownDamage *= halvedDamageMultiplier;
            player.magicDamage *= halvedDamageMultiplier;
            player.minionDamage *= halvedDamageMultiplier;

            player.statDefense += Definition.GetDefenseAdditive(dbtrPlayer);

            float speedMultiplier = Definition.GetSpeedMultiplier(dbtrPlayer);

            player.moveSpeed *= speedMultiplier;
            player.maxRunSpeed *= speedMultiplier;
            player.runAcceleration *= speedMultiplier;

            if (player.jumpSpeedBoost < 1f)
                player.jumpSpeedBoost = 1f;

            player.jumpSpeedBoost *= speedMultiplier;
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            tip = BuildDefaultTooltip(Main.LocalPlayer.GetModPlayer<DBTRPlayer>());
        }


        #region Tooltips

        public virtual string BuildDefaultTooltip() =>
            BuildTooltip(Definition.BaseDamageMultiplier, Definition.BaseSpeedMultiplier, Definition.BaseDefenseAdditive, Definition.UnmasteredKiDrain, Definition.MasteredKiDrain);

        public virtual string BuildDefaultTooltip(DBTRPlayer player) =>
            BuildTooltip(Definition.GetDamageMultiplier(player), Definition.GetSpeedMultiplier(player), Definition.GetDefenseAdditive(player), Definition.GetUnmasteredKiDrain(player), Definition.GetMasteredKiDrain(player));

        private string BuildTooltip(float damageMultiplier, float speedMultiplier, int baseDefenseAdditive, float unmasteredKiDrain, float masteredKiDrain)
        {
            StringBuilder sb = new StringBuilder();

            float roundedDamageMultiplier = (float)Math.Round(damageMultiplier, 2);
            float roundedSpeedMultiplier = (float)Math.Round(speedMultiplier, 2);

            BuildDamageAndSpeedInline(sb, roundedDamageMultiplier, roundedSpeedMultiplier);

            int roundedUnmasteredKiDrain = (int)Math.Round(unmasteredKiDrain);
            int roundedMasteredKiDrain = (int)Math.Round(masteredKiDrain);

            BuildKiDrainInline(sb, roundedUnmasteredKiDrain, roundedMasteredKiDrain);

            return sb.ToString();
        }

        private void BuildDamageAndSpeedInline(StringBuilder builder, float firstValue, float secondValue)
        {
            if (firstValue != 0)
                builder.AppendFormat("{0}{1:F2}x {2}", firstValue > 0 ? '+' : '-', firstValue - 1, "Damage");

            if (firstValue != 0 && secondValue != 0)
                builder.Append(", ");

            if (secondValue != 0)
                builder.AppendFormat("{0}{1:F2}x {2}", secondValue > 0 ? '+' : '-', secondValue - 1, "Speed");

            if (firstValue != 0 && secondValue != 0)
                builder.AppendLine();
        }

        private void BuildKiDrainInline(StringBuilder builder, int unmasteredKiDrain, int masteredKiDrain)
        {
            // "While Unmastered"
            // "While Mastered"

            if (unmasteredKiDrain == 0 && masteredKiDrain == 0) return;

            if (unmasteredKiDrain == masteredKiDrain)
                builder.AppendFormat("{0} Ki/Second\n", unmasteredKiDrain);
            else
            {
                if (unmasteredKiDrain != 0)
                    builder.AppendFormat("{0}{1} Ki/Second {2}", unmasteredKiDrain > 0 ? '-' : '+', unmasteredKiDrain, "While Unmastered");

                if (unmasteredKiDrain != 0 && masteredKiDrain != 0)
                    builder.Append(", ");

                if (masteredKiDrain > 0)
                    builder.AppendFormat("{0}{1} Ki/Second {2}", masteredKiDrain > 0 ? '-' : '+', masteredKiDrain, "While Mastered");

                if (unmasteredKiDrain != 0 && masteredKiDrain != 0)
                    builder.AppendLine();
            }
        }

        #endregion


        public string UnlocalizedName => Definition.UnlocalizedName;

        public TransformationDefinition Definition { get; }
    }
}

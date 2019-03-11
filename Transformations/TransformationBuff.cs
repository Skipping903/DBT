using System;
using System.Text;
using DBTRMod.Buffs;
using DBTRMod.Players;
using Terraria;
using Terraria.ModLoader;

namespace DBTRMod.Transformations
{
    public abstract class TransformationBuff : DBTRModBuff, IHasUnlocalizedName
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
            Main.persistentBuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            DBTModPlayer modPlayer = player.GetModPlayer<DBTModPlayer>();
            if (modPlayer == null) return;

            float 
                damageMultiplier = Definition.GetDamageMultiplier(modPlayer),
                halvedDamageMultiplier = damageMultiplier / 2;

            player.meleeDamage *= damageMultiplier;
            modPlayer.KiMultiplier = damageMultiplier;

            player.rangedDamage *= halvedDamageMultiplier;
            player.thrownDamage *= halvedDamageMultiplier;
            player.magicDamage *= halvedDamageMultiplier;
            player.minionDamage *= halvedDamageMultiplier;

            player.statDefense += Definition.GetDefenseAdditive(modPlayer);

            float speedMultiplier = Definition.GetSpeedMultiplier(modPlayer);

            player.moveSpeed *= speedMultiplier;
            player.maxRunSpeed *= speedMultiplier;
            player.runAcceleration *= speedMultiplier;

            if (player.jumpSpeedBoost < 1f)
                player.jumpSpeedBoost = 1f;

            player.jumpSpeedBoost *= speedMultiplier;
        }

        public override void ModifyBuffTip(ref string tip, ref int rare)
        {
            tip = BuildDefaultTooltip(Main.LocalPlayer.GetModPlayer<DBTModPlayer>());
        }


        public string BuildDefaultTooltip() =>
            BuildTooltip(Definition.BaseDamageMultiplier, Definition.BaseSpeedMultiplier, Definition.BaseDefenseAdditive, Definition.UnmasteredKiDrain, Definition.MasteredKiDrain);

        public string BuildDefaultTooltip(DBTModPlayer player) => 
            BuildTooltip(Definition.GetDamageMultiplier(player), Definition.GetSpeedMultiplier(player), Definition.GetDefenseAdditive(player), Definition.GetUnmasteredKiDrain(player), Definition.GetMasteredKiDrain(player));

        private string BuildTooltip(float damageMultiplier, float speedMultiplier, int baseDefenseAdditive, float unmasteredKiDrain, float masteredKiDrain)
        {
            StringBuilder sb = new StringBuilder();

            float roundedDamageMultiplier = (float) Math.Round(damageMultiplier, 2);
            float roundedSpeedMultiplier = (float) Math.Round(speedMultiplier, 2);

            BuildDamageAndSpeedInline(sb, roundedDamageMultiplier, roundedSpeedMultiplier);

            int roundedUnmasteredKiDrain = (int) Math.Round(unmasteredKiDrain);
            int roundedMasteredKiDrain = (int) Math.Round(masteredKiDrain);

            BuildKiDrainInline(sb, roundedUnmasteredKiDrain, roundedMasteredKiDrain);

            return sb.ToString();
        }

        private void BuildDamageAndSpeedInline(StringBuilder builder, float firstValue, float secondValue)
        {
            if (firstValue != 0)
                builder.AppendFormat("{0}{1:F2}x {2}", firstValue > 0 ? '+' : '-', firstValue, "Damage");

            if (firstValue != 0 && secondValue != 0)
                builder.Append(", ");

            if (secondValue > 0)
                builder.AppendFormat("{0}{1:F2}x {2}", secondValue > 0 ? '+' : '-', secondValue, "Speed");

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


        public string UnlocalizedName => Definition.UnlocalizedName;

        public TransformationDefinition Definition { get; }
    }
}

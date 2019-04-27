using DBT.Buffs;
using DBT.Players;
using Terraria;

namespace DBT.Items.Consumables.Potionlike.KiStimulants
{
    public sealed class KiStimulantBuff : DBTBuff
    {
        public KiStimulantBuff() : base("Ki Stimulant", "Your body is enhanced, increasing natural Ki regeneration by 20%.")
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            base.Update(player, ref buffIndex);
            player.GetModPlayer<DBTPlayer>().NaturalKiRegenerationMultiplier += 0.20f;
        }
    }
}
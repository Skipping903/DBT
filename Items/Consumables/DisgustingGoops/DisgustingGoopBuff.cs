using DBT.Buffs;
using DBT.Players;
using Terraria;

namespace DBT.Items.Consumables.DisgustingGoops
{
    public sealed class DisgustingGoopBuff : DBTBuff
    {
        public DisgustingGoopBuff() : base("Disgusting Goop", "Your ki seems stablized, but you also feel sick.")
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            base.Update(player, ref buffIndex);

            player.statLifeMax2 = (int) (player.statLifeMax2 * 0.95f);
            player.GetModPlayer<DBTPlayer>().NaturalKiRegenerationMultiplier += 0.20f;
        }
    }
}
using DBT.Players;
using Terraria;

namespace DBT.Buffs
{
    public sealed class EnhancedReservesBuff : DBTBuff
    {
        public EnhancedReservesBuff() : base("Enhanced Reserves", "Your Ki reserves have been enhanced.")
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Main.buffNoTimeDisplay[Type] = true;
            Main.debuff[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            base.Update(player, ref buffIndex);

            player.GetModPlayer<DBTPlayer>().ModifyKi(0.2f);
        }
    }
}
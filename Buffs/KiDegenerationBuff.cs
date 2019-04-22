using DBT.Commons.Buffs;
using DBT.Players;
using Terraria;

namespace DBT.Buffs
{
    public sealed class KiDegenerationBuff : DBTBuff, ICanStopCharging
    {
        public KiDegenerationBuff() : base("Ki Degeneration", "You can't regenerate Ki!")
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
            Main.persistentBuff[Type] = true;
        }

        public bool DoesStopCharging(DBTPlayer dbtPlayer) => true;
    }
}
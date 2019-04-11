using DBT.Commons.Buffs;
using DBT.Players;
using Terraria;
using Terraria.ModLoader;

namespace DBT.Buffs
{
    public sealed class KiDegenerationBuff : DBTBuff, ICanStopCharging
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            DisplayName.SetDefault("Ki Degeneration");
            Description.SetDefault("You can't regenerate Ki!");

            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
            Main.persistentBuff[Type] = true;
        }

        public bool DoesStopCharging(DBTPlayer dbtPlayer) => true;
    }
}
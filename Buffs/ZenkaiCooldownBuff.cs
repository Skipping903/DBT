using Terraria;

namespace DBT.Buffs
{
    public sealed class ZenkaiCooldownBuff : DBTBuff
    {
        public ZenkaiCooldownBuff() : base("Zenkai Cooldown", "Your zenkai ability is on cooldown.")
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            Main.buffNoTimeDisplay[Type] = false;
            Main.debuff[Type] = true;
        }
    }
}
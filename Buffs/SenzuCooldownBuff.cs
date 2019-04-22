using Terraria;

namespace DBT.Buffs
{
    public sealed class SenzuCooldownBuff : DBTBuff
    {
        public SenzuCooldownBuff() : base("Senzu Cooldown", "You feel too sick to eat another senzu.")
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
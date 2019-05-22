using DBT.Buffs;
using Terraria;

namespace DBT.Items.Consumables.Potionlike.KiPotions
{
    public class KiPotionSicknessDebuff : DBTBuff
    {
        public KiPotionSicknessDebuff() : base("Ki Potion Sickness", "You feel sick at the thought of another Ki potion.")
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
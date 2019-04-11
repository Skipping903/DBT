using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories.Infusers
{
    public sealed class CursedFlameInfuser : Infuser
    {
        public CursedFlameInfuser() : base("Cursed Flame Ki Infuser", "Hitting enemies with Ki attacks inflicts cursed inferno", 0 * Constants.SILVER_VALUE_MULTIPLIER, ItemID.CursedFlame)
        {
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            base.OnHitNPC(player, target, damage, knockBack, crit);

            target.AddBuff(BuffID.CursedInferno, 300);
        }
    }
}
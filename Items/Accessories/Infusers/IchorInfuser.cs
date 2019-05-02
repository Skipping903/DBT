using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories.Infusers
{
    public sealed class IchorInfuser : Infuser
    {
        public IchorInfuser() : base("Ichor Ki Infuser", "Hitting enemies with Ki attacks inflicts ichor", 0 * Constants.SILVER_VALUE_MULTIPLIER, ItemID.Ichor)
        {
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            base.OnHitNPC(player, target, damage, knockBack, crit);

            target.AddBuff(BuffID.Ichor, 300);
        }
    }
}
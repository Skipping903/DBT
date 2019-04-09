using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories.Infusers
{
    public class RubyInfuser : Infuser
    {
        public RubyInfuser() : base("Ruby Ki Infuser", "Hitting enemies with ki attacks inflicts bleeding.", 220 * Constants.SILVER_VALUE_MULTIPLIER, ItemID.Ruby)
        {
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            base.OnHitNPC(player, target, damage, knockBack, crit);

            target.AddBuff(BuffID.OnFire, 180);
        }
    }
}
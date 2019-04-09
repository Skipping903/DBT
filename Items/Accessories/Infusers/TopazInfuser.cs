using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories.Infusers
{
    public class TopazInfuser : Infuser
    {
        public TopazInfuser() : base("Topaz Ki Infuser", "Hitting enemies with ki attacks inflicts on fire.", 150 * Constants.SILVER_VALUE_MULTIPLIER, ItemID.Topaz)
        {
        }


        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            base.OnHitNPC(player, target, damage, knockBack, crit);

            target.AddBuff(BuffID.OnFire, 180);
        }
    }
}
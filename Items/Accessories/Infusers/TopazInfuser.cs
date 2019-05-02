using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories.Infusers
{
    public class TopazInfuser : Infuser
    {
        public TopazInfuser() : base("Topaz Ki Infuser", "Hitting enemies with Ki attacks inflicts on fire", Item.buyPrice(gold:1, silver: 50), ItemID.Topaz)
        {
        }


        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            base.OnHitNPC(player, target, damage, knockBack, crit);

            target.AddBuff(BuffID.OnFire, 180);
        }
    }
}
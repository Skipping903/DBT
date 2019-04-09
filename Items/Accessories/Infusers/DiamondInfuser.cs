using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories.Infusers
{
    public sealed class DiamondInfuser : Infuser
    {
        public DiamondInfuser() : base("Diamond Ki Infuser", "Hitting enemies with ki attacks inflicts confusion.", 260 * Constants.SILVER_VALUE_MULTIPLIER, ItemID.Diamond)
        {
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            base.OnHitNPC(player, target, damage, knockBack, crit);

            target.AddBuff(BuffID.Confused, 180);
        }
    }
}
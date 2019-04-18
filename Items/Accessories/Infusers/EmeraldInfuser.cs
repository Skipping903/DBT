using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories.Infusers
{
    public sealed class EmeraldInfuser : Infuser
    {
        public EmeraldInfuser() : base("Emerald Ki Infuser", "Hitting enemies with Ki attacks inflicts poison", Item.buyPrice(gold:1, silver: 85), ItemID.Emerald)
        {
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            base.OnHitNPC(player, target, damage, knockBack, crit);

            target.AddBuff(BuffID.Frostburn, 180);
        }
    }
}
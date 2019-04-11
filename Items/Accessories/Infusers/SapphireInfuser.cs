using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories.Infusers
{
    public class SapphireInfuser : Infuser
    {
        public SapphireInfuser() : base("Sapphire Ki Infuser", "Hitting enemies with Ki attacks inflicts frostburn", 165 * Constants.SILVER_VALUE_MULTIPLIER, ItemID.Sapphire)
        {
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            base.OnHitNPC(player, target, damage, knockBack, crit);

            target.AddBuff(BuffID.Frostburn, 180);
        }
    }
}
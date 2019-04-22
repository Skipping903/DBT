using Terraria;
using Terraria.ModLoader;

namespace DBT.Commons.Projectiles
{
    public interface IHandleProjectileOnHitNPC
    {
        void OnProjectileHitNPC(ModProjectile projectile, NPC npc, ref int damage, ref float knockback, ref bool crit);
    }
}
using Terraria;

namespace DBT.Extensions
{
    public static class PlayerExtensions
    {
        public static Projectile FindNearestOwnedProjectileOfType(this Player player, int type)
        {
            int closestProjectileId = -1;
            float? distance = null;

            for (int i = 0; i < Main.projectile.Length; i++)
            {
                Projectile projectile = Main.projectile[i];

                if (!projectile.active || projectile == null || projectile.owner != player.whoAmI || projectile.type != type) continue;

                float projectileDistance = projectile.Distance(player.Center);

                if (projectileDistance < distance)
                {
                    distance = projectileDistance;
                    closestProjectileId = i;
                }
            }

            return closestProjectileId == -1 ? null : Main.projectile[closestProjectileId];
        }

        public static bool RecapturePlayerChargeBall(this Player player, int type)
        {
            if (player.heldProj != -1)
            {
                Projectile heldProjectile = Main.projectile[player.heldProj];

                if (heldProjectile.modProjectile is ChargeBall)
            }
        }
    }
}
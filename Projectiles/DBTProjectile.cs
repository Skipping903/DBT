using DBT.Commons.Projectiles;
using DBT.Extensions;
using DBT.Players;
using Terraria;
using Terraria.ModLoader;

namespace DBT.Projectiles
{
    public abstract class DBTProjectile : ModProjectile
    {
        protected IHandleProjectileOnHitNPC[] onHitNPCHandlers;

        public override bool PreAI()
        {
            if (Owner == null)
                Owner = Main.player[projectile.owner].GetModPlayer<DBTPlayer>();

            if (onHitNPCHandlers == null)
                onHitNPCHandlers = Owner.player.GetItemsByType<IHandleProjectileOnHitNPC>(armor: true, accessories: true).ToArray();

            return base.PreAI();
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            for (int i = 0; i < onHitNPCHandlers.Length; i++)
                onHitNPCHandlers[i].OnProjectileHitNPC(this, target, ref damage, ref knockback, ref crit);
        }

        public DBTPlayer Owner { get; private set; }
    }
}
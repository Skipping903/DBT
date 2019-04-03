using System.Collections.Generic;
using DBT.Extensions;
using DBT.Items.Accessories.Infusers;
using DBT.Players;
using Terraria;

namespace DBT.Projectiles
{
    public abstract class DBTKiProjectile : DBTProjectile
    {
        protected Infuser[] infusers;

        public override bool PreAI()
        {
            if (Owner == null)
                Owner = Main.player[projectile.owner].GetModPlayer<DBTPlayer>();

            if (infusers == null)
                infusers = Owner.GetItemsInArmor<Infuser>().ToArray();

            return base.PreAI();
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            
        }

        public override void PostAI()
        {
            projectile.netUpdate = true;
        }

        public override bool CloneNewInstances => true;

        public DBTPlayer Owner { get; private set; }
    }
}
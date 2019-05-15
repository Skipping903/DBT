using DBT.Players;
using Terraria;
using Terraria.ModLoader;

namespace DBT.Projectiles
{
    public abstract class DBTProjectile : ModProjectile
    {
        public override bool PreAI()
        {
            if (Owner == null)
                Owner = Main.player[projectile.owner].GetModPlayer<DBTPlayer>();

            projectile.netUpdate = true;

            return base.PreAI();
        }

        public DBTPlayer Owner { get; private set; }

        public override bool CloneNewInstances => true;
    }
}
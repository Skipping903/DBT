using DBT.Players;
using Terraria;
using Terraria.ModLoader;

namespace DBT.Projectiles
{
    public abstract class DBTProjectile : ModProjectile
    {
        protected DBTProjectile(int damage)
        {
            Damage = damage;
        }


        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.damage = Damage;
        }

        public override bool PreAI()
        {
            if (Owner == null)
                Owner = Main.player[projectile.owner].GetModPlayer<DBTPlayer>();

            projectile.netUpdate = true;

            return base.PreAI();
        }


        public DBTPlayer Owner { get; protected set; }

        public override bool CloneNewInstances => true;

        public int Damage { get; }
    }
}
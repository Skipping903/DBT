namespace DBT.Projectiles
{
    public abstract class DBTKiProjectile : DBTProjectile
    {
        public override void PostAI()
        {
            projectile.netUpdate = true;
        }

        public override bool CloneNewInstances => true;
    }
}
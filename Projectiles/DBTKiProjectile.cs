using DBT.Players;

namespace DBT.Projectiles
{
    public abstract class KiProjectile : DBTProjectile
    {
        public KiProjectile(float kiDrain)
        {
            KiDrain = kiDrain;
        }

        public override void PostAI()
        {
            base.PostAI();

            if (!ChargeBall)
                projectile.scale = projectile.scale + ChargeLevel;
        }

        public int GetChargeLevelLimit(DBTPlayer dbtPlayer) => ChargeLevelMax + dbtPlayer.SkillChargeLevelLimitModifier;

        public float KiDrain { get; }

        public int ChargeLevel { get; protected set; }
        public int ChargeLevelMax { get; protected set; }

        public int ChargeTimer { get; protected set; }
        public float ChargeTimerMax { get; protected set; }

        public int SizeTimer { get; protected set; }
        
        public bool ChargeBall { get; protected set; }
        public float ChargeBallScale { get; protected set; }

        public bool BeamTrail { get; protected set; }
    }
}

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

            if (BeamTrail && projectile.scale > 0 && SizeTimer > 0)
            {
                SizeTimer = 120 - 1;
                projectile.scale = projectile.scale * SizeTimer / 120f;
            }

            if (ChargeBall)
            {
                if (Owner.Ki <= 0f)
                    Owner.player.channel = false;

                projectile.hide = true;

                if (projectile.timeLeft < 4)
                    projectile.timeLeft = 10;

                projectile.position.X = Owner.player.Center.X + Owner.player.direction * 20 - 5;
                projectile.position.Y = Owner.player.Center.Y - 3;

                projectile.netUpdate2 = true;

                if (!Owner.player.channel && ChargeLevel < 1)
                    projectile.Kill();

                // If the player is channeling, increment the timer and apply some slowdown
                if (Owner.player.channel && projectile.active)
                {
                    ChargeTimer++;
                    Owner.ApplySkillChargeSlowdown();
                }
            }
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

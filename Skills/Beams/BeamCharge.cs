using System;
using DBT.Helpers;
using DBT.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace DBT.Skills.Beams
{
    public abstract class BeamCharge<T> : AbstractChargeBall
    {
        protected BeamCharge(float baseChargeKiDrain, float chargeLimit, float minimumChargeLevel, float chargeRatePerSecond)
        {
            BaseChargeKiDrain = baseChargeKiDrain;

            BaseChargeLimit = chargeLimit;
            BaseMinimumChargeLevel = minimumChargeLevel;
        }


        public void HandleFiring(DBTPlayer dbtPlayer, Vector2 mouseVector)
        {
            // minimum charge level is required to fire in the first place, but once you fire, you can keep firing.
            if (ShouldFireBeam(dbtPlayer))
            {
                // kill the charge sound if we're firing
                chargeSoundSlotId = SoundHelper.KillTrackedSound(chargeSoundSlotId);

                if (!wasSustainingFire && Projectile == null)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient || Main.myPlayer == dbtPlayer.player.whoAmI)
                    {
                        // fire the laser!
                        Projectile = Projectile.NewProjectileDirect(projectile.position + beamCreationOffset, projectile.velocity, mod.ProjectileType(beamProjectileName), GetBeamDamage(), projectile.knockBack, projectile.owner);

                        // set firing time minimum for beams that auto-detach and are stationary, this prevents their self kill routine
                        Projectile.ai[1] = minimumFireFrames;
                    }
                }

                // increment the fire time, this handles "IsSustainingFire" as well as stating the beam is no longer firable (it is already being fired)
                CurrentFireTime++;

                // if the player has charge left, drain the ball
                if (ChargeLevel >= GetFireDecayRate())
                {
                    ChargeLevel = Math.Max(0f, ChargeLevel - GetFireDecayRate());
                }
                else if (dbtPlayer.Ki != 0)
                {
                    if (DBTMod.IsTickRateElapsed(FIRE_KI_DRAIN_WINDOW))
                    {
                        dbtPlayer.ModifyKi(-FireKiDrainRate(), true, false);
                    }
                }
                else
                {
                    // beam is no longer sustainable
                    KillBeam();
                }
            }
            else
            {
                // player has stopped firing or something else has stopped them
                KillBeam();
            }

            wasSustainingFire = IsSustainingFire;
        }


        private bool ShouldFireBeam(DBTPlayer dbtPlayer) =>
            (ChargeLevel >= GetMinimumChargeLevel(dbtPlayer) && IsOnCooldown || IsSustainingFire) && (dbtPlayer.MouseLeftHeld || IsSustainingFire && CurrentFireTime > 0 && CurrentFireTime < minimumFireFrames));

        public float GetBeamPowerMultiplier() => 1f + ChargeLevel / 10f;

        public int GetBeamDamage() => (int) Math.Ceiling(projectile.damage * GetBeamPowerMultiplier());

        public float GetKiDrainMultiplier() => 1f + Math.Max(0f, (CurrentFireTime - minimumFireFrames) / minimumFireFrames);

        public virtual float GetChargeKiDrain(DBTPlayer dbtPlayer) => BaseChargeKiDrain;

        public int GetFireKiDrainRate() => (int)Math.Ceiling(GetBeamPowerMultiplier() * GetKiDrainMultiplier() * (chargeKiDrainPerSecond * 2f / (60f / FIRE_KI_DRAIN_WINDOW)));

        public float GetFireDecayRate() => GetBeamPowerMultiplier() * fireChargeDrainPerSecond / 60f;


        public virtual float GetChargeLimit(DBTPlayer dbtPlayer) => BaseChargeLimit;
        public virtual float GetMinimumChargeLevel(DBTPlayer dbtPlayer) => BaseMinimumChargeLevel;


        public float BaseChargeKiDrain { get; protected set; }

        public float BaseChargeLimit { get; protected set; }
        public float BaseMinimumChargeLevel { get; protected set; }

        public Projectile Projectile { get; protected set; }
    }
}
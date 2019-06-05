using System;
using System.Collections.Generic;
using System.Windows;
using DBT.Commons.Projectiles;
using DBT.Extensions;
using DBT.Items.Armor.Individual;
using DBT.Items.KiOrbs;
using DBT.Players;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace DBT.Projectiles
{
    public abstract class KiProjectile : DBTProjectile
    {
        private static readonly List<int> _damageHalvedAgainst = new List<int>();

        static KiProjectile()
        {
            AddNPCToHalfDamageAgainst(NPCID.EaterofWorldsTail, NPCID.EaterofWorldsHead, NPCID.EaterofWorldsBody);
            AddNPCToHalfDamageAgainst(NPCID.TheDestroyerTail, NPCID.TheDestroyer, NPCID.TheDestroyerBody);
        }

        public static void AddNPCToHalfDamageAgainst(params int[] npcIDs)
        {
            for (int i = 0; i < npcIDs.Length; i++)
                _damageHalvedAgainst.Add(npcIDs[i]);
        }

        protected KiProjectile(float kiDrain, int damage) : base(damage)
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

                if (ChargeTimer > ChargeTimerMax && ChargeLevel < FinalChargeLimit)
                {
                    ChargeLevel++;
                    CombatText.NewText(new Rectangle((int) Owner.player.position.X, (int) Owner.player.position.Y, Owner.player.width, Owner.player.height), new Color(51, 204, 255), ChargeLevel);

                    ChargeTimer = 0;
                }

                if (DBTMod.IsTickRateElapsed(2) && Owner.Ki == 0)
                    Owner.ModifyKi(-KiDrain);

                for (int i = 0; i < 4; i++)
                {
                    float angle = Main.rand.NextFloat(360);
                    float angleRad = MathHelper.ToRadians(angle);
                    Vector2 position = new Vector2((float) Math.Cos(angleRad), (float) Math.Sin(angleRad));

                    Dust tDust = Dust.NewDustDirect(projectile.position + position * (10 + 2 * projectile.scale), projectile.width, projectile.height, DustType, 0f, 0f, 213, default(Color), ChargeBallScale);
                    tDust.velocity = Vector2.Normalize(projectile.position + projectile.Size / 2 - tDust.position) * 2;
                    tDust.noGravity = true;
                }
            }
        }

        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
        {
            if (Owner == null)
                Owner = Main.LocalPlayer.GetModPlayer<DBTPlayer>();

            if (Main.expertMode && _damageHalvedAgainst.Contains(target.type))
                damage /= 2;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            int item = 0;

            if (target.life <= 0)
            {
                if (Main.rand.Next(Owner.KiOrbDropChance) == 0)
                    item = Item.NewItem((int) target.position.X, (int) target.position.Y, target.width, target.height, mod.ItemType<KiOrb>());

                if (Main.netMode == NetmodeID.MultiplayerClient && item >= 0)
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
            }

            if (Owner.player.GetItemsByType<PalladiumBlindfoldCrown>(armor: true).Count > 0)
            {
                if (Main.rand.Next(10) == 0)
                    item = Item.NewItem((int) target.position.X, (int) target.position.Y, target.width, target.height, mod.ItemType<KiOrb>());

                if (Main.netMode == NetmodeID.MultiplayerClient && item >= 0)
                    NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
            }

            List<IHandleProjectileOnHitNPC> handlers = Owner.player.GetItemsByType<IHandleProjectileOnHitNPC>();

            for (int i = 0; i < handlers.Count; i++)
                handlers[i].OnProjectileHitNPC(this, target, ref damage, ref knockback, ref crit);
        }

        public int GetChargeLevelLimit(DBTPlayer dbtPlayer) => ChargeLevelMax + dbtPlayer.SkillChargeLevelLimitModifier;

        public float KiDrain { get; }

        public int ChargeLevel { get; protected set; }
        public int ChargeLevelMax { get; protected set; }
        public int FinalChargeLimit { get; protected set; }

        public int ChargeTimer { get; protected set; }
        public float ChargeTimerMax { get; protected set; }

        public int SizeTimer { get; protected set; }
        
        public bool ChargeBall { get; protected set; }
        public float ChargeBallScale { get; protected set; }

        public bool BeamTrail { get; protected set; }

        public int DustType { get; protected set; }
    }
}

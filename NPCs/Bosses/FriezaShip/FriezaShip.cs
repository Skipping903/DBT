using Microsoft.Xna.Framework;
using System;
using DBT.Extensions;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using DBT.Helpers;
using DBT.NPCs.Bosses.FriezaShip.Projectiles;
using DBT.NPCs.Bosses.FriezaShip.Minions;
using DBT.NPCs.Bosses.FriezaShip.Items;
using DBT.NPCs.Saibas;
using DBT.Projectiles;
using Terraria.Localization;
using System.Collections.Generic;

namespace DBT.NPCs.Bosses.FriezaShip
{
    [AutoloadBossHead]
    public class FriezaShip : ModNPC
    {
		public const int
			STAGE_HOVER = 0,
			STAGE_SLAM = 1,
			STAGE_SLAMBARRAGE = 2,
			STAGE_MINION = 3,
			STAGE_HYPER = 4,

			AI_STAGE_SLOT = 0,
            AI_TIMER_SLOT = 1;

        public FriezaShip()
        {
            HoverDistance = new Vector2(0, 250);
            HyperPosition = new Vector2(0, 0);
            HoverCooldown = 500;
            SlamDelay = 20;
            SlamTimer = 0;
            SlamBarrageCount = 0;
            MinionAmount = 2;
            HasDoneExplodeEffect = false;
            SlamsDone = 0;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("A Frieza Force Ship");
            Main.npcFrameCount[npc.type] = 8;
            NPCID.Sets.TrailingMode[npc.type] = 2;
            NPCID.Sets.TrailCacheLength[npc.type] = 6;
        }

        public override void SetDefaults()
        {
            npc.width = 220;
            npc.height = 120;
            npc.damage = 36;
            npc.defense = 28;
            npc.lifeMax = 3600;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath2;
            npc.value = Item.buyPrice(0, 3, 25, 80);
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/TheUnexpectedArrival");
            bossBag = mod.ItemType<FFShipBag>();
        }

        //To-Do: Add the rest of the stages to the AI. Make green saibaman code.
        //Make the speed of the ship's movements increase with less health, increase the speed of the projectiles, increase how fast the ship goes down on the dash, finish dash afterimage, make the homing projectiles move faster if the player is flying.
        //Boss loot: Drops Undecided material that's used to create a guardian class armor set (frieza cyborg set). Alternates drops between a weapon and accessory, accessory is arm cannon mk2, weapon is a frieza force beam rifle. Expert item is the mechanical amplifier.
        //Spawn condition: Near the ocean you can find a frieza henchmen, if he runs away then you'll get an indicator saying the ship will be coming the next morning.

        
            //AI Rundown: Hovers for 400 ticks then stops in place, spinning up for 190 ticks, then teleports above the player and slams down then flies back up. After every other normal slam and below 70% health it swaps to a barrage of slams, which does 3 slams against 1 player in singleplayer, and 1 for each player in multiplayer.
            //After every 6 basic slams and when below 50% health the ship summons a handful of saibamen and frieza force henchmen as well as a deflector shield that has 800 health, while the shield is up the ship is immune, immobile and will heal over time.
            //After every 5 basic slams and when below 30% health the ship stops, becomes invulnurable and starts emitting yellow dust, after 180 ticks the ship creates lines of dust from right to left indicating the path it will take to attack you, then after 20 ticks it will teleport to the right and move to the left quickly, following the dust line. It does this attack 4 times.

        public override void AI()
        {
			AITimer++;
            #region Base targetting
            Player player = Main.player[npc.target];
            npc.TargetClosest(true);

            //Runaway if no players are alive
            if (!player.active || player.dead) 
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];
                if (!player.active || player.dead)
                {
                    npc.velocity = new Vector2(0f, -10f);

                    if (npc.timeLeft > 10)
                        npc.timeLeft = 10;

                    return;
                }
            }
            #endregion

            #region Random Checks
            if (NPC.AnyNPCs(mod.NPCType<FFShield>()))
            {
                npc.dontTakeDamage = true;
                npc.velocity = Vector2.Zero;
            }
            else
                npc.dontTakeDamage = false;

            //Make sure the stages loop back around
            if (AIStage > 4)
                AIStage = STAGE_HOVER;

            if (SlamsDone > 5)
                SlamsDone = 0;
            
            //Speed between stages and general movement speed drastically increased with health lost
            if (npc.life < npc.lifeMax * 0.80f)
            {
                HoverCooldown = 400;
                SpeedAdd = 1f;
                SlamDelay = 18;
                if (npc.life < npc.lifeMax * 0.50f)
                {
                    HoverCooldown = 250;
                    SpeedAdd = 2f;
                    SlamDelay = 15;
                }
                if (npc.life < npc.lifeMax * 0.2f)
                {
                    HoverCooldown = 100;
                    SpeedAdd = 4f;
                    SlamDelay = 10;
                    MinionAmount = 4;
                }
            }
            #endregion

            #region Hovering
            if (AIStage == STAGE_HOVER)
            {
                //Y Hovering

                if (Main.player[npc.target].position.Y != npc.position.Y + HoverDistance.Y)
                {
                    YHoverTimer++;

                    if (YHoverTimer > 10)
                    {
                        //Thanks UncleDanny for this <3
                        if (Main.player[npc.target].position.Y < npc.position.Y + HoverDistance.Y)
                            npc.velocity.Y -= npc.velocity.Y > 0f ? 1f : 0.15f;

                        if (Main.player[npc.target].position.Y > npc.position.Y + HoverDistance.Y)
                            npc.velocity.Y += npc.velocity.Y < 0f ? 1f : 0.15f;
                    }
                }
                else
                {
                    npc.velocity.Y = 0;
                    YHoverTimer = 0;
                }

                //X Hovering, To-Do: Make the ship not just center itself on the player, have some left and right alternating movement?
                if (Vector2.Distance(new Vector2(player.position.X, 0), new Vector2(npc.position.X, 0)) != HoverDistance.X)
                {
                    //float hoverSpeedY = (-2f + Main.rand.NextFloat(-3, -8));
                    XHoverTimer++;
                    if (XHoverTimer > 30)
                    {
                        npc.velocity.X = 2.5f * npc.direction + SpeedAdd * npc.direction;
                        if (AITimer > HoverCooldown / 1.2)
                        {
                            npc.velocity.X = 7f * npc.direction + SpeedAdd * 2 * npc.direction;
                        }

                    }
                }
                else
                {
                    npc.velocity.X = 0;
                    XHoverTimer = 0;
                }

                //Next Stage
                AITimer++;
                if (AITimer > HoverCooldown)
                {
                    StageAdvance();
                    AITimer = 0;
                }

            }
			#endregion

			#region Slam Barrage
			if (AIStage == STAGE_SLAMBARRAGE)
			{
				if (AITimer < 130)
				{
					npc.velocity = Vector2.Zero;
				}
                if (SlamBarrageCount <= 3)
                {
                    if (Main.netMode != NetmodeID.Server) //Slam barrage for singleplayer, it'll slam 3 times on the same player in a row with a bit of delay.
                    {
                        if (AITimer >= 120)
                        {
                            if (AITimer == 130)
							{
								TeleportAbove();
								if (npc.life <= npc.lifeMax * .5)
									npc.damage = 36;
							}

                            if (AITimer > 130)
                            {
                                SlamTimer++;
                                if (SlamTimer >= SlamDelay) //Delay is 20;
                                {
                                    DoSlam(SlamDelay);
                                    CheckCollision(SlamTimer);
                                }
                            }
                        }
                    }
                    else if (Main.netMode == NetmodeID.Server) //Slam barrage for multiplayer, it'll teleport above each player in the server and slam down 1 by 1.
                    {
                        if (AITimer > 120)
                        {
                            if (AITimer >= 130)
                            {
                                if (SlamDone && IterationSlamAmount <= PlayerMPAmount)
                                {
                                    TeleportAboveAll();
                                    IterationSlamAmount++;
                                    SlamDone = false;
									if (npc.life <= npc.lifeMax * .5)
										npc.damage = 36;
                                }

                                SlamTimer++;

                                if (SlamTimer >= SlamDelayMP && !SlamDone)
                                {
                                    DoSlam(SlamDelayMP);
                                    CheckCollision(SlamTimer);
                                }
                            }
                        }
                    }
                }
                else
                {
                    ResetStage();
                    AITimer = 0;
                    SlamTimer = 0;
                    SlamCoolDownTimer = 0;
                    SlamBarrageCount = 0;
					IterationSlamAmount = 0;
					callArray = -1;
					SlamDone = true;
                    npc.velocity.Y = 0;
                    npc.noTileCollide = true;
                    HasDoneExplodeEffect = false;

                    if (npc.life <= npc.lifeMax * 0.50)//Reset the damage back to its normal amount
                        npc.damage = 36;
                }
            }
            #endregion

            //To-Do: Make the teleport dust get larger the closer it is to teleporting
            #region Basic Slam
            if (AIStage == STAGE_SLAM)
            {
                if(AITimer < 180)
                {
                    npc.velocity = Vector2.Zero;
                    DoTeleportDust();
                }
                    
                if (AITimer > 180)
                {
                    if(AITimer == 190)
                        TeleportAbove();

                    SlamTimer++;
                    if (SlamTimer >= SlamDelay)
                    {
                        if (SlamTimer == SlamDelay)
                        {
							if (npc.life <= npc.lifeMax * 0.50)//Double the contact damage if below 50% health
								npc.damage *= 2;
                        }
                        npc.noTileCollide = false;
                        npc.velocity.Y = 25f;
                    }

                    if (CoordinateExtensions.IsPositionInTile(npc.getRect().Bottom()) || SlamTimer > 30)//If the bottom of the ship touches a tile, nullify speed and do dust particles
                    {
                        npc.velocity.Y = -8f;
                        if (!HasDoneExplodeEffect)
                        {
                            ExplodeEffect();
                            SoundHelper.PlayCustomSound("Sounds/Kiplosion", npc.position, 1.0f);
                        }

                    }

                }
                if (npc.velocity.Y == -8f)
                    SlamCoolDownTimer++;

                if (SlamCoolDownTimer == 30)
                {
                    SlamsDone++;
                    StageAdvance();
                    AITimer = 0;
                    SlamTimer = 0;
                    SlamCoolDownTimer = 0;
                    npc.velocity.Y = 0;
                    npc.noTileCollide = true;
                    HasDoneExplodeEffect = false;

                    if (npc.life <= npc.lifeMax * 0.50)//Reset the damage back to its normal amount
                        npc.damage = npc.damage / 2;
                }

            }
            #endregion

            // Hyper visuals rundown: When it is spinning up it slowly rises up and emits dust from the center of the ship outwards, then becomes invisible until it teleports.
            //A line of dust goes horizontally in the direction the ship is going to teleport and move through, the ship then teleports to the beginning of that line with a puff of dust where it lands.
            //While the ship is moving horizontally it is emitting yellow dust that has no gravity, to simulate movement. Once it reaches the end of the movement it dissapears again with a puff of dust.
            #region Hyper
            int HyperSlamsDone = 0;
            if (AIStage == STAGE_HYPER)
            {
                if (HyperSlamsDone <= 4)
                {
                    if (AITimer < 300)
                    {
                        DoChargeDust();
                        npc.velocity = new Vector2(0, -0.10f);
                    }
                    if (AITimer > 300 && HyperPosition == Vector2.Zero)
                    {
                        npc.alpha = 255;
                        npc.velocity = Vector2.Zero;
                        if (AITimer == 301)
                            ChooseHyperPosition();
                    }
                    if (AITimer > 320 && HyperPosition != Vector2.Zero && npc.velocity == Vector2.Zero)
                        DoLineDust();
                    if (AITimer == 380 && HyperPosition != Vector2.Zero)
                    {
                        TeleportRight();
                        HyperSlamsDone++;
                    }
                    if (AITimer >= 390 && HyperPosition != Vector2.Zero)
                        HorizontalSlam();
                }
                else
                {
                    HorizontalSlamTimer = 0;
                    HyperSlamsDone = 0;
                    AITimer = 0;
                    ResetStage();
                }
            }

            #endregion


            Main.NewText("Slams done: " + SlamsDone);
            Main.NewText("AI Timer is: " + AITimer);
            Main.NewText("Slam barrages done: " + SlamBarrageCount);
            Main.NewText("Ai Stage is:" + AIStage);


            #region Minion Spawning
            if (Main.netMode != 1 && AIStage == STAGE_MINION)
            {
                if (AITimer == 0)
                {
                    SummonSaiba();
                    SummonFfMinions();
                    SummonShield();
                }

                if (AITimer > 60)
                {
                    ResetStage();
                    AITimer = 0;
                }
            }

            //Main.NewText(AIStage);
        }

        public int SummonSaiba()
        {
            for (int amount = 0; amount < MinionAmount; amount++)
            {
                npc.netUpdate = true;

                switch (Main.rand.Next(0, 3))
                {
                    case 0:
                        return NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType<Saibaman1>());
                    case 1:
                        return NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType<Saibaman2>());
                    case 2:
                        return NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType<Saibaman3>());
                    case 3:
                        return NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType<Saibaman4>());
                    default:
                        return 0;
                }
            }
            return NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType<Saibaman1>());
        }

        public int SummonShield()
        {
            SoundHelper.PlayCustomSound("Sounds/ShipShield");
            return NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType<FFShield>());
        }

        public int SummonFfMinions()
        {
            for (int amount = 0; amount < MinionAmount; amount++)
            {
                npc.netUpdate = true;
                switch (Main.rand.Next(0, 2))
                {
                    case 0:
                        return NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType<FriezaForceMinion1>());
                    case 1:
                        return NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType<FriezaForceMinion2>());
                    case 2:
                        return NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType<FriezaForceMinion3>());
                    default:
                        return 0;
                }
            }
            return NPC.NewNPC((int)npc.position.X, (int)npc.position.Y, mod.NPCType<FriezaForceMinion1>());
        }
        #endregion

        #region Hyper Methods
        public void DoChargeDust()
        {
            if (Main.rand.NextFloat() < 3f)
            {
                Dust dust;
                Vector2 position = npc.position + new Vector2(-50f, -220);
                dust = Main.dust[Dust.NewDust(position, 100, 57, 133, 0f, 0f, 0, new Color(255, 255, 255), 0.7236842f)];
                dust.noGravity = true;
            }
        }
        public void ChooseHyperPosition()
        {
            Player targetPlayer = Main.player[npc.target];
            HyperPosition = new Vector2(targetPlayer.position.X + 200, targetPlayer.position.Y + Main.rand.Next(-10, 10));
        }
        public void DoLineDust()
        {
            if (Main.rand.NextFloat() < 1.2f)
            {
                Dust dust;
                dust = Dust.NewDustPerfect(HyperPosition, 133, new Vector2(-50f, 0f), 0, new Color(255, 255, 255), 1.052632f);
                dust.noGravity = true;
            }

        }
        public void TeleportRight()
        {
            npc.position = HyperPosition;
            npc.alpha = 0;
            DoChargeDust();
        }
        int HorizontalSlamTimer = 0;
		public void HorizontalSlam()
		{
            HorizontalSlamTimer++;
            npc.velocity = new Vector2(-20f, 0f);

            if (HorizontalSlamTimer == 60)
            {
                npc.velocity = Vector2.Zero;
                npc.alpha = 255;
                DoChargeDust();
                AITimer = 0;
            }
		}
        #endregion

        #region Slam + Teleporting
        public bool SlamDone = true;
		public int IterationSlamAmount = 0;
		public int callArray = -1;

		public void DoTeleportDust()
        {
            if (Main.rand.NextFloat() < 2f)
            {
                Dust dust;
                Vector2 position = Main.player[npc.target].position + new Vector2(-50f, -220);
                dust = Main.dust[Dust.NewDust(position, 100, 57, 133, 0f, 0f, 0, new Color(255, 255, 255), 0.7236842f)];
                dust.noGravity = true;
            }

        }


        public void TeleportAbove()//For singleplayer
        {
            npc.alpha = 255;
            npc.position = Main.player[npc.target].position + new Vector2(-100f + (Main.player[npc.target].velocity.X * 10), -250);
            Projectile.NewProjectile(npc.oldPosition, Vector2.Zero, mod.ProjectileType<ShipTeleportLinesProj>(), 0, 0);
            SoundHelper.PlayCustomSound("Sounds/ShipTeleport");
            npc.alpha = 0;
        }

		public void TeleportAboveAll() //For multiplayer
		{
			callArray++;

			npc.alpha = 255;
			Vector2 pos = Main.player[callArray].position + new Vector2(-100f + (Main.player[npc.target].velocity.X * 10));
			npc.position = new Vector2(pos.X, -250);
			Projectile.NewProjectile(npc.oldPosition, Vector2.Zero, mod.ProjectileType<ShipTeleportLinesProj>(), 0, 0);
			SoundHelper.PlayCustomSound("Sounds/ShipTeleport");
			npc.alpha = 0;
		}

		public void DoSlam(int sDelay)
		{
			if (npc.life <= npc.lifeMax * 0.50)//Double the contact damage if below 50% health
				npc.damage *= 2;

			SlamBarrageCount++;

			npc.noTileCollide = false;
			npc.velocity.Y = 25f;
		}

		public void CheckCollision(int SlamTimerV) //Gives the slamTimer 10 ticks before going back up, but shouldn't be considered since most of it is wasted on slam movement.
		{
            if (CoordinateExtensions.IsPositionInTile(npc.getRect().Bottom()) || SlamTimerV >= 30)//If the bottom of the ship touches a tile, nullify speed and do dust particles
            {
                npc.velocity.Y = -8f;

				if (!HasDoneExplodeEffect)
				{
					ExplodeEffect();
					SoundHelper.PlayCustomSound("Sounds/Kiplosion", npc.position, 1.0f);
				}

				if (SlamTimerV >= 50)
				{
					SlamTimer = 0;
					AITimer = 90;
                    HasDoneExplodeEffect = false;
				}
			}
		}

        #endregion

        #region Misc Methods
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (AIStage == STAGE_SLAM)
            {
                float extraDrawY = Main.NPCAddHeight(npc.whoAmI);
                Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width / 2, Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type] / 2);
                for (int k = 0; k < npc.oldPos.Length; k++)
                {
                    Vector2 drawPos = new Vector2(npc.position.X - Main.screenPosition.X + npc.width / 2 - (float)Main.npcTexture[npc.type].Width * npc.scale / 2f + drawOrigin.X * npc.scale,
                        npc.position.Y - Main.screenPosition.Y + npc.height - Main.npcTexture[npc.type].Height * npc.scale / Main.npcFrameCount[npc.type] + 4f + extraDrawY + drawOrigin.Y * npc.scale + npc.gfxOffY);

                    Color color = npc.GetAlpha(lightColor) * ((float)(npc.oldPos.Length - k) / (float)npc.oldPos.Length);
                    spriteBatch.Draw(Main.npcTexture[npc.type], drawPos, npc.frame, color, npc.rotation, drawOrigin, npc.scale, SpriteEffects.None, 0f);
                }
            }
            return true;
        }
        
        private void StageAdvance()
        {
			if (AIStage == STAGE_HOVER)
			{
				AIStage++;
				return;
			}
            else if (AIStage == STAGE_SLAM && SlamsDone == 5 && npc.life <= npc.lifeMax * .5)
            {
                AIStage = STAGE_MINION;
                return;
            }
            else if (AIStage == STAGE_SLAM && (SlamsDone == 1 || SlamsDone == 3) && npc.life <= npc.lifeMax * .7)
			{
				AIStage++;
				return;
			}
			else if (AIStage == STAGE_SLAM && SlamsDone == 4 && npc.life <= npc.lifeMax * .3)
			{
				AIStage = STAGE_HYPER;
				return;
			}
			else
				ResetStage();

        }

        private void ResetStage()
        {
            AIStage = STAGE_HOVER;
        }

        
        public override void NPCLoot()
        {

            if (Main.expertMode)
                npc.DropBossBags();
            else
            {
                int choice = Main.rand.Next(0, 1);

                if (choice == 0)
                    Item.NewItem(npc.getRect(), mod.ItemType<BeamRifle>());

                /*if (choice == 1)
                    Item.NewItem(npc.getRect(), mod.ItemType<HenchBlast>());*/

                Item.NewItem(npc.getRect(), mod.ItemType<CyberneticParts>(), Main.rand.Next(7, 18));
                Item.NewItem(npc.getRect(), mod.ItemType<ArmCannonMK2>());

                //if (Main.rand.Next(10) == 0)
                    //Item.NewItem(npc.getRect(), mod.ItemType<FFTrophy>());

            }

            if (!DBTWorld.downedFriezaShip)
            {
                DBTWorld.downedFriezaShip = true;

                if (Main.netMode == NetmodeID.Server)
                    NetMessage.SendData(MessageID.WorldData);
            }
        }

        public void ExplodeEffect()
        {
            for (int num619 = 0; num619 < 3; num619++)
            {
                float scaleFactor9 = 3f;

                if (num619 == 1)
                    scaleFactor9 = 3f;

                int num620 = Gore.NewGore(new Vector2(npc.position.X, npc.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num620].velocity *= scaleFactor9;

                Gore gore97 = Main.gore[num620];
                gore97.velocity.X = gore97.velocity.X + 1f;

                Gore gore98 = Main.gore[num620];
                gore98.velocity.Y = gore98.velocity.Y + 1f;

                num620 = Gore.NewGore(new Vector2(npc.position.X, npc.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num620].velocity *= scaleFactor9;

                Gore gore99 = Main.gore[num620];
                gore99.velocity.X = gore99.velocity.X - 1f;

                Gore gore100 = Main.gore[num620];
                gore100.velocity.Y = gore100.velocity.Y + 1f;

                num620 = Gore.NewGore(new Vector2(npc.position.X, npc.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num620].velocity *= scaleFactor9;

                Gore gore101 = Main.gore[num620];
                gore101.velocity.X = gore101.velocity.X + 1f;

                Gore gore102 = Main.gore[num620];
                gore102.velocity.Y = gore102.velocity.Y - 1f;

                num620 = Gore.NewGore(new Vector2(npc.position.X, npc.position.Y), default(Vector2), Main.rand.Next(61, 64), 1f);
                Main.gore[num620].velocity *= scaleFactor9;

                Gore gore103 = Main.gore[num620];
                gore103.velocity.X = gore103.velocity.X - 1f;

                Gore gore104 = Main.gore[num620];
                gore104.velocity.Y = gore104.velocity.Y - 1f;
            }

            HasDoneExplodeEffect = true;
        }
        #endregion

        #region Animations
        int _frame = 0;
        int _frameTimer = 0;
        int _frameRate = 1;
        public override void FindFrame(int frameHeight)
        {
            if (AIStage == STAGE_SLAM && AITimer < 180 || AIStage == STAGE_SLAMBARRAGE && AITimer < 120 || AIStage == STAGE_HYPER && AITimer < 300)
            {
                npc.frameCounter += _frameRate;
                _frameTimer++;
                if (_frameTimer > 30)
                {
                    _frameRate = 2;
                    if (_frameTimer > 50)
                    {
                        _frameRate = 3;
                        if (_frameTimer > 80)
                        {
                            _frameRate = 4;
                        }
                    }
                }
            }
            else
            {
                npc.frameCounter++;
                _frameRate = 1;
                _frameTimer = 0;
            }


            if (npc.frameCounter > 4)
            {
                _frame++;
                npc.frameCounter = 0;
            }

            if (_frame > 7) //Make it 7 because 0 is counted as a frame, making it 8 frames
                _frame = 0;

            npc.frame.Y = frameHeight * _frame;
        }
		#endregion

		#region Deflector Shield
		public bool JustHitShield = false;

		public void DeflectorShield(float maxDist)
		{
			for (int k = 0; k < Main.maxProjectiles; k++)
			{
				Projectile projectile = Main.projectile[k];

				float distanceBpaFX = npc.position.X - projectile.position.X - projectile.width * .5f;
				float distanceBpaFY = npc.position.Y - projectile.position.Y;
				float distanceV = (float)Math.Sqrt(Math.Pow(distanceBpaFX, 2) + Math.Pow(distanceBpaFY, 2));

				if (distanceV <= maxDist)
				{
					projectile.hostile = true;
					projectile.friendly = false;
					DustManager(projectile);
					JustHitShield = true;

					if (Main.projectile[k] is KiProjectile)
					{
						projectile.Kill();
					}
					else
					{
						projectile.velocity *= -1f;

						if (projectile.Center.X > npc.Center.X * .5f) //Thanks sir Faro for sprite rotation.
						{
							projectile.direction = 1;
							projectile.spriteDirection = 1;
						}
						else
						{
							projectile.direction = -1;
							projectile.spriteDirection = -1;
						}
					}
				}
			}
		}

		public void DustManager(Projectile p)
		{
			for (int i = 0; i < 100; i++)
			{
				Player player = Main.player[npc.target];
				float velocityX = npc.position.X - player.position.X;

				if (velocityX < 0f)
				{
					Dust.NewDust(p.position, p.width, p.height, 59, -2f, 0f, 255, default(Color), 1.5f);
				}
				else
				{
					Dust.NewDust(p.position, p.width, p.height, 59, 2f, 0f, 255, default(Color), 1.5f);
				}
			}
		}
		#endregion

		#region Variables

		public Vector2 HoverDistance { get; set; }
        public Vector2 HyperPosition { get; set; }

		private int pMPamount;
		private int sDelayMP;
		public float HoverCooldown { get; set; }
        public int SlamDelay { get; set; }
        public int SlamBarrageCount { get; set; }
        public int SlamsDone { get; set; }
        public int SlamTimer { get; set; }
        public int SlamCoolDownTimer { get; set; }
        public int MinionAmount { get; set; }
        public bool HasDoneExplodeEffect { get; set; }
        public float SpeedAdd { get; set; }

        public int YHoverTimer { get; set; }
        public int XHoverTimer { get; set; }

        public float AIStage
        {
            get { return npc.ai[AI_STAGE_SLOT]; }
            set { npc.ai[AI_STAGE_SLOT] = value; }
        }

        public float AITimer
        {
            get { return npc.ai[AI_TIMER_SLOT]; }
            set { npc.ai[AI_TIMER_SLOT] = value; }
        }

		public int PlayerMPAmount
		{
			get { return pMPamount; }
			set
			{
				for (int l = 0; l < Main.player.Length; l++) //Will be capturing the amount of players every time it is called.
				{
					if (Main.player[l].active)
					{
						value++;
						pMPamount = value;
					}
					else
					{
						value = 0;
						break;
					}
				}
			}
		}

		public int SlamDelayMP
		{
			get { return sDelayMP; }
			set 
			{
				if (npc.life <= npc.lifeMax * .5)
				{
					value = (int)(SlamDelay * 1.2 / PlayerMPAmount);
					sDelayMP = value;
				}
				else
				{
					value = (int)(SlamDelay * 1.5 / PlayerMPAmount);
					sDelayMP = value;
				} 
			}
		}
		#endregion
	}
}

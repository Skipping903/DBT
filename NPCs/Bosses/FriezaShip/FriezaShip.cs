using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using DBT.NPCs.Saibamen;
using DBT.Helpers;
using DBT.Items.Accessories.ArmCannons;
using DBT.Items.Bags;
using DBT.Items.Materials;
using DBT.Items.Weapons;
using DBT.NPCs.FriezaForce.Minions;
using DBT.Projectiles;

namespace DBT.NPCs.Bosses.FriezaShip
{
    [AutoloadBossHead] //TODO: Work on teleportation dust.
    public class FriezaShip : ModNPC
    {
        public FriezaShip()
        {
            HoverDistance = new Vector2(0, 320);
            HyperPosition = new Vector2(0, 0);
            HoverCooldown = 400;
            XHoverTimer = 0;
            YHoverTimer = 0;
            SSDone = 0;
            DustScaleTimer = 0;
            SSDelay = 20;
            SlamCounter = 0;
            SlamBarrageSpin = 120;
            Random = 0;
            SelectHoverMP = 0;
            IterationCount = 0;
            MinionCount = 2;
            HyperSlamsDone = 0;
            ShieldDuration = 200;
            IterationWarp = 0;
        }

        #region Stages Numbers.

        public const int
            STAGE_HOVER = 0,
            STAGE_SLAM = 1,
            STAGE_SHIELD = 2,
            STAGE_MINION = 3,
            STAGE_HYPER = 4,
            STAGE_WARP = 5,

            AI_STAGE_SLOT = 0,
            AI_TIMER_SLOT = 1;

        #endregion

        #region Defaults

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
            npc.damage = 50;
            npc.defense = 8;
            npc.lifeMax = 3000;
            npc.HitSound = SoundID.NPCHit4;
            npc.DeathSound = SoundID.NPCDeath2;
            npc.value = Item.buyPrice(0, 3, 25, 80);
            npc.knockBackResist = 0f;
            npc.aiStyle = -1;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/TheUnexpectedArrival");
            bossBag = mod.ItemType<FFShipBag>();
        }

        #endregion

        #region Drawing + Animation

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (AIStage == STAGE_SLAM)
            {
                float extraDrawY = Main.NPCAddHeight(npc.whoAmI);
                Vector2 drawOrigin = new Vector2(Main.npcTexture[npc.type].Width / 2,
                    Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type] / 2);
                for (int k = 0; k < npc.oldPos.Length; k++)
                {
                    Vector2 drawPos = new Vector2(
                        npc.position.X - Main.screenPosition.X + npc.width / 2 -
                        (float) Main.npcTexture[npc.type].Width * npc.scale / 2f + drawOrigin.X * npc.scale,
                        npc.position.Y - Main.screenPosition.Y + npc.height -
                        Main.npcTexture[npc.type].Height * npc.scale / Main.npcFrameCount[npc.type] + 4f + extraDrawY +
                        drawOrigin.Y * npc.scale + npc.gfxOffY);

                    Color color = npc.GetAlpha(lightColor) *
                                  ((float) (npc.oldPos.Length - k) / (float) npc.oldPos.Length);
                    spriteBatch.Draw(Main.npcTexture[npc.type], drawPos, npc.frame, color, npc.rotation, drawOrigin,
                        npc.scale, SpriteEffects.None, 0f);
                }
            }

            return true;
        }

        int _frame = 0;
        int _frameTimer = 0;
        int _frameRate = 1;

        public override void FindFrame(int frameHeight) //Spinning before the slam
        {
            if (AIStage == STAGE_SLAM && AITimer <= 180 || AIStage == STAGE_HYPER && AITimer < 300)
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
                            _frameRate = 4;
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

            if (_frame > 7) //Make it 7 because 0 is counted as a frame, making it 8 frames. Genius.com
                _frame = 0;

            npc.frame.Y = frameHeight * _frame;
        }

        #endregion

        public override void AI()
        {
            AITimer++;

            if ((Main.netMode == NetmodeID.MultiplayerClient || Main.netMode == NetmodeID.Server) && SelectHoverMP == 1)
            {
                Random = Main.rand.Next(PlayerCount().Count);
                SelectHoverMP++;
            }

            Player player = Main.player[Random];

            #region Difficulty modification

            if (UnderEightyHealth)
            {
                SpeedAdd = 1f;
                npc.damage = 60;
                SSDelay = 15;
                ShieldDuration = 220;
                HoverCooldown = 350;

            }

            if (UnderFiftyHealth)
            {
                MinionCount = 3;
                SpeedAdd = 2f;
                npc.damage = 65;
                ShieldDuration = 230;
                HoverCooldown = 300;

            }

            if (UnderThirtyHealth)
            {
                MinionCount = 4;
                SpeedAdd = 4f;
                npc.damage = 70;
                SSDelay = 10;
                ShieldDuration = 240;
                ShieldLife = 2;
                HoverCooldown = 200;

            }

            if (Main.expertMode && UnderThirtyHealth)
            {
                MinionCount = 6;
                npc.damage = 80;
                SSDelay = 8;
                ShieldDuration = 250;
                ShieldLife = 3;
                HoverCooldown = 120;
            }

            #endregion

            #region DisappearanceManager

            npc.TargetClosest(true);

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

            #region Fail prevention

            if (AIStage > 4)
                AIStage = STAGE_HOVER;

            if (AITimer > 550)
                AITimer = 0;

            if (SSDone > 10)
                SSDone = 6;

            #endregion

            #region Hovering

            if (AIStage == STAGE_HOVER)
            {
                npc.dontTakeDamage = false;
                //Y Hovering

                if (Main.player[npc.target].position.Y != npc.position.Y + HoverDistance.Y)
                {
                    YHoverTimer++;

                    if (YHoverTimer > 10)
                    {
                        //Thanks UncleDanny and Thorium  team for this <3
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

                if (Vector2.Distance(new Vector2(player.Center.X, 0), new Vector2(npc.Center.X, 0)) != HoverDistance.X)
                {
                    XHoverTimer++;
                    if (XHoverTimer > 30)
                    {
                        npc.velocity.X = 2.5f * npc.direction + SpeedAdd * npc.direction;
                    }
                }
                else
                {
                    npc.velocity.X = 0;
                    XHoverTimer = 0;
                }

                if (AITimer >= HoverCooldown)
                {
                    AdvanceStage(true, true);
                    ResetValues(false);
                    SelectHoverMP = 2;
                }

                if (AITimer <= 100)
                {
                    //Shield code here.
                }

                npc.netUpdate = true;
            }

            #endregion // Starts at 0 ticks.

            #region Slam || MP compatible

            if (AIStage == STAGE_SLAM)
            {
                Slam();
            }

            #endregion

            #region Shield || Regen stage.

            if (AIStage == STAGE_SHIELD)
            {
                if (AITimer > 0)
                {
                    if (AITimer == 1)
                        SoundHelper.PlayCustomSound("Sounds/ShipShield", npc.Center, 2.5f);
                    if (AITimer % 20 == 0)
                        RandomShieldLines();

                    npc.velocity.Y = -1f;

                    for (int k = 0; k < 10; k++)
                    {
                        if (Deg <= 360)
                        {
                            Deg++;

                            float
                                CPosX = npc.Center.X +
                                        ShieldDistance *
                                        (float) Math
                                            .Cos(Deg); //To find the circumference you use formula: x = cX + r * cos(angle), where the x is the coordinate, cX is the center of the circle by X and r is radius.
                            float CPosY = npc.Center.Y + 16f + ShieldDistance * (float) Math.Sin(Deg);

                            for (int i = 0; i < 10; i++)
                            {
                                Dust dust = Main.dust[Dust.NewDust(new Vector2(CPosX, CPosY), 1, 1, 56)];
                                dust.noGravity = true;
                            }
                        }
                    }

                    if (Deg == 360)
                        Deg = 0;
                    npc.netUpdate = true;

                    if (Vector2.Distance(player.Center, npc.Center) <= ShieldDistance)
                    {
                        player.Hurt(
                            PlayerDeathReason.ByCustomReason(
                                player.name + "has been cut in half by the Frieza Force Shield"), 40, 1);
                        Dust dust = Main.dust[
                            Dust.NewDust(player.Center, player.width, player.height, 56)]; //need to pick a new dust
                        dust.noGravity = true;

                        if (player.position.X > npc.Center.X && player.Center.Y < npc.Center.Y) //4th qudrant
                            player.velocity = new Vector2(16f, 16f);
                        else if (player.position.X < npc.Center.X && player.Center.Y < npc.Center.Y) //3rd quadrant
                            player.velocity = new Vector2(-16f, 16f);
                        else if (player.position.X < npc.Center.X && player.Center.Y > npc.Center.Y) //2nd quadrant
                            player.velocity = new Vector2(-16f, -16f);
                        else if (player.position.X > npc.Center.X && player.Center.Y > npc.Center.Y) //1st quadrant
                            player.velocity = new Vector2(16f, -16f);
                    }

                    for (int i = 0; i <= Main.maxProjectiles; i++)
                    {
                        Projectile projectile = Main.projectile[i];

                        if (Vector2.Distance(projectile.Center, npc.Center) <= ShieldDistance && projectile.active)
                        {
                            projectile.velocity *= -1f;

                            for (int j = 0; j < 20; j++)
                            {
                                Dust dust = Main.dust[
                                    Dust.NewDust(projectile.position, projectile.width, projectile.height, 211)];
                                dust.noGravity = true;
                            }

                            projectile.hostile = true;
                            projectile.friendly = false;

                            if (projectile.Center.X > player.Center.X * 0.5f
                            ) //Reference goes to Fargo for voring his epic code.
                            {
                                projectile.direction = 1;
                                projectile.spriteDirection = 1;
                            }
                            else
                            {
                                projectile.direction = -1;
                                projectile.spriteDirection = -1;
                            }

                            projectile.netUpdate = true;
                            npc.netUpdate = true;
                        }
                        else if (Vector2.Distance(projectile.position, npc.Center) <= 6 * 16f)
                        {
                            projectile.Kill();
                            Dust dust = Main.dust[
                                Dust.NewDust(projectile.position, projectile.width, projectile.height, 211)];
                            dust.noGravity = true;
                        }
                    }
                }

                if (npc.life <= npc.lifeMax)
                    npc.life += ShieldLife;
                npc.netUpdate = true;

                if (AITimer >= ShieldDuration)
                {
                    AdvanceStage(true, true);
                    ResetValues(false);
                }
            }

            #endregion

            #region Minions

            if (AIStage == STAGE_MINION)
            {
                npc.velocity = new Vector2(0, -2f);
                if (AITimer == 0)
                {
                    TileX = (int) (npc.position.X + Main.rand.NextFloat(-7f * 16, 6f * 16));
                    TileY = (int) (npc.position.Y + Main.rand.NextFloat(-7f * 16, 6f * 16));
                    SummonFFMinions();
                    SummonSaibamen();
                }

                npc.netUpdate = true;

                if (AITimer == 60)
                {
                    ResetValues(false);
                    AdvanceStage(true, true);
                }
            }

            #endregion

            #region Hyper Stage

            if (AIStage == STAGE_HYPER)
            {
                npc.noTileCollide = true;

                if (HyperSlamsDone <= 4)
                {
                    npc.dontTakeDamage = true;
                    if (AITimer < 300)
                    {
                        DoChargeDust();
                        npc.dontTakeDamage = true;
                        npc.velocity = new Vector2(0, -0.3f);
                        npc.netUpdate = true;
                    }

                    if (AITimer > 300 && HyperPosition == Vector2.Zero)
                    {
                        npc.dontTakeDamage = false;
                        npc.netUpdate = true;
                        npc.velocity = Vector2.Zero;
                        if (AITimer == 301)
                        {
                            CircularDust(30, npc, 133, 10f, 1);
                            ChooseHyperPosition();
                        }

                        npc.netUpdate = true;

                    }

                    if (AITimer > 320 && HyperPosition != Vector2.Zero && npc.velocity == Vector2.Zero)
                    {
                        npc.dontTakeDamage = false;
                        DoLineDust();
                        npc.netUpdate = true;
                    }

                    if (AITimer == 310 && HyperPosition != Vector2.Zero)
                    {
                        npc.dontTakeDamage = false;
                        TeleportRight();
                        HyperSlamsDone++;
                        npc.netUpdate = true;
                    }

                    if (AITimer >= 350 && HyperPosition != Vector2.Zero)
                        HorizontalSlam();
                    npc.netUpdate = true;
                }
                else
                {
                    HorizontalSlamTimer = 0;
                    npc.dontTakeDamage = false;
                    HyperSlamsDone = 0;
                    AITimer = 0;
                    ResetStage();
                    npc.netUpdate = true;
                }
            }

            #endregion

            #region Warp Stage

            if (AIStage == STAGE_WARP)
            {
                Warp();
            }

            #endregion

        }

        #region Warp Stage Methods.

//Performed for all players in MP lobby. 3 slams per player. Left, right, up.

        private void Warp()
        {
            if (IterationWarp < PlayerCount().Count)
            {
                if (AITimer < 130)
                {
                    DustScaleTimer++;
                    npc.velocity = Vector2.Zero;
                    CircularDust(10, npc, 133, 10f - DustScaleTimer / 20, 1);
                }
                else if (AITimer == 130)
                {
                    WarpDict();
                    DoRam(wIT);
                }
                else if (AITimer == 150)
                {
                    wIT++;
                    AITimer = 0;
                }
            }
            else
            {
                AdvanceStage(true, true);
                ResetValues(false);
            }
        }

        private void WarpDict()
        {
            itW++;
            Player player = Main.player[itW];

            offSetW.Add(0, new Vector2(player.Center.X - 8 * 16f, player.Center.Y));
            offSetW.Add(1, new Vector2(player.Center.X + 8 * 16f, player.Center.Y));
            offSetW.Add(2, new Vector2(player.Center.X, player.Center.Y - 8 * 16f));
        }

        private void DoRam(int number)
        {
            Projectile.NewProjectile(npc.Center, Vector2.Zero, mod.ProjectileType<ShipTeleportLinesProjectile>(), 0, 0);
            SoundHelper.PlayCustomSound("Sounds/ShipTeleport");

            if (number == 0)
            {
                npc.velocity.X = 24f;
            }
            else if (number == 1)
            {
                npc.velocity.X = -24f;
            }
            else
            {
                npc.velocity.Y = 24f;
            }

            IterationWarp++;
        }

        #endregion

        #region Hyper Methods

        public void DoChargeDust()
        {
            if (Main.rand.NextFloat() < 5f)
            {
                Dust dust;
                Vector2 position = npc.position + new Vector2(10, 5);

                for (int i = 0; i < 10; i++)
                {
                    dust = Main.dust[
                        Dust.NewDust(position, 220, 120, 133, 0f, 0f, 0, new Color(255, 255, 255), 0.9236842f)];
                    dust.noGravity = true;
                }
            }
        }

        public void ChooseHyperPosition()
        {
            Player targetPlayer = Main.player[Main.rand.Next(PlayerCount().Count)];
            HyperPosition = new Vector2(targetPlayer.Center.X + 500, targetPlayer.Center.Y + Main.rand.Next(10, 20));
            npc.netUpdate = true;
        }

        public void DoLineDust()
        {
            if (Main.rand.NextFloat() < 1.2f)
            {
                Dust dust;
                dust = Dust.NewDustPerfect(HyperPosition, 133, new Vector2(-70f, 0f), 0, new Color(255, 255, 255),
                    1.052632f);
                dust.noGravity = true;
            }

            npc.netUpdate = true;
        }

        public void TeleportRight()
        {
            npc.position = HyperPosition + new Vector2(0, -4 * 16f);
            CircularDust(30, npc, 133, 10f, 1);
            npc.netUpdate = true;
        }

        private int HorizontalSlamTimer = 0;

        public void HorizontalSlam()
        {
            DoChargeDust();
            HorizontalSlamTimer++;
            npc.velocity = new Vector2(-40f, 0f);

            if (HorizontalSlamTimer == 30)
            {
                npc.velocity = Vector2.Zero;
                npc.netUpdate = true;
                AITimer = 300;
                CircularDust(30, npc, 133, 10f, 1);
                HorizontalSlamTimer = 0;
                HyperPosition = Vector2.Zero;
            }

            npc.netUpdate = true;
        }

        #endregion

        #region Main Methods

        private void Slam()
        {
            if (IterationCount < PlayerCount().Count
            ) //Since the list length is 1, but to call the first member we need 0, so it is +1;
            {
                if (AITimer < 130)
                {
                    DustScaleTimer++;
                    npc.velocity = Vector2.Zero;
                    CircularDust(10, npc, 133, 10f - DustScaleTimer / 20, 1);
                }
                else if (AITimer == 130)
                {
                    TeleportAbove();
                }
                else if (AITimer > 130)
                {
                    SlamCounter++;

                    if (SlamCounter == SSDelay)
                    {
                        DoSlam();
                        npc.netUpdate = true;
                    }
                    else if (SlamCounter == SSDelay + 20)
                    {
                        ExplodeEffect();
                        SoundHelper.PlayCustomSound("Sounds/KiExplosion", npc.Center);

                        npc.velocity.Y = -8f;
                        IterationCount++;
                        npc.netUpdate = true;
                    }
                    else if (SlamCounter == SSDelay + 60 &&
                             (Main.netMode == NetmodeID.MultiplayerClient || Main.netMode == NetmodeID.Server))
                    {
                        ResetValues(false);
                    }
                }
            }
            else if (IterationCount == PlayerCount().Count)
            {
                AdvanceStage(true, true);
                ResetValues(true);
            }
        }

        private void TeleportAbove()
        {
            Player player = PlayerCount()[IterationCount];
            Vector2 pos = player.Center + new Vector2(-100f + (player.velocity.X * 10), -HoverDistance.Y);
            npc.position = pos;
            Projectile.NewProjectile(npc.oldPosition, Vector2.Zero, mod.ProjectileType<ShipTeleportLinesProjectile>(),
                0, 0);
            SoundHelper.PlayCustomSound("Sounds/ShipTeleport");
            npc.netUpdate = true;
        }

        public void DoSlam()
        {
            npc.velocity.Y = 25f;

            SSDone++;

            npc.netUpdate = true;
        }

        public int SummonSaibamen()
        {
            for (int i = 0; i <= MinionCount / 2; i++)
            {
                npc.netUpdate = true;

                switch (Main.rand.Next(0, 3))
                {
                    case 0:
                        return NPC.NewNPC(TileX, TileY, mod.NPCType<Saibaman1>());
                    case 1:
                        return NPC.NewNPC(TileX, TileY, mod.NPCType<Saibaman2>());
                    case 2:
                        return NPC.NewNPC(TileX, TileY, mod.NPCType<Saibaman3>());
                    case 3:
                        return NPC.NewNPC(TileX, TileY, mod.NPCType<Saibaman4>());
                    default:
                        return 0;
                }
            }

            return NPC.NewNPC((int) npc.position.X, (int) npc.position.Y, mod.NPCType<Saibaman1>());
        }

        public int SummonFFMinions()
        {
            /*if (Collision.SolidCollision(new Vector2(TileX, TileY), 26, 36) && Main.tile[TileX, TileY].wall == 87)
                {
                    TileVariablesDefinition();
                }*/

            for (int i = 0; i <= MinionCount / 2; i++)
            {
                npc.netUpdate = true;
                switch (Main.rand.Next(0, 2))
                {
                    case 0:
                        return NPC.NewNPC(TileX, TileY, mod.NPCType<FriezaForceMinion1>());
                    case 1:
                        return NPC.NewNPC(TileX, TileY, mod.NPCType<FriezaForceMinion2>());
                    case 2:
                        return NPC.NewNPC(TileX, TileY, mod.NPCType<FriezaForceMinion3>());
                    default:
                        return 0;
                }
            }

            return NPC.NewNPC((int) npc.position.X, (int) npc.position.Y, mod.NPCType<FriezaForceMinion1>());
        }

        private void RandomShieldLines()
        {
            float xStart = npc.Center.X + Main.rand.NextFloat(-8 * 16f, 8 * 16f);
            float yStart = npc.Center.Y + Main.rand.NextFloat(-8 * 16f, 8 * 16f);

            Dust dust = Main.dust[Dust.NewDust(new Vector2(xStart, yStart), 16, 16, 56)];
            dust.noGravity = true;
            dust.velocity = npc.velocity + new Vector2(0, 5 * 16f);
        }

        private void
            ResetValues(
                bool resetiter) //Does not require to reset the timer if anything consists of AdvanceStage(true).
        {
            SelectHoverMP = 0;
            DustScaleTimer = 0;
            SlamCounter = 0;
            AITimer = 0;
            if (resetiter)
                IterationCount = 0;
            npc.netUpdate = true;
        }

        private void ResetStage()
        {
            AIStage = STAGE_HOVER;
            SelectHoverMP = 0;
            AITimer = 0;
            npc.netUpdate = true;
        }

        private void AdvanceStage(bool reset, bool resetCollide)
        {
            if (reset)
                AITimer = 0;


            if (AIStage == STAGE_HOVER)
            {
                if (SSDone == 4 && UnderFiftyHealth)
                {
                    AIStage = STAGE_WARP;
                    return;
                }
                else
                {
                    AIStage++;
                    return;
                }
            }
            else if (AIStage == STAGE_SLAM && (SSDone == 2 || SSDone == 8))
            {
                AIStage = STAGE_SHIELD;
                return;
            }
            else if (AIStage == STAGE_SLAM && SSDone != 2 && SSDone % 2 == 0 && UnderFiftyHealth)
            {
                AIStage = STAGE_MINION;
                return;
            }
            else if (AIStage == STAGE_MINION && (SSDone >= 5 && SSDone >= 7) && UnderThirtyHealth)
            {
                AIStage = STAGE_HYPER;
                return;
            }
            else if (AIStage == STAGE_SLAM && (SSDone >= 7) && UnderThirtyHealth)
            {
                AIStage = STAGE_HYPER;
                return;
            }
            else
            {
                npc.noTileCollide = false;
                ResetStage();
            }

            npc.netUpdate = true;
        }

        #endregion

        #region Misc Methods

        /// <summary>
        /// Returns a list of players. Use .count to find the Count of player present on the server.
        /// </summary>
        /// <returns></returns>
        public List<Player> PlayerCount()
        {
            for (int i = 0; i < Main.player.Length; i++)
            {
                if (Main.player[i].active)
                {
                    if (array.Contains(Main.player[i]))
                        break;

                    array.Add(Main.player[i]);
                }
                else if (!Main.player[i].active)
                    break;
            }

            npc.netUpdate = true;
            return array;
        }

        public static double AngleBetweenVectors(Vector2 v1, Vector2 v2)
        {
            return Math.Atan2((v1.X * v2.Y + v1.Y * v2.X), (v1.X * v2.X + v1.Y * v2.Y)) * (180 / MathHelper.Pi);
        }

        public void CircularDust(int quantity, NPC target, short DustID, float radius, float scale)
        {
            for (int i = 0; i < quantity; i++)
            {
                Vector2 pos = new Vector2(Main.rand.NextFloat(-1, 1), Main.rand.NextFloat(-1, 1)) + target.Center;
                float angle = Main.rand.NextFloat(-(float) Math.PI, (float) Math.PI);
                Dust dust = Dust.NewDustPerfect(pos, DustID,
                    new Vector2((float) Math.Cos(angle), (float) Math.Sin(angle)) * radius, 255, default(Color),
                    scale);
                dust.noGravity = true;
            }
        }

        public void ExplodeEffect()
        {
            for (int num619 = 0; num619 < 3; num619++)
            {
                float scaleFactor9 = 3f;

                if (num619 == 1)
                    scaleFactor9 = 3f;

                int num620 = Gore.NewGore(new Vector2(npc.Center.X, npc.Center.Y), default(Vector2),
                    Main.rand.Next(61, 64), 1f);
                Main.gore[num620].velocity *= scaleFactor9;

                Gore gore97 = Main.gore[num620];
                gore97.velocity.X = gore97.velocity.X + 1f;

                Gore gore98 = Main.gore[num620];
                gore98.velocity.Y = gore98.velocity.Y + 1f;

                num620 = Gore.NewGore(new Vector2(npc.Center.X, npc.Center.Y), default(Vector2), Main.rand.Next(61, 64),
                    1f);
                Main.gore[num620].velocity *= scaleFactor9;

                Gore gore99 = Main.gore[num620];
                gore99.velocity.X = gore99.velocity.X - 1f;

                Gore gore100 = Main.gore[num620];
                gore100.velocity.Y = gore100.velocity.Y + 1f;

                num620 = Gore.NewGore(new Vector2(npc.Center.X, npc.Center.Y), default(Vector2), Main.rand.Next(61, 64),
                    1f);
                Main.gore[num620].velocity *= scaleFactor9;

                Gore gore101 = Main.gore[num620];
                gore101.velocity.X = gore101.velocity.X + 1f;

                Gore gore102 = Main.gore[num620];
                gore102.velocity.Y = gore102.velocity.Y - 1f;

                num620 = Gore.NewGore(new Vector2(npc.Center.X, npc.Center.Y), default(Vector2), Main.rand.Next(61, 64),
                    1f);
                Main.gore[num620].velocity *= scaleFactor9;

                Gore gore103 = Main.gore[num620];
                gore103.velocity.X = gore103.velocity.X - 1f;

                Gore gore104 = Main.gore[num620];
                gore104.velocity.Y = gore104.velocity.Y - 1f;
            }
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

                if (Main.netMode == NetmodeID.MultiplayerClient || Main.netMode == NetmodeID.Server)
                    NetMessage.SendData(MessageID.WorldData);
            }
        }

        #endregion

        #region Variables 

        /// <summary>
        /// Sets the normal hover distance between the player and the ship on the hovering stage.
        /// </summary>
        public Vector2 HoverDistance { get; set; }

        public Vector2 HyperPosition { get; set; }

        public List<Player> array = new List<Player>();
        private Dictionary<int, Vector2> offSetW = new Dictionary<int, Vector2>();

        public const float ShieldDistance = 10 * 16f;
        public float CircleX = 0f;
        public float CircleY = 0f;
        public int Deg = 0;
        private int wIT = -1;
        public int itW = -1;

        public int IterationWarp { get; private set; }
        public int SelectHoverMP { get; private set; }
        public int Random { get; private set; }
        public int SlamCounter { get; private set; }
        public int SSDelay { get; private set; }
        public int DustScaleTimer { get; private set; }
        public double SSDone { get; private set; }
        public int XHoverTimer { get; private set; }
        public float HoverCooldown { get; private set; }
        public int SlamBarrageSpin { get; private set; }
        public int IterationCount { get; private set; }
        public int MinionCount { get; private set; }
        public int YHoverTimer { get; private set; }
        private float SpeedAdd { get; set; }
        public int HyperSlamsDone { get; private set; }
        public float ShieldDuration { get; private set; }
        public int ShieldLife { get; private set; }
        public int TileX { get; private set; }
        public int TileY { get; private set; }


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

        public bool UnderEightyHealth
        {
            get { return npc.life <= npc.lifeMax * .8; }
        }

        public bool UnderFiftyHealth
        {
            get { return npc.life <= npc.lifeMax * .5; }
        }

        public bool UnderThirtyHealth
        {
            get { return npc.life <= npc.lifeMax * .3; }
        }

        #endregion
    }
}

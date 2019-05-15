using Microsoft.Xna.Framework;
using System;
using DBT.Extensions;
using DBT.Utilities;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using DBT.Helpers;
using DBT.NPCs.Bosses.FriezaShip.Projectiles;
using DBT.NPCs.Bosses.FriezaShip.Minions;
using DBT.NPCs.Bosses.FriezaShip.Items;
using DBT.NPCs.Saibas;


namespace DBT.NPCs.Bosses.FriezaShip
{
    [AutoloadBossHead]
    //Thanks a bit to examplemod's flutterslime for helping with organization
	public class FriezaShip : ModNPC
	{
        private Vector2 hoverDistance = new Vector2(0, 150);
        private float hoverCooldown = 500;
        private int slamDelay = 10;
        private int slamTimer = 0;
        private int slamCoolDownTimer = 0;
        private int minionAmount = 2;
        private bool locationSelected = false;
        private bool hasDoneExplodeEffect = false;
        private float speedAdd = 0f;

        private int YHoverTimer = 0;
        private int XHoverTimer = 0;

        private float speedMulti = 1f;

        const int AIStageSlot = 0;
        const int AITimerSlot = 1;

        public float AIStage
        {
            get { return npc.ai[AIStageSlot]; }
            set { npc.ai[AIStageSlot] = value; }
        }

        public float AITimer
        {
            get { return npc.ai[AITimerSlot]; }
            set { npc.ai[AITimerSlot] = value; }
        }

        const int Stage_Hover = 0;
        const int Stage_Slam = 1;
        const int Stage_Barrage = 2;
        const int Stage_Homing = 3;
        const int Stage_Saiba = 4;

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


        public override void AI()
        {
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
                    {
                        npc.timeLeft = 10;
                    }
                    return;
                }
            }

            //Make sure the stages loop back around
            if(AIStage > 4)
            {
                AIStage = Stage_Hover;
            }

            //Speed between stages and general movement speed drastically increased with health lost
            if(npc.life < npc.lifeMax * 0.80f)
            {
                hoverCooldown = 400;
                speedAdd = 1f;
                slamDelay = 8;
                if (npc.life < npc.lifeMax * 0.50f)
                {
                    hoverCooldown = 250;
                    speedAdd = 2f;
                    slamDelay = 6;
                }
                if (npc.life < npc.lifeMax * 0.2f)
                {
                    hoverCooldown = 100;
                    speedAdd = 4f;
                    slamDelay = 4;
                    minionAmount = 4;
                }
            }
            //If the ship is really far away, nullify its movement and set it back to hover
            if(Vector2.Distance(new Vector2(0, player.position.Y), new Vector2(0, npc.position.Y)) > hoverDistance.Y * 3)
            {
                npc.velocity = new Vector2(0, -10f);
            }

            
            //General movement (stage 0)
            if (AIStage == Stage_Hover)
            {
                //Y Hovering

                if (Main.player[npc.target].position.Y != npc.position.Y + hoverDistance.Y)
                {
                    YHoverTimer++;
                    if (YHoverTimer > 10)
                    {
                        //Thanks UncleDanny for this <3
                        if (Main.player[npc.target].position.Y < npc.position.Y + hoverDistance.Y)
                        {
                            npc.velocity.Y -= npc.velocity.Y > 0f ? 1f : 0.15f;
                        }
                        if (Main.player[npc.target].position.Y > npc.position.Y + hoverDistance.Y)
                        {
                            npc.velocity.Y += npc.velocity.Y < 0f ? 1f : 0.15f;
                        }
                    }
                }
                else
                {
                    npc.velocity.Y = 0;
                    YHoverTimer = 0;
                }
                //X Hovering, To-Do: Make the ship not just center itself on the player, have some left and right alternating movement?
                if (Vector2.Distance(new Vector2(player.position.X, 0), new Vector2(npc.position.X, 0)) != hoverDistance.X)
                {
                    //float hoverSpeedY = (-2f + Main.rand.NextFloat(-3, -8));
                    XHoverTimer++;
                    if (XHoverTimer > 30)
                    {
                        npc.velocity.X = (2.5f * npc.direction) + (speedAdd * npc.direction);
                        if (AITimer > hoverCooldown / 1.2)
                        {
                            npc.velocity.X = (7f * npc.direction) + (speedAdd * 2 * npc.direction);
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
                if (AITimer > hoverCooldown)
                {
                    StageAdvance();
                    AITimer = 0;
                }

            }


            //Main.NewText("Speed Addition is: " + speedAdd);



            //Slam attack (stage 1) - Quickly moves to directly above the player, then waits a second before slamming straight down.
            //To-Do: Fix bug where the ship flies down into the ground. Fix afterimage on slam not working.

            if (AIStage == Stage_Slam)
            {
                npc.velocity.X = 0;

                locationSelected = true;
                AITimer++;
                if (AITimer > slamDelay)
                {
                    if (AITimer == slamDelay + 1)
                    {
                        npc.noTileCollide = false;
                        if (npc.life <= npc.lifeMax * 0.50)//Double the contact damage if below 50% health
                        {
                            npc.damage = npc.damage * 2;
                        }
                        npc.velocity.Y = 25f;
                    }
                    if (CoordinateExtensions.IsPositionInTile(npc.getRect().Bottom()) || AITimer > 30)//If the bottom of the ship touches a tile, nullify speed and do dust particles
                    {
                        npc.velocity.Y = -8f;
                        if (!hasDoneExplodeEffect)
                        {
                            ExplodeEffect();
                            SoundHelper.PlayCustomSound("Sounds/Kiplosion", npc.position, 1.0f);
                        }

                    }

                    if (npc.velocity.Y == -8f)
                    {
                        slamCoolDownTimer++;
                    }
                    if (slamCoolDownTimer > 20)
                    {
                        StageAdvance();
                        AITimer = 0;
                        slamCoolDownTimer = 0;
                        locationSelected = false;
                        npc.noTileCollide = true;
                        hasDoneExplodeEffect = false;
                        if (npc.life <= npc.lifeMax * 0.50)//Reset the damage back to its normal amount
                        {
                            npc.damage = npc.damage / 2;
                        }
                    }
                }

            }
            //float ydistance = Vector2.Distance(new Vector2(0, player.position.Y), new Vector2(0, npc.position.Y));
            //Main.NewText("Y Distance is: " + ydistance);
            //Vertical projectile barrage (stage 2) - Fires a barrage of projectiles upwards that randomly spread out and fall downwards which explode on ground contact

            if (AIStage == Stage_Barrage)
            {
                AITimer++;
                npc.velocity.Y = 0;
                npc.velocity.X = 0;

                if (AITimer == 10)
                {
                    BarrageAttack();
                }

                if (AITimer > 60)
                {
                    if (npc.life < npc.lifeMax * 0.70f)
                    {
                        StageAdvance();
                    }
                    else
                    {
                        AIStage = Stage_Hover;
                    }
                    AITimer = 0;
                }
            }

            //Vertical projectile barrage + homing (stage 3) - Fires 2 projectiles in opposite arcs diagonally from the ship, after 3 seconds they stop, after 1 second both will fly towards the player.
            // These projectiles are stronger than the barrage ones, but also slower.

            if (AIStage == Stage_Homing)
            {
                npc.velocity.Y = 0;
                npc.velocity.X = 0;

                if (AITimer == 0)
                {
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 2.5f, -1f, mod.ProjectileType<FFHomingBlast>(), npc.damage / 3, 3f, Main.myPlayer);
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, -2.5f, -1f, mod.ProjectileType<FFHomingBlast>(), npc.damage / 3, 3f, Main.myPlayer);

                    if (npc.life < npc.lifeMax * 0.50f) //Fire an extra stronger projectile upwards if below 50% health
                    {
                        Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, -1f, mod.ProjectileType<FFHomingBlast>(), npc.damage / 2, 3f, Main.myPlayer);
                    }
                }
                AITimer++;
                if (AITimer > 60)
                {
                    if (npc.life < npc.lifeMax * 0.40f)
                    {
                        StageAdvance();
                    }
                    else
                    {
                        AIStage = Stage_Hover;
                    }
                    AITimer = 0;
                }
            }

            //To-Do: Summon saibamen (stage 4) - Summons a green saiba from the ship, green dust when this happens to make it look smoother (Perhaps make this something after 40% HP)
            if (Main.netMode != 1 && AIStage == Stage_Saiba)
            {
                if (AITimer == 0)
                {
                    SummonSaiba();
                    SummonFFMinions();
                }
                AITimer++;
                if (AITimer > 60)
                {
                    StageAdvance();
                    AITimer = 0;
                }
            }

            //Main.NewText(AIStage);
        }

        public void BarrageAttack()
        {
            for (int i = 0; i < 15; i++) //fire 16 of this projectile, 0 counts as a number so that's why its 15.
            {
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, -6f, mod.ProjectileType<FFBarrageBlast>(), npc.damage / 4, 3f, Main.myPlayer); ;
            }
            if (npc.life < npc.lifeMax * 0.50f) //Fire 8 extra projectiles if below 50% health
            {
                for (int i = 0; i < 8; i++)
                    Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0, -6f, mod.ProjectileType<FFBarrageBlast>(), npc.damage / 4, 3f, Main.myPlayer);
            }
        }

        public int SummonSaiba()
        {
            for (int amount = 0; amount < minionAmount; amount++)
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

        public int SummonFFMinions()
        {
            for (int amount = 0; amount < minionAmount; amount++)
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

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
            if (AIStage == Stage_Slam)
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
            AIStage++;
        }

        //Animations
        int frame = 0;
        public override void FindFrame(int frameHeight)
        {
            if(AIStage == Stage_Barrage || AIStage == Stage_Homing)
            {
                npc.frameCounter += 2;
            }
            else
            {
                npc.frameCounter++;
            }
            if (npc.frameCounter > 4)
            {
                frame++;
                npc.frameCounter = 0;
            }
            if(frame > 7) //Make it 7 because 0 is counted as a frame, making it 8 frames
            {
                frame = 0;
            }

            npc.frame.Y = frameHeight * frame;
        }

        public override void NPCLoot()
        {

            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                int choice = Main.rand.Next(0, 1);

                if (choice == 0)
                {
                    Item.NewItem(npc.getRect(), mod.ItemType<BeamRifle>());
                }
                if (choice == 1)
                {
                    //Item.NewItem(npc.getRect(), mod.ItemType<HenchBlast>());
                }
                Item.NewItem(npc.getRect(), mod.ItemType<CyberneticParts>(), Main.rand.Next(7, 18));
                Item.NewItem(npc.getRect(), mod.ItemType<ArmCannonMK2>());
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
                {
                    scaleFactor9 = 3f;
                }
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
            hasDoneExplodeEffect = true;
        }

    }
}
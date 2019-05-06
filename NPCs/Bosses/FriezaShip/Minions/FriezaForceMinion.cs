using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using DBT.Players;
using Microsoft.Xna.Framework.Graphics;

namespace DBT.NPCs.Bosses.FriezaShip.Minions
{
    public class FriezaForceMinion : ModNPC
    {
        private int jumpTimer = 0;
        private int explodeTimer = 0;
        private int soundTimer = 0;
        private bool grabbed = false;
        private int YHoverTimer = 0;

        public override string Texture
        {
            get
            {
                return "DBT/NPCs/Bosses/FriezaShip/Minions/FriezaForceMinion_0";
            }
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frieza Force Henchman");
            Main.npcFrameCount[npc.type] = 3;
        }

        public override void SetDefaults()
        {
            npc.width = 52;
            npc.height = 71;
            npc.damage = 26;
            npc.defense = 2;
            npc.lifeMax = 120;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath3;
            npc.value = 60f;
            npc.knockBackResist = 0.1f;
            npc.aiStyle = 3;
        }

        public override void AI()
        {
            Player player = Main.player[npc.target];
            npc.TargetClosest(true);

            if (Vector2.Distance(new Vector2(0, player.position.Y), new Vector2(0, npc.position.Y)) <= 120)
            {
                soundTimer++;
                if (soundTimer > (180 + Main.rand.Next(60, 120)))
                {
                    Main.PlaySound(mod.GetLegacySoundSlot(SoundType.Custom, GeGeGe()).WithVolume(0.6f));
                    soundTimer = 0;
                }
            }

            if (Vector2.Distance(new Vector2(0, player.position.Y), new Vector2(0, npc.position.Y)) <= 60)
            {

                if (Vector2.Distance(new Vector2(0, player.position.Y), new Vector2(0, npc.position.Y)) > hoverDistance.Y)
                {
                    //float hoverSpeedY = (2f + Main.rand.NextFloat(3, 8));
                    //Add a little bit of delay before moving, this lets melee players possibly get a hit in
                    YHoverTimer++;
                    if (YHoverTimer > 15)
                    {
                        npc.velocity.Y = 2f;
                    }
                }
                else if (Vector2.Distance(new Vector2(0, player.position.Y), new Vector2(0, npc.position.Y)) < hoverDistance.Y)
                {
                    //float hoverSpeedY = (-2f + Main.rand.NextFloat(-3, -8));
                    YHoverTimer++;
                    if (YHoverTimer > 15)
                    {
                        npc.velocity.Y = -2f;
                    }
                }
                else
                {
                    npc.velocity.Y = 0;
                    YHoverTimer = 0;
                }
            }
            if (grabbed)
            {
                explodeTimer++;
                if (explodeTimer > 20)
                {
                    Explode();
                }
            }
            base.AI();
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            npc.position = target.position;
            grabbed = true;
        }
        //Thanks putan for helping with this
        public float AiTexture
        {
            get
            {
                return npc.ai[3];
            }
            set
            {
                npc.ai[3] = value;
            }
        }

        public override bool PreAI()
        {
            if (AiTexture == 0 && npc.localAI[0] == 0 && Main.netMode != NetmodeID.MultiplayerClient)
            {
                AiTexture = Main.rand.Next(3);

                npc.localAI[0] = 1;
                npc.netUpdate = true;
            }

            return true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = mod.GetTexture("NPCs/Saibas/Saibaman_" + AiTexture);
            Vector2 Offset = new Vector2(0f, npc.gfxOffY);
            SpriteEffects effect = npc.spriteDirection == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            Vector2 drawOrigin = new Vector2(npc.width * 0.5f, npc.height * 0.5f);
            Vector2 drawPos = npc.position - Main.screenPosition + drawOrigin + Offset;
            spriteBatch.Draw(texture, drawPos, npc.frame, drawColor, npc.rotation, npc.frame.Size() / 2, npc.scale, effect, 0f);
            return false;
        }

        int frame = 0;
        public override void FindFrame(int frameHeight)
        {
            if (!grabbed)
            {
                npc.frameCounter += 1;
            }
            else
            {
                npc.frameCounter = 0;
            }
            if (npc.frameCounter > 4)
            {
                frame++;
                npc.frameCounter = 0;
            }
            if (frame > 2 && !grabbed)
            {
                frame = 0;
            }
            if(grabbed)
            {
                frame = 3;
            }

            npc.frame.Y = frameHeight * frame;
        }

        public void Explode()
        {
            for (int num619 = 0; num619 < 3; num619++)
            {
                float scaleFactor9 = 3f;
                if (num619 == 1)
                {
                    scaleFactor9 = 3f;
                }
                npc.width = 60;
                npc.height = 60;
                npc.damage = 140;
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
                npc.active = false;
            }
        }
        public string GeGeGe()
        {          
            switch (Main.rand.Next(0, 2))
            {
                case 0:
                    return "Sounds/Saibamen/Saibamen1";
                case 1:
                    return "Sounds/Saibamen/Saibamen2";
                case 2:
                    return "Sounds/Saibamen/Saibamen3";
                default:
                    return "";

            }
        }
    }
}

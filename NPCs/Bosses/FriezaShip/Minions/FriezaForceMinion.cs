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
        private int shootTimer = 0;
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
            npc.knockBackResist = 0f;
            npc.aiStyle = 3;
        }

        public override void AI()
        {
            Player player = Main.player[npc.target];
            npc.TargetClosest(true);

            if (Main.player[npc.target].position.Y < npc.position.Y + 240)
            {
                YHoverTimer++;
                if (YHoverTimer > 15)
                {
                    npc.velocity.Y -= 1f;
                }
            }
            if (Main.player[npc.target].position.Y > npc.position.Y + 240)
            {
                YHoverTimer++;
                if (YHoverTimer > 15)
                {
                    npc.velocity.Y += 1f;
                }
            }
            else
            {
                npc.velocity.Y = 0;
                YHoverTimer = 0;
            }
            if (Vector2.Distance(new Vector2(0, player.position.X), new Vector2(0, npc.position.X)) > 200)
            {
                npc.velocity.X = 1.5f * npc.direction;
            }
            shootTimer++;
            if (shootTimer > 180)
            {
                Vector2 newVelocity = Vector2.Normalize(npc.Center - player.Center);
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, newVelocity.X, newVelocity.Y, mod.ProjectileType("FFMinionBlast"), npc.damage, 3f, Main.myPlayer);
                shootTimer = 0;
            }
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
                AiTexture = Main.rand.Next(2);

                npc.localAI[0] = 1;
                npc.netUpdate = true;
            }

            return true;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            Texture2D texture = mod.GetTexture("NPCs/Bosses/FriezaShip/Minions/FriezaForceMinion_" + AiTexture);
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

            npc.frameCounter += 1;
            if (npc.frameCounter > 3)
            {
                frame++;
                npc.frameCounter = 0;
            }

            npc.frame.Y = frameHeight * frame;
        }
    }
}

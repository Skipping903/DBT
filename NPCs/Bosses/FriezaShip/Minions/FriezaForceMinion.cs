using DBT.Commons.Items;
using DBT.NPCs.Bosses.FriezaShip.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.NPCs.Bosses.FriezaShip.Minions
{
    public abstract class FriezaForceMinion : DBTNPC
    {
        protected FriezaForceMinion(string displayName, int width, int height, int health, LegacySoundStyle hitSound, LegacySoundStyle deathSound, int aistyle, int aitype = 0, int defense = 0, int damage = 0, float value = 0f, float knockbackResist = 0f, int frameCount = 0) : base(displayName, width, height, health, hitSound, deathSound, aistyle)
        {
        }

        public override void AI()
        {
            base.AI();
            Player player = Main.player[npc.target];
            npc.TargetClosest(true);

            //Thanks UncleDanny for this <3
            if (Main.player[npc.target].position.Y < npc.position.Y + 260)
            {
                npc.velocity.Y -= npc.velocity.Y > 0f ? 1.2f : 0.20f;
            }
            if (Main.player[npc.target].position.Y > npc.position.Y + 260)
            {
                npc.velocity.Y += npc.velocity.Y < 0f ? 1.2f : 0.20f;
            }

            if (Vector2.Distance(new Vector2(player.position.X, 0), new Vector2(npc.position.X, 0)) > 100)
            {
                npc.velocity.X = 2f * npc.direction;
            }
            shootTimer++;
            if (shootTimer > 180)
            {
                Vector2 newVelocity = Vector2.Normalize(Main.player[npc.target].Center + Main.player[npc.target].velocity - npc.Center) * 18;
                Projectile.NewProjectile(npc.Center.X, npc.Center.Y, newVelocity.X, newVelocity.Y, mod.ProjectileType<FFMinionBlast>(), npc.damage / 3, 3f, Main.myPlayer);
                shootTimer = 0;
            }
        }

        int frame = 0;
        public override void FindFrame(int frameHeight)
        {
            base.FindFrame(frameHeight);
            npc.frameCounter += 1;
            if (npc.frameCounter > 4)
            {
                frame++;
                npc.frameCounter = 0;
            }
            if (frame > 2)
            {
                frame = 0;
            }

            npc.frame.Y = frameHeight * frame;
        }

        private int shootTimer = 0;
        private int YHoverTimer = 0;
        private bool assignedTexture = false;
    }
}

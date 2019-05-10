using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using DBT.Players;
using Microsoft.Xna.Framework.Graphics;
using DBT.NPCs.Bosses.FriezaShip.Projectiles;

namespace DBT.NPCs.Bosses.FriezaShip.Minions
{
    public class FriezaForceMinion1 : FriezaForceMinion
    {
        public FriezaForceMinion1() : base("Frieza Force Henchman", 52, 71, 120, SoundID.NPCHit1, SoundID.NPCDeath3, 3, 0, 2, 26, 60f, 0f, 3)
        {
        }
    }
}

using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using DBT.Players;
using Microsoft.Xna.Framework.Graphics;
using DBT.NPCs.Saibas;

namespace DBT.NPCs.Bosses.FriezaShip.Minions
{
    public class Saibaman2 : Saibaman
    {
        public Saibaman2() : base("Blue Saibaman", 26, 36, 50, SoundID.NPCHit1, SoundID.NPCDeath1, 3, NPCID.Zombie, 4, 12, 60f, 0.3f, 4)
        {
        }
    }
}

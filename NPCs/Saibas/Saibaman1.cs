using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using DBT.Players;
using Microsoft.Xna.Framework.Graphics;
using DBT.NPCs.Saibas;

namespace DBT.NPCs.Bosses.FriezaShip.Minions
{
    public class Saibaman1 : Saibaman
    {
        public Saibaman1() : base("Green Saibaman", 26, 36, 50, SoundID.NPCHit1, SoundID.NPCDeath1, 3, NPCID.Zombie, 4, 12, 60f, 0.3f, 4)
        {
        }
    }
}

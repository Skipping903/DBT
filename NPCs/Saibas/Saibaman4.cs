using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using DBT.Players;
using Microsoft.Xna.Framework.Graphics;
using DBT.NPCs.Saibas;

namespace DBT.NPCs.Saibas
{
    public class Saibaman4 : Saibaman
    {
        public Saibaman4() : base("Purple Saibaman", 26, 36, 50, SoundID.NPCHit1, SoundID.NPCDeath1, 3, NPCID.Zombie, 4, 12, 60f, 0.3f, 4)
        {
        }
    }
}

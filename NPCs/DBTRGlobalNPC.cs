using DBTR.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBTR.NPCs
{
    public sealed class DBTRGlobalNPC : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            DBTRPlayer dbtrPlayer = Main.LocalPlayer.GetModPlayer<DBTRPlayer>();

            if (dbtrPlayer == null)
                return;

            if (npc.lastInteraction == Main.LocalPlayer.whoAmI)
                dbtrPlayer.OnKilledNPC(npc);
        }
    }
}

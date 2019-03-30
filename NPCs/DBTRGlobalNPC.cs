using DBTMod.Players;
using Terraria;
using Terraria.ModLoader;

namespace DBTMod.NPCs
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

using System;
using DBTR.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBTR.Auras
{
    public sealed class AuraDrawPlayer : PlayerLayer
    {
        public AuraDrawPlayer(int index) : base(DBTRMod.Instance.GetType().Name + index, "AuraLayer0", null, DrawLayer)
        {
        }

        private static void DrawLayer(PlayerDrawInfo drawInfo)
        {
            if (Main.netMode == NetmodeID.Server) return;

            Player player = drawInfo.drawPlayer;
            DBTRPlayer dbtrPlayer = player.GetModPlayer<DBTRPlayer>();

            AuraAppearance aura = dbtrPlayer.GetAura();
            if (aura == null) return;

            dbtrPlayer.DrawAura(aura);
        }
    }
}

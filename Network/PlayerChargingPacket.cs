using System.IO;
using DBTMod.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBTMod.Network
{
    public sealed class PlayerChargingPacket : NetworkPacket
    {
        public override bool Receive(BinaryReader reader, int fromWho)
        {
            byte whichPlayer = reader.ReadByte();
            bool charging = reader.ReadBoolean();

            if (Main.netMode == NetmodeID.Server)
                SendPacketToAllClients(fromWho, whichPlayer, charging);

            DBTRPlayer dbtrPlayer = Main.player[whichPlayer].GetModPlayer<DBTRPlayer>();
            dbtrPlayer.IsCharging = charging;

            return true;
        }

        public override void SendPacket(int toWho, int fromWho, params object[] args)
        {
            ModPacket packet = MakePacket();

            packet.Write((byte) args[0]);
            packet.Write((bool) args[1]);

            packet.Send(toWho, fromWho);
        }
    }
}

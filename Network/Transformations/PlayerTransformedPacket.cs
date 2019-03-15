using System.IO;
using DBTR.Players;
using DBTR.Transformations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBTR.Network.Transformations
{
    public sealed class PlayerTransformedPacket : NetworkPacket
    {
        public override bool Receive(BinaryReader reader, int fromWho)
        {
            byte whichPlayer = reader.ReadByte();
            string transformationName = reader.ReadString();

            if (Main.netMode == NetmodeID.Server)
                SendPacketToAllClients(fromWho, whichPlayer, transformationName);

            DBTRPlayer dbtrPlayer = Main.player[whichPlayer].GetModPlayer<DBTRPlayer>();
            dbtrPlayer.AcquireAndTransform(TransformationDefinitionManager.Instance[transformationName]);

            return true;
        }

        public override void SendPacket(int toWho, int fromWho, params object[] args)
        {
            ModPacket packet = MakePacket();

            packet.Write((byte) args[0]);
            packet.Write((string) args[1]);

            packet.Send(toWho, fromWho);
        }
    }
}

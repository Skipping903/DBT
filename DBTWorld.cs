using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using System.IO;

namespace DBT
{
    public class DBTWorld : ModWorld
    {
        public static bool downedFriezaShip = false;
        public static bool friezaShipTriggered = false;

        public override void Initialize()
        {
            downedFriezaShip = false;
        }

        public override TagCompound Save()
        {
            List<string> downed = new List<string>();

            if (downedFriezaShip) downed.Add("friezaShip");

            return new TagCompound { { "downed", downed } };
        }

        public override void Load(TagCompound tag)
        {
            IList<string> downed = tag.GetList<string>("downed");
            downedFriezaShip = downed.Contains("friezaShip");
        }

        public override void LoadLegacy(BinaryReader reader)
        {
            int loadVersion = reader.ReadInt32();

            if (loadVersion == 0)
            {
                BitsByte flags = reader.ReadByte();
                downedFriezaShip = flags[0];
            }
        }

        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = downedFriezaShip;

            writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            downedFriezaShip = flags[0];
        }
    }
}

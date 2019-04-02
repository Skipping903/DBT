using System;
using System.Collections.Generic;
using System.IO;
using DBT.Network.Transformations;

namespace DBT.Network
{
    public sealed class NetworkPacketManager
    {
        private static NetworkPacketManager _instance;

        private byte _latestPacketTypeId = 1;
        private Dictionary<byte, NetworkPacket> _networkPacketsById = new Dictionary<byte, NetworkPacket>();
        private Dictionary<Type, NetworkPacket> _networkPacketsByType = new Dictionary<Type, NetworkPacket>();

        public void DefaultInitialize()
        {
            PlayerTransformedPacket = Add(new PlayerTransformedPacket()) as PlayerTransformedPacket;
            PlayerChargingPacket = Add(new PlayerChargingPacket()) as PlayerChargingPacket;

            Initialized = true;
        }

        public NetworkPacket Add(NetworkPacket networkPacket)
        {
            if (_networkPacketsById.ContainsValue(networkPacket))
                return _networkPacketsByType[networkPacket.GetType()];

            _networkPacketsById.Add(_latestPacketTypeId, networkPacket);

            networkPacket.PacketType = _latestPacketTypeId;
            _latestPacketTypeId++;

            return networkPacket;
        }

        public void HandlePacket(BinaryReader reader, int fromWho)
        {
            byte packetType = reader.ReadByte();

            _networkPacketsById[packetType].Receive(reader, fromWho);
        }


        public PlayerTransformedPacket PlayerTransformedPacket { get; private set; }
        public PlayerChargingPacket PlayerChargingPacket { get; private set; }

        public bool Initialized { get; private set; }

        public NetworkPacket this[byte packetType] => _networkPacketsById[packetType];


        public static NetworkPacketManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new NetworkPacketManager();

                if (!_instance.Initialized)
                    _instance.DefaultInitialize();

                return _instance;
            }
        }
    }
}

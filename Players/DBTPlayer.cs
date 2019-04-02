using DBT.Network;
using DBT.Transformations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Players
{
    public sealed partial class DBTPlayer : ModPlayer
    {
        private bool _isCharging;


        public void ModifyKi(float kiAmount)
        {
            // TODO Add mastery for being in a form, if need be.

            float projectedKi = Ki + kiAmount;

            if (projectedKi > MaxKi)
                Ki = MaxKi;
            else
                Ki = projectedKi;
        }
        

        public void OnKilledNPC(NPC npc)
        {
            TransformationDefinitionManager.Instance.ForAllItems(t => t.OnPreAcquirePlayerKilledNPC(this, npc));
            ForAllActiveTransformations(t => t.OnActivePlayerKilledNPC(this, npc));
        }

        public bool IsCharging
        {
            get { return _isCharging; }
            set
            {
                if (_isCharging == value)
                    return;

                _isCharging = value;

                if (Main.netMode == NetmodeID.MultiplayerClient && player.whoAmI == Main.myPlayer)
                    NetworkPacketManager.Instance.PlayerChargingPacket.SendPacketToServer(Main.myPlayer, (byte) Main.myPlayer, value);
            }
        }

        public bool PlayerInitialized { get; private set; }
    }
}

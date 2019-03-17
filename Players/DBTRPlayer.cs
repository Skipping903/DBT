using System;
using DBTR.Network;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBTR.Players
{
    public sealed partial class DBTRPlayer : ModPlayer
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
            ForAllActiveTransformations(t => t.OnPlayerKilledNPC(this, npc));
        }


        #region Ki

        public float KiMultiplier { get; set; }

        #region Current Ki

        public float Ki { get; private set; }

        #endregion

        #region Max Ki

        public float BaseMaxKi { get; private set; }

        public float MaxKiMultiplier { get; private set; } = 1;

        public int MaxKi => (int)Math.Round(BaseMaxKi * MaxKiMultiplier);

        #endregion

        #endregion


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

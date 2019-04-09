using System;
using DBT.Network;
using Terraria;
using Terraria.ID;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        private bool _isCharging;


        public float ModifyKi(float kiAmount)
        {
            // TODO Add mastery for being in a form, if need be.

            float projectedKi = Ki + kiAmount;

            if (projectedKi < 0)
                projectedKi = 0;
            else if (projectedKi > MaxKi)
                projectedKi = MaxKi;
            
            Ki = projectedKi;
            return Ki;
        }

        internal void ResetEffectsKi()
        {
            KiDamageMultiplier = 1;
            KiChargeRate = 1;

            MaxKiMultiplier = 1;
            MaxKiModifier = 1;

            KiSpeedAddition = 0;
            KiKnockbackAddition = 0;
        }

        internal void PreUpdateKi()
        {
            if (IsCharging)
                Ki += KiChargeRate;
        }

        public float KiDamageMultiplier { get; set; } = 1;


        public float Ki { get; private set; }

        public float KiChargeRate { get; internal set; }

        public bool IsCharging
        {
            get { return _isCharging; }
            set
            {
                if (_isCharging == value)
                    return;

                _isCharging = value;

                if (Main.netMode == NetmodeID.MultiplayerClient && player.whoAmI == Main.myPlayer)
                    NetworkPacketManager.Instance.PlayerChargingPacket.SendPacketToServer(Main.myPlayer, (byte)Main.myPlayer, value);
            }
        }

        public float BaseMaxKi { get; private set; }

        public float MaxKiMultiplier { get; set; } = 1;

        public float MaxKiModifier { get; set; }

        public int MaxKi => (int)(Math.Round(BaseMaxKi * MaxKiMultiplier) + MaxKiModifier);


        public float KiSpeedAddition { get; set; }

        public float KiKnockbackAddition { get; set; }
    }
}

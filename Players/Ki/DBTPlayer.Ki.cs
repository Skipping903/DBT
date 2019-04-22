using System;
using System.Collections.Generic;
using DBT.Commons;
using DBT.Extensions;
using DBT.Network;
using Terraria;
using Terraria.ID;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        private bool _isCharging;


        public float ModifyKi(float KiAmount)
        {
            // TODO Add mastery for being in a form, if need be.

            float projectedKi = Ki + KiAmount;

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
            KiCritAddition = 0;

            NaturalKiRegeneration = 0;
            ExtraKiRegeneration = 0;

            KiOrbRestoreAmount = 100;
            KiOrbGrabRange = 2;

            KiChargeRate = 0;
            KiChargeRateMultiplierLimit = 0;

            KiDrainMultiplier = 1;
        }

        internal void PreUpdateKi()
        {
            
        }

        internal void PostUpdateHandleKi()
        {
            if (IsCharging)
            {
                ModifyKi(KiChargeRate);

                List<IUpdatesOnChargeTick> items = player.GetItemsInInventory<IUpdatesOnChargeTick>(accessories: true, armor: true);
                float defenseMultiplier = 1;

                for (int i = 0; i < items.Count; i++)
                    items[i].OnPlayerPostUpdateChargingTick(this, ref defenseMultiplier);

                player.statDefense = (int)(defenseMultiplier * player.statDefense);
            }
        }

        public float KiDamageMultiplier { get; set; } = 1;


        public float Ki { get; private set; }

        public float KiChargeRate { get; set; }
        public float KiChargeRateMultiplierLimit { get; set; }


        public float NaturalKiRegeneration { get; set; }
        public float ExtraKiRegeneration { get; set; }

        public float KiOrbRestoreAmount { get; set; }
        public float KiOrbGrabRange { get; set; }

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


        public float BaseMaxKi { get; set; }

        public float MaxKiMultiplier { get; set; } = 1;
        public float MaxKiModifier { get; set; }

        public int MaxKi => (int)(Math.Round(BaseMaxKi * MaxKiMultiplier) + MaxKiModifier);


        public float KiSpeedAddition { get; set; }
        public float KiKnockbackAddition { get; set; }
        public int KiCritAddition { get; set; }

        public float KiDrainMultiplier { get; set; }
    }
}

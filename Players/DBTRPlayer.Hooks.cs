using DBTR.HairStyles;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBTR.Players
{
    public sealed partial class DBTRPlayer
    {
        private const float CHARGING_MOVE_SPEED_MULTIPLIER = 0.5f;

        public override void Initialize()
        {
            Aura = null;
            ActiveTransformations.Clear();

            if (!PlayerInitialized)
            {
                ChosenHairStyle = HairStyleManager.Instance.NoChoice;

                Ki = 0;
                BaseMaxKi = 1000;
            }

            PlayerInitialized = true;
        }

        #region Pre Update

        public override void PreUpdate()
        {
            Ki = MaxKi;
        }

        #endregion


        #region Post Update

        public override void PostUpdate()
        {
            FirstTransformation = GetFirstTransformation();

            if (Main.netMode != NetmodeID.Server)
            {
                PostUpdateHandleAura();
                PostHandleHair();
            }
        }

        public override void PostUpdateRunSpeeds()
        {
            if (IsCharging)
            {
                player.moveSpeed *= CHARGING_MOVE_SPEED_MULTIPLIER;
                player.maxRunSpeed *= CHARGING_MOVE_SPEED_MULTIPLIER;
                player.runAcceleration *= CHARGING_MOVE_SPEED_MULTIPLIER;
            }
        }

        #endregion


        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            HandleAuraDrawLayers(layers);
            HandleHairDrawLayers(layers);
        }


        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            ForAllActiveTransformations(p => p.OnPlayerDied(this, damage, pvp));
            ClearTransformations();

            if (Main.netMode == NetmodeID.MultiplayerClient && player.whoAmI == Main.myPlayer)
                IsCharging = false;
        }
    }
}
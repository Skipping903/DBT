using System.Collections.Generic;
using DBT.HairStyles;
using DBT.Transformations;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        private const float CHARGING_MOVE_SPEED_MULTIPLIER = 0.5f;

        public override void Initialize()
        {
            Aura = null;
            InitializeTransformations();

            if (!PlayerInitialized)
            {
                ChosenHairStyle = HairStyleManager.Instance.NoChoice;

                Ki = 0;
                BaseMaxKi = 1000;
            }

            PlayerInitialized = true;
        }


        public override void ResetEffects()
        {
            ResetEffectsKi();
        }


        #region Pre Update

        public override void PreUpdate()
        {
            PreUpdateKi();
        }

        #endregion


        #region Post Update

        public override void PostUpdate()
        {
            FirstTransformation = GetTransformation();

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
            TransformationDefinitionManager.Instance.ForAllItems(t => t.OnPreAcquirePlayerDied(this, damage, pvp, damageSource));

            ForAllActiveTransformations(p => p.OnActivePlayerDied(this, damage, pvp, damageSource));
            ClearTransformations();

            // TODO Check if this is required.
            //if (Main.netMode == NetmodeID.MultiplayerClient && player.whoAmI == Main.myPlayer)
                IsCharging = false;
        }
    }
}
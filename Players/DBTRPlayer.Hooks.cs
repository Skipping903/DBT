using System.Collections.Generic;
using DBTR.Transformations;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ModLoader;

namespace DBTR.Players
{
    public sealed partial class DBTRPlayer
    {
        public override void Initialize()
        {
            Aura = null;
            ActiveTransformations.Clear();
        }


        public override void PreUpdate()
        {
            Ki = MaxKi;

            if (DBTRMod.Instance.transformUpKey.JustPressed)
                Transform(TransformationDefinitionManager.Instance.SSJG);

            if (DBTRMod.Instance.transformDownKey.JustPressed)
                Untransform(TransformationDefinitionManager.Instance.SSJG);
        }

        public override void PostUpdate()
        {
            PostUpdateHandleAura();
        }


        public override void ModifyDrawLayers(List<PlayerLayer> layers)
        {
            HandleAuraDrawLayers(layers);
        }


        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            ForAllActiveTransformations(p => p.Definition.OnPlayerDied(this, damage, pvp));

            ClearTransformations();
        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            IsCharging = DBTRMod.Instance.energyChargeKey.Current;

            // TODO Add network synchronization for charging.
        }
    }
}
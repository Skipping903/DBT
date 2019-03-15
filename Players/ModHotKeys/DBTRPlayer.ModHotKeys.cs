using DBTR.Transformations;
using Terraria;
using Terraria.GameInput;

namespace DBTR.Players
{
    public sealed partial class DBTRPlayer
    {
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            IsCharging = DBTRMod.Instance.energyChargeKey.Current;

            if (player.whoAmI == Main.myPlayer)
            {
                if (DBTRMod.Instance.transformUpKey.JustPressed)
                    AcquireAndTransform(TransformationDefinitionManager.Instance.SSJG);

                if (DBTRMod.Instance.transformDownKey.JustPressed)
                    Untransform(TransformationDefinitionManager.Instance.SSJG);
            }
        }
    }
}

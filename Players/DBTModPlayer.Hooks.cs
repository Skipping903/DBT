using DBTRMod.Transformations;
using Terraria.DataStructures;

namespace DBTRMod.Players
{
    public sealed partial class DBTModPlayer
    {
        public override void PreUpdate()
        {
            Ki = MaxKi;

            if (DBTRMod.Instance.transformUpKey.JustPressed)
                Transform(TransformationDefinitionManager.Instance.SSJ1Definition);

            if (DBTRMod.Instance.transformDownKey.JustPressed)
                Untransform(TransformationDefinitionManager.Instance.SSJ1Definition);
        }

        public override void Kill(double damage, int hitDirection, bool pvp, PlayerDeathReason damageSource)
        {
            ForAllActiveTransformations(p => p.Definition.OnPlayerDied(this, damage, pvp));

            ClearTransformations();
        }
    }
}
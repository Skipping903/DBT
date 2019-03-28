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
                if (DBTRMod.Instance.transformUpKey.JustPressed && SelectedTransformation != null)
                    Transform(SelectedTransformation);

                if (DBTRMod.Instance.transformDownKey.JustPressed)
                    ClearTransformations();

                if (DBTRMod.Instance.characterMenuKey.JustPressed)
                    DBTRMod.Instance.characterMenu.Visible = !DBTRMod.Instance.characterMenu.Visible;
            }
        }
    }
}

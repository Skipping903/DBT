using Terraria;
using Terraria.GameInput;

namespace DBTMod.Players
{
    public sealed partial class DBTRPlayer
    {
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            IsCharging = DBTMod.Instance.energyChargeKey.Current;

            if (player.whoAmI == Main.myPlayer)
            {
                if (DBTMod.Instance.transformUpKey.JustPressed && SelectedTransformation != null)
                    Transform(SelectedTransformation);

                if (DBTMod.Instance.transformDownKey.JustPressed)
                    ClearTransformations();

                if (DBTMod.Instance.characterMenuKey.JustPressed)
                    DBTMod.Instance.characterMenu.Visible = !DBTMod.Instance.characterMenu.Visible;
            }
        }
    }
}

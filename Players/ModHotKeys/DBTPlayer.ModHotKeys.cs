using Terraria;
using Terraria.GameInput;

namespace DBT.Players
{
    public sealed partial class DBTPlayer
    {
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            IsCharging = DBTMod.Instance.energyChargeKey.Current;

            if (player.whoAmI == Main.myPlayer)
            {
                if (DBTMod.Instance.transformUpKey.JustPressed && SelectedTransformations != null)
                    TryTransforming(SelectedTransformations);

                if (DBTMod.Instance.transformDownKey.JustPressed)
                    ClearTransformations();

                if (DBTMod.Instance.characterMenuKey.JustPressed)
                    DBTMod.Instance.dbtMenu.Visible = !DBTMod.Instance.dbtMenu.Visible;
            }
        }
    }
}

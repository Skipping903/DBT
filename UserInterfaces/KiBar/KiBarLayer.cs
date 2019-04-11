using Terraria;
using Terraria.UI;

namespace DBT.UserInterfaces.KiBar
{
    public sealed class KiBarLayer : GameInterfaceLayer
    {
        public KiBarLayer() : base(typeof(KiBar).FullName, InterfaceScaleType.UI)
        {
        }

        protected override bool DrawSelf()
        {
            if (DBTMod.Instance.KiBar.Visible)
            {
                DBTMod.Instance.KiBarInterface.Update(Main._drawInterfaceGameTime);
                DBTMod.Instance.KiBar.Draw(Main.spriteBatch);
            }

            return true;
        }
    }
}

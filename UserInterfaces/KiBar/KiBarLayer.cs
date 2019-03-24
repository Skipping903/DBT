using Terraria;
using Terraria.UI;

namespace DBTR.UserInterfaces.KiBar
{
    public sealed class KiBarLayer : GameInterfaceLayer
    {
        public KiBarLayer() : base(typeof(KiBar).FullName, InterfaceScaleType.UI)
        {
        }

        protected override bool DrawSelf()
        {
            if (DBTRMod.Instance.kiBar.Visible)
            {
                DBTRMod.Instance.kiBarInterface.Update(Main._drawInterfaceGameTime);
                DBTRMod.Instance.kiBar.Draw(Main.spriteBatch);
            }

            return true;
        }
    }
}

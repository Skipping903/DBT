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
            if (DBTRMod.kiBar.Visible)
            {
                DBTRMod.kiBarInterface.Update(Main._drawInterfaceGameTime);
                DBTRMod.kiBar.Draw(Main.spriteBatch);
            }

            return true;
        }
    }
}

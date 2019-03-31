using Terraria;
using Terraria.UI;

namespace DBTMod.UserInterfaces.CharacterMenus
{
    public sealed class DBTMenuLayer : GameInterfaceLayer
    {
        public DBTMenuLayer(DBTMenu menu, UserInterface interf) : base(menu.GetType().FullName, InterfaceScaleType.UI)
        {
            Menu = menu;
            Interface = interf;
        }

        protected override bool DrawSelf()
        {
            if (Menu.Visible)
                Interface.Draw(Main.spriteBatch, Main._drawInterfaceGameTime);

            return true;
        }

        public DBTMenu Menu { get; }

        public UserInterface Interface { get; }
    }
}

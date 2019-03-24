using Terraria;
using Terraria.UI;

namespace DBTR.UserInterfaces.CharacterMenus
{
    public sealed class CharacterMenuLayer : GameInterfaceLayer
    {
        public CharacterMenuLayer(CharacterMenu menu, UserInterface interf) : base(typeof(CharacterMenu).FullName, InterfaceScaleType.UI)
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

        public CharacterMenu Menu { get; }

        public UserInterface Interface { get; }
    }
}

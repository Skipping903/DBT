using DBTMod.Players;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;

namespace DBTMod.UserInterfaces.Buttons
{
    // Credits to ExampleMod (BlushieMagic).
    public class UIHoverImageButton : UIImageButton
    {
        public UIHoverImageButton(Texture2D texture, string hoverText) : base(texture)
        {
            HoverText = HoverText;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            if (IsMouseHovering)
            {
                DBTRPlayer dbtrPlayer = Main.LocalPlayer.GetModPlayer<DBTRPlayer>();

                if (dbtrPlayer == null)
                    Main.hoverItemName = HoverText;
                else
                    Main.hoverItemName = GetHoverText(dbtrPlayer);
            }
        }


        public virtual string GetHoverText(DBTRPlayer dbtrPlayer) => HoverText;


        public string HoverText { get; }
    }
}

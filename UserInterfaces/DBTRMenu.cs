using DBTR.UserInterfaces.Buttons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace DBTR.UserInterfaces
{
    // Credit to X3n0ph0b3 / MerceriusXeno.
    public class DBTRMenu : UIState
    {
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }


        protected void InitializeButton(ref UIImageButton button, Texture2D texture, MouseEvent onClick, float offsetX, float offsetY, UIElement parent = null)
        {
            button = new UIImageButton(texture);
            button.OnClick += onClick;

            InitializeUIElement<UIImageButton>(ref button, texture, offsetX, offsetY, parent);
        }

        protected void InitializeHoverTextButton(ref UIHoverImageButton button, Texture2D texture, string hoverText, MouseEvent onClick, float offsetX, float offsetY, UIElement parent = null)
        {
            button = new UIHoverImageButton(texture, hoverText);
            button.OnClick += onClick;

            InitializeUIElement<UIHoverImageButton>(ref button, texture, offsetX, offsetY, parent);
        }

        protected void InitializeImage(ref UIImage image, Texture2D texture, float offsetX, float offsetY, UIElement parent = null)
        {
            image = new UIImage(texture);

            InitializeUIElement<UIImage>(ref image, texture, offsetX, offsetY, parent);
        }

        protected void InitializeText(ref UIText text, string shownText, float offsetX, float offsetY, float scale = 1, Color color = default(Color), UIElement parent = null)
        {
            text = new UIText(shownText, scale);

            text.Width.Set(16f, 0f);
            text.Height.Set(16f, 0f);
            text.Left.Set(offsetX, 0f);
            text.Top.Set(offsetY, 0f);

            text.TextColor = color;

            if (parent == null)
                BackPanel.Append(parent);
            else
                parent.Append(parent);
        }


        private void InitializeUIElement<T>(ref T element, Texture2D texture, float offsetX, float offsetY, UIElement parent = null) where T : UIElement
        {
            element.Width.Set(texture.Width, 0.0f);
            element.Height.Set(texture.Height, 0.0f);
            element.Left.Set(offsetX, 0f);
            element.Top.Set(offsetY, 0f);

            if (parent == null)
                BackPanel.Append(element);
            else
                parent.Append(element);
        }


        protected void DragStart(UIMouseEvent evt, UIElement element)
        {
            Offset = new Vector2(evt.MousePosition.X - BackPanel.Left.Pixels, evt.MousePosition.Y - BackPanel.Top.Pixels);
            Dragging = true;
        }

        protected void DragEnd(UIMouseEvent evt, UIElement element)
        {
            Vector2 end = evt.MousePosition;
            Dragging = false;

            BackPanel.Left.Set(end.X - Offset.X, 0f);
            BackPanel.Top.Set(end.Y - Offset.Y, 0f);

            Recalculate();
        }


        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);
            Vector2 mousePosition = new Vector2((float)Main.mouseX, (float)Main.mouseY);

            if (BackPanel.ContainsPoint(mousePosition))
                Main.LocalPlayer.mouseInterface = true;

            if (Dragging)
            {
                BackPanel.Left.Set(mousePosition.X - Offset.X, 0f);
                BackPanel.Top.Set(mousePosition.Y - Offset.Y, 0f);

                Recalculate();
            }
        }


        public UIPanel BackPanel { get; private set; }

        public Vector2 Offset { get; private set; }

        public bool Dragging { get; private set; }
    }
}

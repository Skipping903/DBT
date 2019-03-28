using Microsoft.Xna.Framework;
using Terraria.GameContent.UI.Elements;
using Terraria.UI;

namespace DBTR.UserInterfaces
{
    public struct UIImagePair
    {
        public Point position;

        public readonly UIImageButton button;
        public readonly UIImage unknownImage, unknownImageGray, lockedImage;

        public UIImagePair(Point position, UIImageButton button, UIImage unknownImage, UIImage unknownImageGray, UIImage lockedImage)
        {
            this.position = position;

            this.button = button;
            this.unknownImage = unknownImage;
            this.unknownImageGray = unknownImageGray;
            this.lockedImage = lockedImage;
        }
    }
}

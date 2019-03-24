using Terraria.GameContent.UI.Elements;

namespace DBTR.UserInterfaces
{
    public struct UIImagePair
    {
        public UIImageButton button;
        public UIImage unknownImage, unknownImageGray, lockedImage;

        public UIImagePair(UIImageButton button, UIImage unknownImage, UIImage unknownImageGray, UIImage lockedImage)
        {
            this.button = button;
            this.unknownImage = unknownImage;
            this.unknownImageGray = unknownImageGray;
            this.lockedImage = lockedImage;
        }
    }
}

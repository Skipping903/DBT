namespace DBT.Items.Accessories.Necklaces
{
    public abstract class Necklace : DBTAccessory
    {
        protected Necklace(string displayName, string tooltip, int width, int height, int value, int defense, int rare) : base(displayName, tooltip, value, defense, rare)
        {
            Width = width;
            Height = height;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.width = Width;
            item.height = Height;
            item.accessory = true;
        }

        public int Width { get; }

        public int Height { get; }
    }
}

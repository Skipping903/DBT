namespace DBT.Items.Accessories.Necklaces
{
    public abstract class Necklace : DBTItem
    {
        protected Necklace(string displayName, string tooltip, int width, int height, int value, int rare, int defense) : base(displayName, tooltip)
        {
            Width = width;
            Height = height;

            Value = value;
            Rare = rare;

            Defense = defense;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.width = Width;
            item.height = Height;
            item.value = Value;
            item.rare = Rare;
            item.accessory = true;
            item.defense = Defense;
        }

        public int Width { get; }

        public int Height { get; }

        public int Value { get; }

        public int Rare { get; }

        public int Defense { get; }
    }
}

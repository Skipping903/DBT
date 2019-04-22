namespace DBT.Items.Accessories.Earthens
{
    public abstract class EarthenItem : DBTAccessory
    {
        protected EarthenItem(string displayName, string tooltip, int width, int height, int value, int defense, int rarity) : base(displayName, tooltip, width, height, value, defense, rarity)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.accessory = true;
        }
    }
}
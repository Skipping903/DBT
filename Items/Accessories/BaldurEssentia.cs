using Terraria.ID;

namespace DBT.Items.Accessories
{
    public sealed class BaldurEssentia : DBTItem
    {
        // TODO Add effect on charge.
        public BaldurEssentia() : base("Baldur Essentia", "The essence of strong defense.\nCharging grants a protective barrier that grants drastically increased defense")
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.width = 18;
            item.height = 30;
            item.value = 3 * Constants.SILVER_VALUE_MULTIPLIER;
            item.rare = ItemRarityID.LightRed;
            item.defense = 6;
            item.accessory = true;
        }

        // TODO Add functionality
    }
}
using Terraria.ID;

namespace DBT.Items.Accessories.Baldurs
{
    public sealed class BuldariumSigmite : BaldurItem
    {
        public BuldariumSigmite() : base("Buldarium Sigmite", "'A fragment of the God of Defense's soul'\nCharging grants a protective barrier that grants massively increased defense\nCharging also grants drastically increased life regen\nIncreased Ki charge rate",
            180 * Constants.SILVER_VALUE_MULTIPLIER, 10, ItemRarityID.Yellow, 0.5f)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.width = 18;
            item.height = 30;
            item.accessory = true;
        }
    }
}
using DBT.Commons;
using DBT.Extensions;
using DBT.Players;
using Terraria.ID;

namespace DBT.Items.Accessories.Baldurs
{
    public sealed class BaldurEssentia : BaldurItem
    {
        public BaldurEssentia() : base("Baldur Essentia", "'The essence of strong defense'\nCharging grants a protective barrier that grants drastically increased defense",
            3 * Constants.SILVER_VALUE_MULTIPLIER, 6, ItemRarityID.LightRed, 0.3f)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.accessory = true;
        }
    }
}
using DBT.Commons.Items;
using Terraria.ID;

namespace DBT.Items.Accessories
{
    public sealed class BloodstainedBandana : DBTItem, IHasValue, IHasDefense, IHasRarity
    {
        public BloodstainedBandana() : base("Bloodstained Bandana", "'Change the future'\n14% Increased Ki damage\nThorns effect", 
            18, 30, value: 80 * Constants.SILVER_VALUE_MULTIPLIER, defense: 2, rarity: ItemRarityID.LightRed)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.accessory = true;
        }
    }
}
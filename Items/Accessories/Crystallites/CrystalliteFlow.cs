using Terraria.ID;

namespace DBT.Items.Accessories.Crystallites
{
    public sealed class CrystalliteFlow : Crystallite
    {
        public CrystalliteFlow() : base("Influunt Crystallite", "'The essence of a calm flowing spirit lives within the crystal.'\nGreatly Increased speed while charging\n+1000 Max ki",
            22, 34, 640 * Constants.SILVER_VALUE_MULTIPLIER, ItemRarityID.Pink, 1000)
        {
        }
    }
}
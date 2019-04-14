using Terraria;
using Terraria.ID;

namespace DBT.Items.Accessories.Crystallites
{
    public sealed class CrystalliteControl : Crystallite
    {
        public CrystalliteControl() : base("Imperium Crystallite", "'The essence of pure ki control lives within the crystal.'\nIncreased speed while charging\n+500 Max ki", 
            22, 34, 240 * Constants.SILVER_VALUE_MULTIPLIER, ItemRarityID.LightRed, 500)
        {  
        }


    }
}
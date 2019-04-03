using Terraria.ID;

namespace DBT.Items.Accessories.Infusers
{
    public class RubyInfuser : Infuser
    {
        public RubyInfuser() : base("Ruby Ki Infuser", "Hitting enemies with ki attacks inflicts bleeding.", 220 * Constants.SILVER_VALUE_MULTIPLIER, ItemID.Ruby)
        {
        }
    }
}
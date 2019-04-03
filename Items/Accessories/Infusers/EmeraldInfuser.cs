using Terraria.ID;

namespace DBT.Items.Accessories.Infusers
{
    public sealed class EmeraldInfuser : Infuser
    {
        public EmeraldInfuser() : base("Emerald Ki Infuser", "Hitting enemies with ki attacks inflicts poison.", 185 * Constants.SILVER_VALUE_MULTIPLIER, ItemID.Emerald)
        {
        }
    }
}
using Terraria.ID;
using Terraria;

namespace DBT.Items.Accessories.Baldurs
{
    public sealed class BaldurEssentia : BaldurItem
    {
        public BaldurEssentia() : base("Baldur Essentia", 
            "'The essence of a strong defense.'" +
            "\nCharging grants a protective barrier that grants drastically increased defense",
            Item.buyPrice(silver: 3), 6, ItemRarityID.LightRed, 0.3f)
        {
        }
    }
}
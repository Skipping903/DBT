using Terraria;
using Terraria.ID;

namespace DBT.Items.Guardian.Accessories.Chakras
{
    public class ThroatChakra : GuardianItem
    {
        public ThroatChakra() : base("Throat Chakra", 
            "Grants the compelling voice buff to players in the same party." +
            "\nThis increases their Ki charge rate and Ki damage by 10%," +
            "\nMovement speed by 20% and increases all healing by the player by 10%." +
            "\n'Your voice innervates the energy of those around you.'",
            18, 30, value: Item.buyPrice(gold: 1, silver: 49), rarity: ItemRarityID.LightRed)
        {
        }

        public override void SetDefaults()
        {
            item.value = 74500;
            item.rare = ItemRarityID.LightRed;
            base.SetDefaults();
            item.accessory = true;
        }
    }
}

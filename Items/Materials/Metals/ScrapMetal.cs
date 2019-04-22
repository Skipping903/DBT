using Terraria;
using Terraria.ID;

namespace DBT.Items.Materials.Metals
{
    public class ScrapMetal : DBTItem
    {
        public ScrapMetal() : base("Scrap Metal", 
            "Level 1 Craft Item" +
            "\nAn old piece of metal, seems like something a junk merchant would sell.", 
            34, 34, value: Item.buyPrice(silver: 30), rarity: ItemRarityID.Green)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.maxStack = 9999;
        }
    }
}
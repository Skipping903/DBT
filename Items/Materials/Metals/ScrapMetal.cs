using Terraria.ID;

namespace DBT.Items.Materials.Metals
{
    public class ScrapMetal : DBTItem
    {
        public ScrapMetal() : base("Scrap Metal", "Level 1 Craft Item\nAn old piece of metal, seems like something a junk merchant would sell.", 1 * Constants.SILVER_VALUE_MULTIPLIER, 0, ItemRarityID.Green)
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.width = 34;
            item.height = 34;
            item.maxStack = 9999;
        }
    }
}
using Terraria.ID;

namespace DBT.Items.Materials.Metals
{
    public class ScrapMetal : DBTItem
    {
        public ScrapMetal() : base("Scrap Metal", "Level 1 Craft Item\nAn old piece of metal, seems like something a junk merchant would sell.")
        {
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.width = 34;
            item.height = 34;
            item.maxStack = 9999;
            item.value = 500;
            item.rare = ItemRarityID.Green;
        }
    }
}
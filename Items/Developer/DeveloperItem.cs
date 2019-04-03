using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items.Developer
{
    public abstract class DeveloperItem : ModItem
    {
        protected DeveloperItem(int width, int height)
        {
            Width = width;
            Height = height;
        }
        
        public override void SetDefaults()
        {
            item.consumable = false;
            item.maxStack = 1;
            item.UseSound = SoundID.Item3;
            item.useStyle = 2;
            item.useTurn = true;
            item.useAnimation = 17;
            item.useTime = 17;
            item.value = 0;
            item.expert = true;
            item.potion = false;
        }

        public int Width { get; }

        public int Height { get; }
    }
}

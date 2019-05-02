using Terraria;
using Terraria.DataStructures;
using Terraria.ID;

namespace DBT.Items.Materials
{
    public sealed class SoulofEntity : DBTMaterial
    {
        private static int _width, _height;

        public SoulofEntity() : base("Soul of Entity", "'The soul of a reanimated foe.'",
            0, 0, Item.buyPrice(silver: 1, copper: 4), ItemRarityID.Pink)
        {
        }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(5, 4));
            ItemID.Sets.AnimatesAsSoul[item.type] = true;
            ItemID.Sets.ItemIconPulse[item.type] = true;
            ItemID.Sets.ItemNoGravity[item.type] = true;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            if (_width == 0 || _height == 0)
            {
                Item refItem = new Item();
                refItem.SetDefaults(ItemID.SoulofSight);
                _width = refItem.width;
                _height = refItem.height;
            }

            item.width = _width;
            item.height = _height;
        }
    }
}
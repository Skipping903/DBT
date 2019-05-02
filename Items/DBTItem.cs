using DBT.Commons.Items;
using Terraria.ID;
using Terraria.ModLoader;

namespace DBT.Items
{
    public abstract class DBTItem : ModItem, IHasValue, IHasDefense, IHasRarity
    {
        private readonly string _displayName, _tooltip;

        protected DBTItem(string displayName, string tooltip, int width, int height, int value = 0, int defense = 0, int rarity = ItemRarityID.White)
        {
            _displayName = displayName;
            _tooltip = tooltip;

            Value = value;
            Defense = defense;
            Rarity = rarity;
        }

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            DisplayName.SetDefault(_displayName);

            string tooltip = _tooltip;

            IIsPatreonItem patreonItem = this as IIsPatreonItem;
            if (patreonItem != null)
                tooltip += "\n" + patreonItem.PatreonDonor;

            Tooltip.SetDefault(tooltip);
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.value = Value;
            item.defense = Defense;
            item.rare = Rarity;
        }

        public int Value { get; }
        public int Defense { get; }
        public int Rarity { get; }
    }
}

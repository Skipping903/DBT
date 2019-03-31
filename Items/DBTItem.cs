using Terraria.ModLoader;

namespace DBTMod.Items
{
    public abstract class DBTItem : ModItem
    {
        private readonly string _displayName, _tooltip;

        protected DBTItem(string displayName, string tooltip)
        {
            _displayName = displayName;
            _tooltip = tooltip;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault(_displayName);
            Tooltip.SetDefault(_tooltip);
        }
    }
}

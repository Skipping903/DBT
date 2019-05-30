using Terraria.ModLoader;

namespace DBT.Buffs
{
    public abstract class DBTBuff : ModBuff
    {
        private readonly string _displayName, _tooltip;

        protected DBTBuff(string displayName, string tooltip)
        {
            _displayName = displayName;
            _tooltip = tooltip;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            DisplayName.SetDefault(_displayName);
            Description.SetDefault(_tooltip);
        }
    }
}

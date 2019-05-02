using Terraria.Audio;

namespace DBT.Items.Tools
{
    public abstract class DBTTool : DBTItem
    {
        private readonly int _useStyle, _useAnimation, _useTime;
        private readonly LegacySoundStyle _useSound;

        protected DBTTool(string displayName, int width, int height, int value, int rarity,
            int useStyle, LegacySoundStyle useSound, int useAnimation, int useTime) : base(displayName, "", width, height, value, 0, rarity)
        {
            _useStyle = useStyle;
            _useSound = useSound;
            _useAnimation = useAnimation;
            _useTime = useTime;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.autoReuse = true;
            item.melee = true;

            item.UseSound = _useSound;
            item.useStyle = _useStyle;
            item.useAnimation = _useAnimation;
            item.useTime = _useTime;
        }
    }
}
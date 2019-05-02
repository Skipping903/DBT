using Terraria.Audio;

namespace DBT.Items.Consumables
{
    public abstract class DBTConsumable : DBTItem
    {
        private readonly int _useStyle, _useAnimation, _useTime;
        private readonly bool _useTurn;
        private readonly LegacySoundStyle _useSound;

        protected DBTConsumable(string displayName, string tooltip, int width, int height, int value, int rarity,
            int useStyle, bool useTurn, LegacySoundStyle useSound, int useAnimation, int useTime) : base(displayName, tooltip, width, height, value, 0, rarity)
        {
            _useStyle = useStyle;
            _useTurn = useTurn;
            _useSound = useSound;
            _useAnimation = useAnimation;
            _useTime = useTime;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.consumable = true;
            item.potion = false;

            item.UseSound = _useSound;
            item.useStyle = _useStyle;
            item.useTurn = _useTurn;
            item.useAnimation = _useAnimation;
            item.useTime = _useTime;
        }
    }
}
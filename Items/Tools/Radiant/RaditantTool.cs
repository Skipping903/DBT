using Terraria.ID;

namespace DBT.Items.Tools.Radiant
{
    public abstract class RaditantTool : DBTTool
    {
        private readonly int _pickPower, _axePower, _hammerPower, _tileBoost, _damage;
        private readonly float _knockBack;

        protected RaditantTool(string displayName, int width, int height, int value, int useAnimation, int useTime, int pickPower, int axePower, int hammerPower, int tileBoost, int damage, float knockBack) : base(displayName, width, height, value, ItemRarityID.Red, ItemUseStyleID.SwingThrow, SoundID.Item1, useAnimation, useTime)
        {
            _pickPower = pickPower;
            _axePower = axePower;
            _hammerPower = hammerPower;
            _tileBoost = tileBoost;

            _damage = damage;
            _knockBack = knockBack;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.pick = _pickPower;
            item.axe = _axePower;
            item.hammer = _hammerPower;

            item.damage = _damage;
            item.tileBoost = _tileBoost;
        }
    }
}
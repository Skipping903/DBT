using DBT.Commons;
using DBT.Commons.Items;

namespace DBT.Items.KiStones
{
    public abstract class KiStone : DBTItem, IHasValue, IHasRarity
    {
        protected KiStone(string displayName, string tooltip, int rarity, KiStoneDefinition definition) : base(displayName, tooltip)
        {
            Value = EmptyKiStone.VALUE;
            Rarity = rarity;

            Definition = definition;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            item.width = 24;
            item.height = 24;
            item.value = Value;
            item.rare = Rarity;
            item.maxStack = 99;
        }

        public int Value { get; }
        public int Rarity { get; }

        public KiStoneDefinition Definition { get; }
        public float StoredKi => Definition.RequiredKi;
    }
}
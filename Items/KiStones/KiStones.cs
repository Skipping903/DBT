using Terraria.ID;

namespace DBT.Items.KiStones
{
    public sealed class KiStoneT1 : KiStone
    {
        public KiStoneT1() : base("Weak Ki Stone", "This stone barely has any Ki infused into it", ItemRarityID.Blue, KiStoneDefinitionManager.Instance.KiStoneT1)
        {
        }
    }

    public sealed class KiStoneT2 : KiStone
    {
        public KiStoneT2() : base("Temperate Ki Stone", "This stone has some Ki infused into it", ItemRarityID.Green, KiStoneDefinitionManager.Instance.KiStoneT2)
        {
        }
    }

    public sealed class KiStoneT3 : KiStone
    {
        public KiStoneT3() : base("Awakened Ki Stone", "This stone has a respectable amount of Ki infused into it", ItemRarityID.Orange, KiStoneDefinitionManager.Instance.KiStoneT3)
        {
        }
    }

    public sealed class KiStoneT4 : KiStone
    {
        public KiStoneT4() : base("Mystical Ki Stone", "This stone has an impressive amount of Ki infused into it", ItemRarityID.LightRed, KiStoneDefinitionManager.Instance.KiStoneT4)
        {
        }
    }

    public sealed class KiStoneT5 : KiStone
    {
        public KiStoneT5() : base("Overflowing Ki Stone", "This stone is overflowing with Ki", ItemRarityID.Pink, KiStoneDefinitionManager.Instance.KiStoneT5)
        {
        }
    }

    public sealed class KiStoneT6 : KiStone
    {
        public KiStoneT6() : base("Transcendant Ki Stone", "The amount of Ki infused into this stone transcends understanding", ItemRarityID.LightPurple, KiStoneDefinitionManager.Instance.KiStoneT6)
        {
        }
    }
}
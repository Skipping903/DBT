using DBTR.HairStyles.Goku;
using DBTR.HairStyles.Kale;
using DBTR.HairStyles.NoChoice;
using DBTR.Managers;

namespace DBTR.HairStyles
{
    public sealed class HairStyleManager : Manager<HairStyle>
    {
        private static HairStyleManager _instance;

        internal override void DefaultInitialize()
        {
            NoChoice = Add(new NoChoiceHairStyle()) as NoChoiceHairStyle;

            Goku = Add(new GokuHairStyle()) as GokuHairStyle;
            Kale = Add(new KaleHairStyle()) as KaleHairStyle;
        }

        public NoChoiceHairStyle NoChoice { get; private set; }

        public GokuHairStyle Goku { get; private set; }

        public KaleHairStyle Kale { get; private set; }


        public static HairStyleManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new HairStyleManager();

                if (!_instance.Initialized)
                    _instance.DefaultInitialize();

                return _instance;
            }
        }
    }
}

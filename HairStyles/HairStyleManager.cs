using DBT.HairStyles.Gine;
using DBT.HairStyles.Gogeta;
using DBT.HairStyles.Goku;
using DBT.HairStyles.Kale;
using DBT.HairStyles.Nappa;
using DBT.HairStyles.NoChoice;
using DBT.HairStyles.Vegeta;
using DBT.HairStyles.Webmilio;
using DBT.Managers;

namespace DBT.HairStyles
{
    public sealed class HairStyleManager : Manager<HairStyle>
    {
        private static HairStyleManager _instance;

        internal override void DefaultInitialize()
        {
            NoChoice = Add(new NoChoiceHairStyle()) as NoChoiceHairStyle;

            Gine = Add(new GineHairStyle()) as GineHairStyle;
            Gogeta = Add(new GogetaHairStyle()) as GogetaHairStyle;
            Goku = Add(new GokuHairStyle()) as GokuHairStyle;
            Kale = Add(new KaleHairStyle()) as KaleHairStyle;
            Nappa = Add(new NappaHairStyle()) as NappaHairStyle;
            Vegeta = Add(new VegetaHairStyle()) as VegetaHairStyle;
            Webmilio = Add(new WebmilioHairStyle()) as WebmilioHairStyle;
        }

        public NoChoiceHairStyle NoChoice { get; private set; }

        public GineHairStyle Gine { get; private set; }
        public GogetaHairStyle Gogeta { get; private set; }
        public GokuHairStyle Goku { get; private set; }
        public KaleHairStyle Kale { get; private set; }
        public NappaHairStyle Nappa { get; private set; }
        public VegetaHairStyle Vegeta { get; private set; }
        public WebmilioHairStyle Webmilio { get; private set; }


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

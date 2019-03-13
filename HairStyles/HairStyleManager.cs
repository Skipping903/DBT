using DBTR.Managers;

namespace DBTR.HairStyles
{
    public sealed class HairStyleManager : Manager<HairStyle>
    {
        private static HairStyleManager _instance;

        internal override void DefaultInitialize()
        {
            
        }

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

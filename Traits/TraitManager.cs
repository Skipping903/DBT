using DBTR.Managers;

namespace DBTR.Traits
{
    public sealed class TraitManager : Manager<Trait>
    {
        private static TraitManager _instance;

        internal override void DefaultInitialize()
        {


            base.DefaultInitialize();
        }

        public Trait Prodigy { get; private set; }

        public Trait Peaceful { get; private set; }

        public Trait Legendary { get; private set; }

        public static TraitManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TraitManager();

                if (!_instance.Initialized)
                    _instance.DefaultInitialize();

                return _instance;
            }
        }
    }
}

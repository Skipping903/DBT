using DBT.Managers;

namespace DBT.Traits
{
    public sealed class TraitManager : Manager<TraitDefinition>
    {
        private static TraitManager _instance;

        internal override void DefaultInitialize()
        {


            base.DefaultInitialize();
        }

        public TraitDefinition Prodigy { get; private set; }

        public TraitDefinition Peaceful { get; private set; }

        public TraitDefinition Legendary { get; private set; }

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

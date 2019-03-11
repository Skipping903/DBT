using DBTRMod.Managers;
using DBTRMod.Transformations.SSJs.SSJ1;

namespace DBTRMod.Transformations
{
    public sealed class TransformationDefinitionManager : Manager<TransformationDefinition>
    {
        private static TransformationDefinitionManager _instance;

        internal override void DefaultInitialize()
        {
            SSJ1Definition = new SSJ1Transformation();

            base.DefaultInitialize();
        }

        public SSJ1Transformation SSJ1Definition { get; private set; }

        public static TransformationDefinitionManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TransformationDefinitionManager();

                if (!_instance.Initialized)
                    _instance.DefaultInitialize();

                return _instance;
            }
        }
    }
}

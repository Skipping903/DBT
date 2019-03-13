using DBTR.Managers;
using DBTR.Transformations.SSJs.SSJ1;
using DBTR.Transformations.SSJs.SSJG;

namespace DBTR.Transformations
{
    public sealed class TransformationDefinitionManager : Manager<TransformationDefinition>
    {
        private static TransformationDefinitionManager _instance;

        internal override void DefaultInitialize()
        {
            SSJ1 = Add(new SSJ1Transformation()) as SSJ1Transformation;

            SSJG = Add(new SSJGTransformation()) as SSJGTransformation;

            base.DefaultInitialize();
        }


        public SSJ1Transformation SSJ1 { get; private set; }


        public SSJGTransformation SSJG { get; private set; }


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

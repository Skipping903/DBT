using DBTR.Managers;
using DBTR.Transformations.SSJs.SSJ1s.SSJ1;
using DBTR.Transformations.SSJs.SSJ1s.ASSJ1;
using DBTR.Transformations.SSJs.SSJ1s.USSJ1;
using DBTR.Transformations.SSJs.SSJ2;
using DBTR.Transformations.SSJs.SSJ3;
using DBTR.Transformations.SSJs.SSJ4s.SSJ4;
using DBTR.Transformations.SSJGs.SSJG;

namespace DBTR.Transformations
{
    public sealed class TransformationDefinitionManager : Manager<TransformationDefinition>
    {
        private static TransformationDefinitionManager _instance;

        internal override void DefaultInitialize()
        {
            SSJ1 = Add(new SSJ1Transformation()) as SSJ1Transformation;
            ASSJ1 = Add(new ASSJ1Transformation(SSJ1)) as ASSJ1Transformation;
            USSJ1 = Add(new USSJ1Transformation(ASSJ1)) as USSJ1Transformation;

            SSJ2 = Add(new SSJ2Transformation(SSJ1)) as SSJ2Transformation;
            SSJ3 = Add(new SSJ3Transformation(SSJ2)) as SSJ3Transformation;

            SSJ4 = Add(new SSJ4Transformation(SSJ3)) as SSJ4Transformation;

            SSJG = Add(new SSJGTransformation()) as SSJGTransformation;

            base.DefaultInitialize();
        }


        public SSJ1Transformation SSJ1 { get; private set; }
        public ASSJ1Transformation ASSJ1 { get; private set; }
        public USSJ1Transformation USSJ1 { get; private set; }

        public SSJ2Transformation SSJ2 { get; private set; }
        public SSJ3Transformation SSJ3 { get; private set; }

        public SSJ4Transformation SSJ4 { get; private set; }

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

using DBT.Dynamicity;
using DBT.Managers;
using DBT.Transformations.Developers.Webmilio;
using DBT.Transformations.SSJGs.SSJBs.SSJB;
using DBT.Transformations.SSJGs.SSJBs.SSJBE;
using DBT.Transformations.SSJGs.SSJG;
using DBT.Transformations.SSJGs.SSJR;
using DBT.Transformations.SSJs.SSJ1s.ASSJ1;
using DBT.Transformations.SSJs.SSJ1s.SSJ1;
using DBT.Transformations.SSJs.SSJ1s.USSJ1;
using DBT.Transformations.SSJs.SSJ2;
using DBT.Transformations.SSJs.SSJ3;
using DBT.Transformations.SSJs.SSJ4s.SSJ4;

namespace DBT.Transformations
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

            SSJG = Add(new SSJGTransformation(SSJ3)) as SSJGTransformation;
            SSJB = Add(new SSJBTransformation(SSJG)) as SSJBTransformation;
            SSJR = Add(new SSJRTransformation(SSJG)) as SSJRTransformation;
            SSJBE = Add(new SSJBETransformation(SSJB)) as SSJBETransformation;

            //SoulStealer = Add(new SoulStealerTransformation()) as SoulStealerTransformation;
            Tree = new Tree<TransformationDefinition>(byIndex);

            base.DefaultInitialize();
        }


        public SSJ1Transformation SSJ1 { get; private set; }
        public ASSJ1Transformation ASSJ1 { get; private set; }
        public USSJ1Transformation USSJ1 { get; private set; }

        public SSJ2Transformation SSJ2 { get; private set; }
        public SSJ3Transformation SSJ3 { get; private set; }

        public SSJ4Transformation SSJ4 { get; private set; }

        public SSJGTransformation SSJG { get; private set; }
        public SSJBTransformation SSJB { get; private set; }
        public SSJRTransformation SSJR { get; private set; }
        public SSJBETransformation SSJBE { get; private set; }

        public SoulStealerTransformation SoulStealer { get; private set; }

        public Tree<TransformationDefinition> Tree { get; private set; }

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

using DBT.Managers;
using DBT.Skills.DebugKiBlast;
using DBT.Skills.KiBlast;

namespace DBT.Skills
{
    public sealed class SkillDefinitionManager : SingletonManager<SkillDefinitionManager, SkillDefinition>
    {
        private static SkillDefinitionManager _instance;

        internal override void DefaultInitialize()
        {
            DebugKiBlast = Add(new DebugKiBlastDefinition()) as DebugKiBlastDefinition;
            KiBlast = Add(new KiBlastDefinition()) as KiBlastDefinition;

            base.DefaultInitialize();
        }

        public DebugKiBlastDefinition DebugKiBlast { get; private set; }
        public KiBlastDefinition KiBlast { get; private set; }

        public static SkillDefinitionManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SkillDefinitionManager();

                if (!_instance.Initialized)
                    _instance.DefaultInitialize();

                return _instance;
            }
        }
    }
}
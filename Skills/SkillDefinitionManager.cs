using DBT.Managers;
using DBT.Skills.KiBlast.DebugKiBlast;

namespace DBT.Skills
{
    public sealed class SkillDefinitionManager : Manager<SkillDefinition>
    {
        private static SkillDefinitionManager _instance;

        internal override void DefaultInitialize()
        {
            DebugKiBlast = Add(new DebugKiBlastDefinition()) as DebugKiBlastDefinition;

            base.DefaultInitialize();
        }

        public DebugKiBlastDefinition DebugKiBlast { get; private set; }

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
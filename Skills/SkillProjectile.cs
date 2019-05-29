using System;
using DBT.Projectiles;

namespace DBT.Skills
{
    public abstract class SkillProjectile : DBTProjectile
    {
        protected SkillProjectile(SkillDefinition definition)
        {
            Definition = definition;
        }

        public SkillDefinition Definition { get; }
    }
}
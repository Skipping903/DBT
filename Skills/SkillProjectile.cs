using System;
using DBT.Projectiles;

namespace DBT.Skills
{
    public abstract class SkillProjectile : DBTProjectile
    {
        protected SkillProjectile(SkillDefinition definition, int damage) : base(damage)
        {
            Definition = definition;
        }

        public SkillDefinition Definition { get; }
    }
}
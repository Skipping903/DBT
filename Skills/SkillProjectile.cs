using System;
using DBT.Projectiles;

namespace DBT.Skills
{
    public abstract class SkillProjectile : KiProjectile
    {
        protected SkillProjectile(SkillDefinition definition) : base(0)
        {
            Definition = definition;
        }

        public override void SetDefaults()
        {
            base.SetDefaults();

            // TODO Add actual charges.
            projectile.damage = (int) Definition.Characteristics.GetDamage(Owner, 1);
        }

        public SkillDefinition Definition { get; }
    }
}
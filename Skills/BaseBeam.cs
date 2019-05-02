using DBT.Commons.Projectiles;
using DBT.Projectiles;
using DBT.Skills.Beams;

namespace DBT.Skills
{
    public abstract class BaseBeam : DBTProjectile
    {
        protected BaseBeam(BeamOffsets sizeAndOffsets)
        {
            SizeAndOffsets = sizeAndOffsets;
        }

        public BeamOffsets SizeAndOffsets { get; }
    }
}
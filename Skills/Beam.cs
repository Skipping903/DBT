using DBT.Commons.Projectiles;
using DBT.Projectiles;
using DBT.Skills.Beams;

namespace DBT.Skills
{
    public abstract class Beam : DBTProjectile
    {
        protected Beam(BeamOffsets sizeAndOffsets)
        {
            SizeAndOffsets = sizeAndOffsets;
        }

        public BeamOffsets SizeAndOffsets { get; }
    }
}
using DBTR.Auras;
using DBTR.HairStyles;
using Microsoft.Xna.Framework;

namespace DBTR.Transformations
{
    public abstract class TransformationAppearance
    {
        protected TransformationAppearance(AuraAppearance aura, HairAppearance hair)
        {
            Aura = aura;
            Hair = hair;
        }

        public AuraAppearance Aura { get; }

        public HairAppearance Hair { get; }
    }

    public class HairAppearance
    {
        public HairAppearance(Color? color)
        {
            Color = color;
        }

        public Color? Color { get; }
    }
}

using Microsoft.Xna.Framework;

namespace DBT.Skills.Beams
{
    public struct BeamOffsets
    {
        public BeamOffsets(Point tailOrigin, Vector2 tailSize, Point beamOrigin, Vector2 beamSize, Point headOrigin, Vector2 headSize)
        {
            TailOrigin = tailOrigin;
            TailSize = tailSize;

            BeamOrigin = beamOrigin;
            BeamSize = beamSize;

            HeadOrigin = headOrigin;
            HeadSize = headSize;
        }

        public Point TailOrigin { get; }
        public Vector2 TailSize { get; }

        public Point BeamOrigin { get; }
        public Vector2 BeamSize { get; }

        public Point HeadOrigin { get; }
        public Vector2 HeadSize { get; }
    }
}
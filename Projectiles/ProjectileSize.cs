using Microsoft.Xna.Framework;

namespace DBT.Projectiles
{
    public struct ProjectileSize
    {
        public readonly Point origin, size;

        public ProjectileSize(int xOrigin, int yOrigin, int xSize, int ySize)
        {
            origin = new Point(xOrigin, yOrigin);
            size = new Point(xSize, ySize);
        }

        public ProjectileSize(Point origin, Point size)
        {
            this.origin = origin;
            this.size = size;
        }
    }
}
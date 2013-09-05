using SFML.Graphics;

using Mappy.Textures;

namespace Mappy
{
    public static class CollisionHelper
    {
        public static IntRect CreateHitbox(Vector2D position)
        {
            int w = (int)TextureManager.TextureSize.X;
            int h = (int)TextureManager.TextureSize.Y;
            int x = (int)position.X;
            int y = (int)position.Y;

            IntRect hitbox = new IntRect(x, y, w, h);
            return hitbox;
        }

        public static Vector2D GetCenter(IntRect hitbox)
        {
            int x = hitbox.Left + hitbox.Width / 2;
            int y = hitbox.Top + hitbox.Height / 2;

            return new Vector2D(x, y);
        }
    }
}

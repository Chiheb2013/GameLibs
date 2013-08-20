using System;

namespace Mappy
{
    public static class CoordinateSystemConverter
    {
        public static Vector2D PixelsToWorld(Vector2D pixelCoords)
        {
            int x = (int)(pixelCoords.X / Texture.TextureManager.TextureSize.X);
            int y = (int)(pixelCoords.Y / Texture.TextureManager.TextureSize.Y);
            return new Vector2D(x, y);
        }

        public static Vector2D WorldToPixels(Vector2D worldCoords)
        {
            int x = (int)(worldCoords.X * Texture.TextureManager.TextureSize.X);
            int y = (int)(worldCoords.Y * Texture.TextureManager.TextureSize.Y);
            return new Vector2D(x, y);
        }

        ///--------------------------\
        //|     p(x,y) = p(i)         |
        //|     p(i) = wy + x         |
        //\--------------------------/
        public static int PlaneToLine(Vector2D worldCoords, int width)
        {
            int y = (int)worldCoords.Y;
            int x = (int)worldCoords.X;

            int line = width * y + x;
            
            return line;
        }

        ///--------------------------\
        //|     p(i) = p(x,y)         |
        //|     y = floor i / w       |
        //|     x = -wy + i           |
        //\--------------------------/
        public static Vector2D LineToPlane(int i, int width)
        {
            int y = i / width;
            int x = -width * y + i;

            return new Vector2D(x, y);
        }
    }
}

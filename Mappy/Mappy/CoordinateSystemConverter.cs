using System;

namespace Mappy
{
    public static class CoordinateSystemConverter
    {
        public static Vector2D PixelsToWorld(Vector2D pixelCoords)
        {
            int x = (int)(pixelCoords.X / Textures.TextureManager.TextureSize.X);
            int y = (int)(pixelCoords.Y / Textures.TextureManager.TextureSize.Y);
            return new Vector2D(x, y);
        }

        public static Vector2D WorldToPixels(Vector2D worldCoords)
        {
            int x = (int)(worldCoords.X * Textures.TextureManager.TextureSize.X);
            int y = (int)(worldCoords.Y * Textures.TextureManager.TextureSize.Y);
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

            if (x < 0 || x > width)
                return -1;        // If x is not in the grid, return
                                 // a negative value that won't be
                                // processed downward. ;-)
            
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

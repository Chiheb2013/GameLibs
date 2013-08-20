using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Mappy
{
    public static class BitmapHelper
    {
        /// <summary>
        /// Sets alpha component for each pixel of the image to 'alpha'
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="alpha"></param>
        /// <remarks>This is not optimized GDI+.NET, use only with small images or 
        /// execution time will be really big !</remarks>
        public static void SetAlphaForAll(Bitmap bmp, int alpha)
        {
            for (int x = 0; x < bmp.Width; x++)
                for (int y = 0; y < bmp.Height; y++)
                    SetAlphaComponent(bmp, x, y, alpha);
        }

        public static void SetAlphaComponent(Bitmap bmp, int x, int y, int alpha)
        {
            Color pixel = bmp.GetPixel(x, y);
            pixel = Color.FromArgb(alpha, pixel);
            bmp.SetPixel(x, y, pixel);
        }
    }
}

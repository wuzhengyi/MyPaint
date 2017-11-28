using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painting
{
    class ImageOperation
    {
        static public Bitmap SetBitmapSize(Bitmap src,int w,int h)
        {
            int tw = src.Width;
            int th = src.Height;
            double dw = (double)w / tw;
            double dh = (double)h / th;
            Bitmap map = new Bitmap(w, h);
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    map.SetPixel(i, j, src.GetPixel((int)((double)i/dw), (int)((double)j / dh)));
                }
            }       
            return map;
        }
    }
}

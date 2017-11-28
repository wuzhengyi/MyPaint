using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Painting
{
    partial class Form1
    {

        private void FillColor(Color c, int x1, int y1, int x2, int y2)
        {
            //在矩形内填充纯色
            Bitmap map = new Bitmap(pictureBox.Image);
            for (int i = x1; i < x2; i++)
            {
                for (int j = y1; j < y2; j++)
                {
                    map.SetPixel(i, j, color);
                }
            }
            pictureBox.Image = map;
        }

        private void FillPic(Bitmap pic, int x0, int y0, int x1, int y1)
        {
            Bitmap map = new Bitmap(pictureBox.Image);
            for (int i = x0; i < x1; i++)
            {
                for (int j = y0; j < y1; j++)
                {
                    if (i - x0 > 0 && i - x0 < map.Width && j - y0 > 0 && j - y0 < map.Height)
                        map.SetPixel(i, j, pic.GetPixel(i - x0, j - y0));
                }
            }
            pictureBox.Image = map;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Painting
{
    partial class Form1
    {
        public static void drawPixel(PictureBox pictureBox, int x, int y, Color cr)//画点
        {
            Brush br = new SolidBrush(cr);
            Graphics.FromImage(pictureBox.Image).FillRectangle(br, x, y, 1, 1);
            pictureBox.Invalidate();
            br.Dispose();
        }

        public static void DDADottedLine(PictureBox pictureBox, int x1, int y1, int x2, int y2)
        {
            double dx, dy, e, x, y;
            dx = x2 - x1;
            dy = y2 - y1;
            e = (Math.Abs(dx) > Math.Abs(dy)) ? Math.Abs(dx) : Math.Abs(dy);
            dx /= e; dy /= e;
            x = x1;
            y = y1;
            for (int i = 1; i <= e; i++)
            {
                if (i % 10 < 5)
                    drawPixel(pictureBox, (int)(x + 0.5), (int)(y + 0.5), Color.Blue);
                    x += dx;
                y += dy;
            }
        }

        /*private bool PointInRectangle(int x, int y)
        {
            if (x >= x1 && x <= x2 && y >= y1 && y <= y2)
                return true;
            else
                return false;
        }*/

    }
}

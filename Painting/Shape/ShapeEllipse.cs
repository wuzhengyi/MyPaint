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
    class Ellipse : Shape
    {
        public void InitEllipse(PictureBox pictureBox, Color color, int x0, int y0, int x1, int y1)
        {
            type = ShapeType.Ellipse;
            SetPictureBox(pictureBox);
            SetColor(color);
            SetLocation(x0, y0, x1, y1) ;
            InitShape();
        }
        public override void Draw()
        {
            if (!visible)
            {
                return;
            }
            int x, y, p, t1, t2;
            int rx2 = x1 * x1;
            int ry2 = y1 * y1;
            x = 0; y = y1;
            p = ry2 - rx2 * y1 + rx2 / 4;
            t1 = 0;
            t2 = 2 * rx2 * y;
            for (; ry2 * x < rx2 * y; x++)
            {
                Form1.drawPixel(pictureBox, x + x0, y + y0, color);
                Form1.drawPixel(pictureBox, x + x0, -y + y0, color);
                Form1.drawPixel(pictureBox, -x + x0, y + y0, color);
                Form1.drawPixel(pictureBox, -x + x0, -y + y0, color);

                t1 += 2 * ry2;
                if (p >= 0)
                {
                    y--;
                    t2 -= 2 * rx2;
                    p += t1 - t2 + ry2;
                }
                else
                {
                    p += t1 + ry2;
                }
            }
            //我靠ppt这里面给错了？？？？我找了半天问题
            p = ry2 * (x * x + x) + ry2 / 4 + rx2 * (y - 1) * (y - 1) - rx2 * ry2;
            for (; y >= 0; y--)
            {
                Form1.drawPixel(pictureBox, x + x0, y + y0, color);
                Form1.drawPixel(pictureBox, x + x0, -y + y0, color);
                Form1.drawPixel(pictureBox, -x + x0, y + y0, color);
                Form1.drawPixel(pictureBox, -x + x0, -y + y0, color);

                t2 -= 2 * rx2;
                if (p >= 0)
                {
                    p += rx2 - t2;
                }
                else
                {
                    x++;
                    t1 += 2 * ry2;
                    p += rx2 + t1 - t2;
                }
            }
        }

        public override bool PointOnIt(int x, int y)
        {
            throw new NotImplementedException();
        }
    }

}

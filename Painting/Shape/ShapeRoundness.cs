using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Painting.Shapes
{
    class Roundness : Shape
    {
        int r;
        int cx, cy;
        public void InitRoundness(PictureBox pictureBox, Color color,int x0, int y0,int x1,int y1)
        {
            type = ShapeType.Roundness;
            SetPictureBox(pictureBox);
            SetColor(color);
            SetLocation(x0, y0, x1, y1);
            Point a = new Point(x0, y0);
            Point b = new Point(x1, y1);
            r = (int)Form1.DistanceOfPoint(a, b) / 2;
            cx = (x0 + x1) / 2;
            cy = (y0 + y1) / 2;
            InitShape();
        }
        public override void Draw()
        {
            if (!visible)
            {
                return;
            }
            int x, y, p;
            x = 0; y = r;
            p = 3 - 2 * r;
            for (; x <= y; x++)
            {
                DrawPixel(x, cx, y, cy);

                if (p >= 0)
                {
                    p += 4 * (x - y) + 10;
                    y--;
                }
                else
                {
                    p += 4 * x + 6;
                }
            }

            if (selected)
            {
                Rect rec = new Rect();
                rec.InitRectangle(pictureBox, color, cx - r, cy - r, cx + r, cy + r);
                rec.SelectShape();
                rec.Draw();
            }

        }

        public void DrawPixel(int x,int cx,int y,int cy)
        {
                Form1.drawPixel(pictureBox, x + cx, y + cy, color);
                Form1.drawPixel(pictureBox, x + cx, -y + cy, color);
                Form1.drawPixel(pictureBox, y + cx, x + cy, color);
                Form1.drawPixel(pictureBox, y + cx, -x + cy, color);
                Form1.drawPixel(pictureBox, -x + cx, y + cy, color);
                Form1.drawPixel(pictureBox, -x + cx, -y + cy, color);
                Form1.drawPixel(pictureBox, -y + cx, x + cy, color);
                Form1.drawPixel(pictureBox, -y + cx, -x + cy, color);
            
        }

        public override bool PointOnIt(int x, int y)
        {
            Point a = new Point(x, y);
            Point b = new Point(cx, cy);
            double d = Form1.DistanceOfPoint(a, b);
            if (Math.Abs(d - r) < 1.5)
                return true;
            else
                return false;
        }
    }
}

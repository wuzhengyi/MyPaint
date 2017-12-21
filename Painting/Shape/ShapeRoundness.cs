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
            InitData();
            InitShape();
        }

        private void InitData()
        {
            Point a = new Point(x0, y0);
            Point b = new Point(x1, y1);
            r = (int)Form1.DistanceOfPoint(a, b) / 2;
           // r = Math.Abs(x0 - x1) / 2;
            cx = (x0 + x1) / 2;
            cy = (y0 + y1) / 2;
        }

        public override void Draw()
        {
            InitData();
            if (!visible)
            {
                return;
            }
            int x, y, p;
            x = 0; y = r;
            p = 3 - 2 * r;
            for (; x <= y; x++)
            {
                if(!selected)
                    DrawPixel(x, cx, y, cy);
                else if (x % 10 < 5)
                {
                    DrawPixel(x, cx, y, cy, Color.Blue);
                }
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

        public void DrawPixel(int x, int cx, int y, int cy, Color color)
        {

            Form1.drawPixel(pictureBox, x + cx + dx, y + cy + dy, color);
            Form1.drawPixel(pictureBox, x + cx + dx, -y + cy + dy, color);
            Form1.drawPixel(pictureBox, y + cx + dx, x + cy + dy, color);
            Form1.drawPixel(pictureBox, y + cx + dx, -x + cy + dy, color);
            Form1.drawPixel(pictureBox, -x + cx + dx, y + cy + dy, color);
            Form1.drawPixel(pictureBox, -x + cx + dx, -y + cy + dy, color);
            Form1.drawPixel(pictureBox, -y + cx + dx, x + cy + dy, color);
            Form1.drawPixel(pictureBox, -y + cx + dx, -x + cy + dy, color);

        }

        public override bool PointOnEdge(int x, int y)
        {
            Point a = new Point(x, y);
            Point b = new Point(cx, cy);
            double d = Form1.DistanceOfPoint(a, b);
            if (Math.Abs(d - r) < 1.5)
                return true;
            else
                return false;
        }

        //public override Point NWPoint()
        //{
        //    return new Point(x0, y0);
        //}

        //public override Point SEPoint()
        //{
        //    return new Point(x1, y1);
        //}

        public override bool PointInIt(int x, int y)
        {
            Point a = new Point(x, y);
            Point b = new Point(cx, cy);
            double d = Form1.DistanceOfPoint(a, b);
            if (d < r - 2)
                return true;
            else
                return false;
        }
    }
}

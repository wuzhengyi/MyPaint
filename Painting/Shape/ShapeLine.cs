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
    class Line : Shape
    {
        public void InitLine(PictureBox pictureBox, Color color, int x0, int y0, int x1, int y1)
        {
            type = ShapeType.Line;
            SetPictureBox(pictureBox);
            SetColor(color);
            SetLocation(x0, y0, x1, y1);
            InitShape();
        }
        public override void Draw()
        {
            if (!visible)
            {
                return;
            }

            double dx, dy, e, x, y;
            dx = x1 - x0;
            dy = y1 - y0;
            e = (Math.Abs(dx) > Math.Abs(dy)) ? Math.Abs(dx) : Math.Abs(dy);
            dx /= e; dy /= e;
            x = x0;
            y = y0;
            for (int i = 1; i <= e; i++)
            {
                if(!selected)
                    Form1.drawPixel(pictureBox, (int)(x + 0.5), (int)(y + 0.5), color);
                else if(i % 10 < 5)
                    Form1.drawPixel(pictureBox, (int)(x + 0.5), (int)(y + 0.5), Color.Blue);
                
                x += dx;
                y += dy;
            }
        }

        public override bool PointOnIt(int x, int y)
        {
            Point a = new Point(x0, y0);
            Point b = new Point(x1, y1);
            Point c = new Point(x, y);
            double lac = Form1.DistanceOfPoint(a, c);
            double lbc = Form1.DistanceOfPoint(c, b);
            double lab = Form1.DistanceOfPoint(a, b);
            return (lac + lbc < lab + 0.05) ? true : false;
        }
    }
    
}

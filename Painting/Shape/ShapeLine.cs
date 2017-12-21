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

            double tx, ty, e, x, y;
            tx = x1 - x0;
            ty = y1 - y0;
            e = (Math.Abs(tx) > Math.Abs(ty)) ? Math.Abs(tx) : Math.Abs(ty);
            tx /= e; ty /= e;
            x = x0;
            y = y0;
            for (int i = 1; i <= e; i++)
            {
                if (!selected)
                {
                    Form1.drawPixel(pictureBox, (int)(x + 0.5 + dx), (int)(y + 0.5 + dy), color);
                    ButtonHide();
                }
                else
                {
                    if (i % 10 < 5)
                        Form1.drawPixel(pictureBox, (int)(x + 0.5 + dx), (int)(y + 0.5 + dy ), Color.Blue);
                }
                
                x += tx;
                y += ty;
            }
        }

        public override bool PointOnEdge(int x, int y)
        {
            Point a = new Point(x0, y0);
            Point b = new Point(x1, y1);
            Point c = new Point(x, y);
            double lac = Form1.DistanceOfPoint(a, c);
            double lbc = Form1.DistanceOfPoint(c, b);
            double lab = Form1.DistanceOfPoint(a, b);
            return (lac + lbc < lab + 0.25) ? true : false;
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
            return PointOnEdge(x, y);
        }
    }
    
}

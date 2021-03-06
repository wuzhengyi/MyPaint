﻿using System;
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
        private bool hide;
        public void InitLine(PictureBox pictureBox, Color color, int x0, int y0, int x1, int y1)
        {
            type = ShapeType.Line;
            SetPictureBox(pictureBox);
            SetColor(color);
            SetLocation(x0, y0, x1, y1);
            InitShape();
            hide = false;          
        }

       

        public override void Draw()
        {
            if (hide)
                return;
            int tempx = x1;
            int tempy = y1;
            Point temp = GetSpinPoint(new Point(x1, y1));
            x1 = temp.X;
            y1 = temp.Y;

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
            x1 = tempx;
            y1 = tempy;
        }

        public override bool PointOnEdge(int x, int y)
        {
            double cost = Math.Cos(-angle);
            double sint = Math.Sin(-angle);
            x = (int)((x - x0) * cost + (y - y0) * sint + x0);
            y = (int)(-sint * (x - x0) + cost * (y - y0) + y0);

            Point a = new Point(x0, y0);
            Point b = new Point(x1, y1);
            Point c = new Point(x, y);
            double lac = Form1.DistanceOfPoint(a, c);
            double lbc = Form1.DistanceOfPoint(c, b);
            double lab = Form1.DistanceOfPoint(a, b);
            return (lac + lbc < lab + 0.25) ? true : false;
        }

        public override bool PointInIt(int x, int y)
        {
            return PointOnEdge(x, y);
        }
        public override void FillColor(Color color)
        {
            this.color = color;
        }

        //求交点
        public Point Intersection(int X0, int Y0, int X1, int Y1)
        {
            try
            {
                int x = (X0 * Y1 * x0 - X1 * Y0 * x0 - X0 * Y1 * x1 + X1 * Y0 * x1 - X0 * x0 * y1 + X0 * x1 * y0 + X1 * x0 * y1 - X1 * x1 * y0) / (X0 * y0 - Y0 * x0 - X0 * y1 - X1 * y0 + Y0 * x1 + Y1 * x0 + X1 * y1 - Y1 * x1);
                int y = (X0 * Y1 * y0 - X1 * Y0 * y0 - X0 * Y1 * y1 + X1 * Y0 * y1 - Y0 * x0 * y1 + Y0 * x1 * y0 + Y1 * x0 * y1 - Y1 * x1 * y0) / (X0 * y0 - Y0 * x0 - X0 * y1 - X1 * y0 + Y0 * x1 + Y1 * x0 + X1 * y1 - Y1 * x1);
                return new Point(x, y);
            }
            catch (DivideByZeroException)
            {
                return new Point(1, 1);
            }



        }

        //用一条直线来裁剪原有直线
        public void LineClip(int X0, int Y0, int X1, int Y1)
        {
            if (hide)
                return;
            Point t = Intersection(X0, Y0, X1, Y1);
            if (PointInEdge(x0, y0, X0, Y0, X1, Y1) && !PointInEdge(x1, y1, X0, Y0, X1, Y1))
            {
                x1 = t.X;
                y1 = t.Y;
            }
            else if(!PointInEdge(x0, y0, X0, Y0, X1, Y1) && PointInEdge(x1, y1, X0, Y0, X1, Y1))
            {
                x0 = t.X;
                y0 = t.Y;
            }
            else if (!PointInEdge(x0, y0, X0, Y0, X1, Y1) && !PointInEdge(x1, y1, X0, Y0, X1, Y1))
            {
                x0 = y0 = x1 = y1 = 1;
                hide = true;
            }
            
        }
        //矩形框裁剪
        public override void Clip(int X0, int Y0, int X1, int Y1)
        {
            LineClip(X0, Y1, X0, Y0);
            LineClip(X0, Y0, X1, Y0);
            LineClip(X1, Y0, X1, Y1);
            LineClip(X1, Y1, X0, Y1);
        }
    }
    
}

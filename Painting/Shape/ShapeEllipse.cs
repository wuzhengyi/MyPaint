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
    class Ellipse : Shape
    {
        
        public void InitEllipse(PictureBox pictureBox, Color color, int x0, int y0, int x1, int y1)
        {           
            type = ShapeType.Ellipse;
            SetPictureBox(pictureBox);
            SetColor(color);
            SetLocation(x0, y0, x1, y1) ;
            InitData();
            InitShape();
        }

        private void InitData()
        {
            rx = Math.Abs(x0 - x1) / 2;
            ry = Math.Abs(y0 - y1) / 2;
            cx = (x0 + x1) / 2;
            cy = (y0 + y1) / 2;
        }
        private void DrawPoint(int x, int y)
        {
            Point temp = GetSpinPoint(new Point(x + cx + dx, y + cy + dy));
            Form1.drawPixel(pictureBox, temp.X, temp.Y, color);
            temp = GetSpinPoint(new Point(x + cx + dx, -y + cy + dy));
            Form1.drawPixel(pictureBox, temp.X, temp.Y, color);
            temp = GetSpinPoint(new Point(-x + cx + dx, y + cy + dy));
            Form1.drawPixel(pictureBox, temp.X, temp.Y, color);
            temp = GetSpinPoint(new Point(-x + cx + dx, -y + cy + dy));
            Form1.drawPixel(pictureBox, temp.X, temp.Y, color);
            
        }

        public override void Draw()
        {
            if (!visible)
            {
                return;
            }
            InitData();
            int x, y, p, t1, t2;
            int rx2 = rx * rx;
            int ry2 = ry * ry;
            x = 0; y = ry;
            p = ry2 - rx2 * ry + rx2 / 4;
            t1 = 0;
            t2 = 2 * rx2 * y;
            for (; ry2 * x < rx2 * y; x++)
            {
                DrawPoint(x, y);
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
                DrawPoint(x, y);

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

            if (selected)
            {
                Rect rec = new Rect();
                rec.InitRectangle(pictureBox, color, x0, y0, x1, y1);
                rec.SelectShape();
                rec.Draw();
                this.SelectShape();
            }
            else
                ButtonHide();
        }

        public override bool PointOnEdge(int x, int y)
        {
            double cost = Math.Cos(-angle);
            double sint = Math.Sin(-angle);
            x = (int)((x - x0) * cost + (y - y0) * sint + x0);
            y = (int)(-sint * (x - x0) + cost * (y - y0) + y0);

            Point p1 = new Point(x, y);
            if (rx > ry)
            {
                int c = (int)Math.Sqrt(rx * rx - ry * ry);
                Point p2 = new Point(c + cx, cy);
                Point p3 = new Point(-c + cx, cy);
                if (Math.Abs(Form1.DistanceOfPoint(p1, p2)+ Form1.DistanceOfPoint(p1, p3) - 2*rx) < 1.5)
                    return true;                
            }
            else
            {
                int c = (int)Math.Sqrt(ry * ry - rx * rx);
                Point p2 = new Point(cx, c + cy);
                Point p3 = new Point(cx, -c + cy);
                if (Math.Abs(Form1.DistanceOfPoint(p1, p2) + Form1.DistanceOfPoint(p1, p3) - 2*ry) < 1.5)
                    return true;
            }
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
            double cost = Math.Cos(-angle);
            double sint = Math.Sin(-angle);
            x = (int)((x - x0) * cost + (y - y0) * sint + x0);
            y = (int)(-sint * (x - x0) + cost * (y - y0) + y0);

            Point p1 = new Point(x, y);
            if (rx > ry)
            {
                int c = (int)Math.Sqrt(rx * rx - ry * ry);
                Point p2 = new Point(c + cx, cy);
                Point p3 = new Point(-c + cx, cy);
                if (Form1.DistanceOfPoint(p1, p2) + Form1.DistanceOfPoint(p1, p3) < 2 * rx - 3)
                    return true;
            }
            else
            {
                int c = (int)Math.Sqrt(ry * ry - rx * rx);
                Point p2 = new Point(cx, c + cy);
                Point p3 = new Point(cx, -c + cy);
                if (Form1.DistanceOfPoint(p1, p2) + Form1.DistanceOfPoint(p1, p3) < 2 * rx - 3)
                    return true;
            }
            return false;
        }

        public override void FillColor(Color color)
        {
            for(int x=x0;x<x1;x++)
                for(int y = y0; y < y1; y++)
                {
                    if()
                }
        }
    }

}

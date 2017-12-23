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

    class Rect : Shape
    {
        public void InitRectangle(PictureBox pictureBox, Color color, int x0, int y0, int x1, int y1)
        {
            type = ShapeType.Rectangle;
            SetPictureBox(pictureBox);
            SetColor(color);
            SetLocation(Math.Min(x0, x1), Math.Min(y0, y1), Math.Max(x0, x1), Math.Max(y0, y1));
            InitShape();
        }
        public override void Draw()
        {
            Point x1y0 = GetSpinPoint(new Point(x1, y0));
            Point x0y1 = GetSpinPoint(new Point(x0, y1));
            Point x1y1 = GetSpinPoint(new Point(x1, y1));

            Line l = new Line();            
            l.InitLine(pictureBox, color, x0, y0, x0y1.X, x0y1.Y);
            if (selected) l.SelectShape();
            l.Draw();

                        
            l.InitLine(pictureBox, color, x0, y0, x1y0.X, x1y0.Y);
            if (selected) l.SelectShape();
            l.Draw();

            l.InitLine(pictureBox, color, x1y0.X, x1y0.Y, x1y1.X, x1y1.Y);
            if (selected) l.SelectShape();
            l.Draw();

            l.InitLine(pictureBox, color, x0y1.X, x0y1.Y, x1y1.X, x1y1.Y);
            if (selected) l.SelectShape();
            l.Draw();

            if (fillcolor != null)
                FillColor(fillcolor);
        }

        public override bool PointOnEdge(int x, int y)
        {
            double cost = Math.Cos(-angle);
            double sint = Math.Sin(-angle);            
            x = (int)((x - x0) * cost + (y - y0) * sint + x0);
            y = (int)(-sint * (x - x0) + cost * (y - y0) + y0);
            
            Line a = new Line();
            a.InitLine(pictureBox, color, x0 + dx, y0 + dy, x0 + dx, y1 + dy);
            if (a.PointOnEdge(x, y))
                return true;
            a.InitLine(pictureBox, color, x0 + dx, y0 + dy, x1 + dx, y0 + dy);
            if (a.PointOnEdge(x, y))
                return true;
            a.InitLine(pictureBox, color, x1 + dx, y0 + dy, x1 + dx, y1 + dy);
            if (a.PointOnEdge(x, y))
                return true;
            a.InitLine(pictureBox, color, x0 + dx, y1 + dy, x1 + dx, y1 + dy);
            if (a.PointOnEdge(x, y))
                return true;
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

            if (x > Math.Min(x0, x1) && x < Math.Max(x0, x1) && y > Math.Min(y0, y1) && y < Math.Max(y0, y1))
                return true;
            return false;
        }

        public override void FillColor(Color color)
        {
            Point x1y0 = GetSpinPoint(new Point(x1, y0));
            Point x0y1 = GetSpinPoint(new Point(x0, y1));
            Point x1y1 = GetSpinPoint(new Point(x1, y1));

            double dm = (x1y1.Y - x1y0.Y) / (x1y1.X - x1y0.X);

            //for(int y;i)
            Line l = new Line();
            l.InitLine(pictureBox, color, x0, y0, x0y1.X, x0y1.Y);
            if (selected) l.SelectShape();
            l.Draw();


            l.InitLine(pictureBox, color, x0, y0, x1y0.X, x1y0.Y);
            if (selected) l.SelectShape();
            l.Draw();

            l.InitLine(pictureBox, color, x1y0.X, x1y0.Y, x1y1.X, x1y1.Y);
            if (selected) l.SelectShape();
            l.Draw();

            l.InitLine(pictureBox, color, x0y1.X, x0y1.Y, x1y1.X, x1y1.Y);
            if (selected) l.SelectShape();
            l.Draw();
            
        }
    }
}

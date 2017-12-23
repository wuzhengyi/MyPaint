using Painting.GUI;
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
    enum ShapeType {Dot, Line, Roundness, Ellipse, Rectangle, Pencil, FillCr, FillPic, polygon};

    abstract class Shape
    {
        protected ShapeType type;
        protected int x0, y0, x1, y1, dx, dy;
        protected int rx, ry, cx, cy, r;
        protected PictureBox pictureBox;
        public Color color;
        public bool visible;
        protected int replaceid;
        protected bool selected;
        protected double angle;
        protected static ControlSizeButton NWButton = new ControlSizeButton();
        protected static ControlSizeButton SEButton = new ControlSizeButton();

        public ShapeType GetShapeType()
        {
            return type;
        }

        public static void SetMouseEvent(MouseEventHandler up, MouseEventHandler down, MouseEventHandler move)
        {
            NWButton.SetMouseEvent(up, down, move);
            SEButton.SetMouseEvent(up, down, move);
        }

        public void SetPictureBox(PictureBox pictureBox)
        {
            this.pictureBox = pictureBox;
        }
        
        public void SetColor(Color color)
        {
            this.color = color;
        }

        public void SetLocation(int x0, int y0,int x1, int y1)
        {
            this.x0 = x0;
            this.y0 = y0;
            this.x1 = x1;
            this.y1 = y1;
        }

        public void InitShape()
        {
            selected = false;
            visible = true;
            replaceid = -1;
            dx = dy = 0;
        }

        public void SelectShape()
        {
            selected = true;
            ButtonShow();
        }

        public void unSelectShape()
        {
            selected = false;
            ButtonHide();
            UpdatePoint();
        }

        public void InitButton()
        {
            NWButton.InitButton(NWPoint(), pictureBox);
            SEButton.InitButton(SEPoint(), pictureBox);
        }

        public void UpdateLocation()
        {
            if(type == ShapeType.polygon)
            {
                ((Polygon)this).InitData();
            }
            x0 = NWButton.GetLocation().X;
            y0 = NWButton.GetLocation().Y;
            x1 = SEButton.GetLocation().X;
            y1 = SEButton.GetLocation().Y;
        }

        

        protected void ButtonShow()
        {
            NWButton.Show();
            SEButton.Show();
        }

        protected void ButtonHide()
        {
            NWButton.Hide();
            SEButton.Hide();
        }

        public void Setdxdy(int dx,int dy)
        {
            this.dx = dx;
            this.dy = dy;
            NWButton.SetLocation(new Point(x0 + dx, y0 + dy));
            SEButton.SetLocation(new Point(x1 + dx, y1 + dy));
        }

        public void SetAngle(double t)
        {
            angle += t;
        }

        protected Point GetSpinPoint(Point p)
        {
            double cost = Math.Cos(angle);
            double sint = Math.Sin(angle);
            return new Point((int)((p.X-x0) * cost + (p.Y-y0) * sint + x0), (int)(-sint * (p.X - x0) + cost * (p.Y - y0) + y0));
        }

        public void UpdatePoint()
        {
            x0 += dx;
            x1 += dx;
            y0 += dy;
            y1 += dy;
            dx = dy = 0;
        } 
        public abstract void Draw();

        public abstract bool PointOnEdge(int x,int y);

        public abstract bool PointInIt(int x, int y);

        public abstract void FillColor(Color color);

        //public abstract Point NWPoint();

        //public abstract Point SEPoint();
        public Point NWPoint()
        {
            return new Point(x0 + dx, y0 + dx);
        }

        public Point SEPoint()
        {
            return new Point(x1 + dx, y1 + dy);
        }

    }

    

    
}

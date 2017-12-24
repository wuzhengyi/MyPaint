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
    class Bsplines:Shape
    {
        public bool FinishDraw;
        private ArrayList ControlPoint = new ArrayList();
        public override void Clip(int X0, int Y0, int X1, int Y1)
        {
            throw new NotImplementedException();
        }

        public void InitBsplines(PictureBox pictureBox, Color color)
        {
            type = ShapeType.Bsplines;
            SetPictureBox(pictureBox);
            SetColor(color);
            InitShape();
            FinishDraw = false;
            x0 = y0 = 100000;
            x1 = y1 = -1;
        }

        public override void Draw()
        {
            if (!FinishDraw)
                for (int k = 0; k < ControlPoint.Count; k++)
                    Form1.drawPixel(pictureBox, ((Point)ControlPoint[k]).X, ((Point)ControlPoint[k]).Y, Color.Red);
            
            int n = 1000;
            double f1, f2, f3, f4;
            double deltaT = 1.0 / n;
            double T;
            
            for (int num = 0; num < ControlPoint.Count-3; num++)
            {
                for (int i = 0; i <= n; i++)
                {

                    T = i * deltaT;

                    f1 = (-T * T * T + 3 * T * T - 3 * T + 1) / 6.0;
                    f2 = (3 * T * T * T - 6 * T * T + 4) / 6.0;
                    f3 = (-3 * T * T * T + 3 * T * T + 3 * T + 1) / 6.0;
                    f4 = (T * T * T) / 6.0;                    
                    Form1.drawPixel(pictureBox, (int)(f1 * ((Point)ControlPoint[num]).X + f2 * ((Point)ControlPoint[num+1]).X + f3 * ((Point)ControlPoint[num+2]).X + f4 * ((Point)ControlPoint[num+3]).X),
                               (int)(f1 * ((Point)ControlPoint[num]).Y + f2 * ((Point)ControlPoint[num+1]).Y + f3 * ((Point)ControlPoint[num+2]).Y + f4 * ((Point)ControlPoint[num+3]).Y), color);
                }
            }
        }

        public int GetCount()
        {
            return ControlPoint.Count;
        }
        public void AddControlPoint(int x, int y)
        {
            ControlPoint.Add(new Point(x, y));
        }

        public void RemovePoint(int x, int y)
        {
            Point temp = new Point(x, y);
            ControlPoint.Remove(temp);
        }

        public void UpdateEdge(int x, int y)
        {
            if (x < x0) x0 = x;
            if (x > x1) x1 = x;
            if (y < y0) y0 = y;
            if (y > y1) y1 = y;
        }

        public override void FillColor(Color color)
        {
            throw new NotImplementedException();
        }

        public override bool PointInIt(int x, int y)
        {
            throw new NotImplementedException();
        }

        public override bool PointOnEdge(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}

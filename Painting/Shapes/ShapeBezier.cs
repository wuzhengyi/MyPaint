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
    class Bezier : Shape
    {
        public bool FinishDraw;
        private ArrayList ControlPoint = new ArrayList();
        public override void Clip(int X0, int Y0, int X1, int Y1)
        {
            throw new NotImplementedException();
        }

        public void InitBezier(PictureBox pictureBox, Color color)
        {
            type = ShapeType.Bezier;
            SetPictureBox(pictureBox);
            SetColor(color);            
            InitShape();
            FinishDraw = false;
            x0 = y0 = 100000;
            x1 = y1 = -1;
        }
        private int factorial(int n)
        {
            if (n == 1 || n == 0)
            {
                return 1;
            }
            else
            {
                return n * factorial(n - 1);
            }
        }
        private double C(int n, int i)
        {
            return ((double)factorial(n)) / ((factorial(i) * factorial(n - i)));
        }

        private void GetPoint(double t) //计算Bezier曲线上点的坐标
        {
            double x = 0, y = 0, Ber;
            int k;
            for (k = 0; k < ControlPoint.Count; k++)
            {
                Ber = C(ControlPoint.Count - 1, k) * Math.Pow(t, k) * Math.Pow(1 - t, ControlPoint.Count - 1 - k);
                x += ((Point)ControlPoint[k]).X * Ber;
                y += ((Point)ControlPoint[k]).Y * Ber;
            }
            Form1.drawPixel(pictureBox, (int)x, (int)y, color);
        }

        public override void Draw()
        {
            if(!FinishDraw)
                for (int k = 0; k < ControlPoint.Count; k++)
                    Form1.drawPixel(pictureBox, ((Point)ControlPoint[k]).X, ((Point)ControlPoint[k]).Y, Color.Red);            
            for (int i = 0; i <= 1000; i++)
                GetPoint((double)i / 1000);
        }

        public int GetCount()
        {
            return ControlPoint.Count;
        }
        public void AddControlPoint(int x,int y)
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

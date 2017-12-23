using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Painting.Shapes;

namespace Painting.Shapes
{
    class Polygon : Shape
    {
        public bool FinishDraw;
        private ArrayList PointList = new ArrayList();
        public void AddPoint(int x,int y)
        {
            Point temp = new Point(x, y);
            PointList.Add(temp);
            //UpdateEdge(x, y);
        }

        public int GetCount()
        {
            return PointList.Count;
        }

        public void RemovePoint(int x,int y)
        {
            Point temp = new Point(x, y);
            PointList.Remove(temp);
        }

        public void Updatedxdy()
        {
            for (int i = 0; i < PointList.Count; i++)
            {
                Point t = (Point)PointList[i];
                t.X += dx;
                t.Y += dy;
                PointList[i] = t;
            }
            x0 += dx;
            x1 += dx;
            y0 += dy;
            y1 += dy;
            dx = dy = 0;
        }

        public void InitData()
        {
            double rx = (double)(SEButton.GetLocation().X - NWButton.GetLocation().X) / (x1 - x0);
            double ry = (double)(SEButton.GetLocation().Y - NWButton.GetLocation().Y) / (y1 - y0);

            for (int i = 0; i < PointList.Count; i++)
            {
                Point t = (Point)PointList[i];
                t.X = (int)(NWButton.GetLocation().X + (t.X - x0) * rx);
                t.Y = (int)(NWButton.GetLocation().Y + (t.Y - y0) * ry);
                PointList[i] = t;
            }

        }

        public override void Draw()
        {
            if (PointList.Count < 2)
                return;
            int i;
            Line t;
            for (i = 1; i < PointList.Count; i++)
            {
                t = new Line();
                Point t1 = GetSpinPoint(new Point(((Point)PointList[i - 1]).X + dx, ((Point)PointList[i - 1]).Y + dy));
                Point t2 = GetSpinPoint(new Point(((Point)PointList[i]).X + dx, ((Point)PointList[i]).Y + dy));
                t.InitLine(pictureBox, color, t1.X, t1.Y, t2.X, t2.Y);
                t.Draw();
            }
            if (FinishDraw)
            {
                t = new Line();
                Point t1 = GetSpinPoint(new Point(((Point)PointList[0]).X + dx, ((Point)PointList[0]).Y + dy));
                Point t2 = GetSpinPoint(new Point(((Point)PointList[PointList.Count - 1]).X + dx,((Point)PointList[PointList.Count - 1]).Y + dy));
                
                t.InitLine(pictureBox, color, t1.X, t1.Y, t2.X, t2.Y);
                t.Draw();

                if (selected)
                {
                    Rect rec = new Rect();
                    rec.InitRectangle(pictureBox, color, x0 + dx, y0 + dy, x1 + dx, y1 + dy);
                    rec.SetAngle(angle);
                    rec.SelectShape();
                    rec.Draw();
                    this.SelectShape();
                }
                else
                    ButtonHide();

            }
            if (fillcolor != null)
                FillColor(fillcolor);
        }

        public void UpdateEdge(int x,int y)
        {
            if (x < x0) x0 = x;
            if (x > x1) x1 = x;
            if (y < y0) y0 = y;
            if (y > y1) y1 = y;
        }
        public override bool PointOnEdge(int x, int y)
        {
            double cost = Math.Cos(-angle);
            double sint = Math.Sin(-angle);
            x = (int)((x - x0) * cost + (y - y0) * sint + x0);
            y = (int)(-sint * (x - x0) + cost * (y - y0) + y0);

            if (PointList.Count < 2)
                return false;
            Line a = new Line();
            int i;
            for(i = 1; i < PointList.Count; i++)
            {
                a.InitLine(pictureBox, color, ((Point)PointList[i - 1]).X + dx, ((Point)PointList[i - 1]).Y + dy, ((Point)PointList[i]).X + dx, ((Point)PointList[i]).Y + dy);
                if (a.PointOnEdge(x, y))
                    return true;
            }
            a.InitLine(pictureBox, color, ((Point)PointList[0]).X + dx, ((Point)PointList[0]).Y + dy, ((Point)PointList[PointList.Count - 1]).X + dx, ((Point)PointList[PointList.Count - 1]).Y + dx);
            if (a.PointOnEdge(x, y))
                return true;
            return false;
        }

        public override bool PointInIt(int x, int y)
        {
            double cost = Math.Cos(-angle);
            double sint = Math.Sin(-angle);
            x = (int)((x - x0) * cost + (y - y0) * sint + x0);
            y = (int)(-sint * (x - x0) + cost * (y - y0) + y0);

            int i, j = PointList.Count - 1;
            bool oddNodes = false;

            for (i = 0; i < PointList.Count; i++)
            {
                if ((((Point)PointList[i]).Y + dy < y && ((Point)PointList[j]).Y + dy >= y
                || ((Point)PointList[j]).Y + dy < y && ((Point)PointList[i]).Y + dy >= y)
                && (((Point)PointList[i]).X + dx <= x || ((Point)PointList[j]).X + dx <= x))
                {
                    oddNodes ^= (((Point)PointList[i]).X + dx + (y - ((Point)PointList[i]).Y + dy) / (((Point)PointList[j]).Y + dy - ((Point)PointList[i]).Y + dy) * (((Point)PointList[j]).X + dx - ((Point)PointList[i]).X) + dx < x);
                }
                j = i;
            }

            return oddNodes;
        }

        internal void InitPolygon(PictureBox pictureBox, Color color)
        {
            type = ShapeType.polygon;
            SetPictureBox(pictureBox);
            SetColor(color);
            FinishDraw = false;
            x0 = y0 = 100000;
            x1 = y1 = -1;
            rx = ry = 1;
        }

        public override void FillColor(Color color)
        {
            ArrayList TempPoint = new ArrayList();
            TempPoint.Add(PointList[0]);
            int i, j;
            int ty0 = 10000;
            int ty1 = -1;
            Point tp = GetSpinPoint((Point)PointList[0]);
            ty1 = Math.Max(ty1, tp.Y);
            ty0 = Math.Min(ty0, tp.Y);

            for (i = 1; i < PointList.Count; i++)
            {
                tp = GetSpinPoint((Point)PointList[i]);
                ty1 = Math.Max(ty1, tp.Y);
                ty0 = Math.Min(ty0, tp.Y);
                TempPoint.Add(tp);
            }

            List<float> s = new List<float>();
            int N = TempPoint.Count;
            List<Xet> pNET = new List<Xet>();

            for(i=ty0;i<=ty1;i++)  
            {  
                for(j=0;j<N;j++)  
                {                      
                    if(((Point)TempPoint[j]).Y==i)  
                    {  
                        //按逆时针，某顶点的前一个顶点  
                        if(((Point)TempPoint[(j-1+N)%N]).Y>((Point)TempPoint[j]).Y)  
                        {  
                            Xet p = new Xet();
                            p.xmin = ((Point)TempPoint[j]).X;
                            p.ymax = ((Point)TempPoint[(j - 1 + N) % N]).Y;
                            p.ymin = ((Point)TempPoint[j]).Y;
                            p.dx = (float)(((Point)TempPoint[(j - 1 + N) % N]).X - ((Point)TempPoint[j]).X) / (((Point)TempPoint[(j - 1 + N) % N]).Y - ((Point)TempPoint[j]).Y);  
                            //判断是否为局部最值  
                            if (((Point)TempPoint[(j + 1 + N) % N]).Y <= ((Point)TempPoint[j]).Y)  
                                p.ymin++;                              
                            pNET.Add(p);
                        }  
                        //按逆时针，某顶点的后一个顶点  
                        if (((Point)TempPoint[(j + 1 + N) % N]).Y > ((Point)TempPoint[j]).Y)  
                        {
                            Xet p = new Xet();
                            p.xmin = ((Point)TempPoint[j]).X;
                            p.ymax = ((Point)TempPoint[(j + 1 + N) % N]).Y;
                            p.ymin = ((Point)TempPoint[j]).Y;
                            p.dx = (float)(((Point)TempPoint[(j + 1 + N) % N]).X - ((Point)TempPoint[j]).X) / (((Point)TempPoint[(j + 1 + N) % N]).Y - ((Point)TempPoint[j]).Y);
                            //判断是否为局部最值  
                            if (((Point)TempPoint[(j - 1 + N) % N]).Y <= ((Point)TempPoint[j]).Y)  
                                p.ymin++;
                            pNET.Add(p);
                        }  
                    }  
                }  
            }   
            //所有扫描线进行扫描  
            for(i=ty0;i<=ty1;i++)  
            {  
                for(j=0;j<pNET.Count;j++)  
                {  
                    //当前扫描线在某条边之间  
                    if(pNET[j].ymin<=i&&pNET[j].ymax>=i)  
                    {
                        s.Add(pNET[j].xmin);                        
                        //更新xmin坐标  
                        pNET[j].xmin+=pNET[j].dx;  
                    }  
                }
                s.Sort();
                for(int k=0;k+1<s.Count;k+=2){
                    int a = (int)s[k];
                    int b = (int)s[k+1];
                    for(int m=a;m<=b;m++)
                        Form1.drawPixel(pictureBox, m, i, color);
                }
                s.Clear();
            }
            fillcolor = color;
         }

        public override void Clip(int X0, int Y0, int X1, int Y1)
        {
            throw new NotImplementedException();
        }
    }

}
